using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace ConfigurationSectionDesigner
{
    internal partial class CodeFileGenerator
    {
        private void GenerateProperty( ConfigurationElement element, CodeTypeDeclaration elementClass, ConfigurationProperty property )
        {
            CodeAttributeDeclarationCollection customAttributes = new CodeAttributeDeclarationCollection();
            foreach( Attribute customAttribute in property.Attributes )
            {
                CodeAttributeDeclaration attribute = new CodeAttributeDeclaration( GlobalReference( customAttribute.Name ) );
                foreach( AttributeParameter parameter in customAttribute.Parameters )
                {
                    if( !string.IsNullOrEmpty( parameter.Name ) )
                        attribute.Arguments.Add(
                            new CodeAttributeArgument( parameter.Name, new CodeSnippetExpression( parameter.Value ) ) );
                    else
                        attribute.Arguments.Add( new CodeAttributeArgument( new CodeSnippetExpression( parameter.Value ) ) );
                }
                customAttributes.Add( attribute );
            }

            string xmlPropertyName = string.Format( "{0}PropertyName", property.Name );

            // Property's XML name field
            CodeMemberField xmlPropertyNameField = new CodeMemberField( _string, xmlPropertyName );
            elementClass.Members.Add( xmlPropertyNameField );
            xmlPropertyNameField.StartDirectives.Add( Region( string.Format( "{0} Property", property.Name ) ) );
            xmlPropertyNameField.Comments.Add( DocComment( "<summary>" ) );
            xmlPropertyNameField.Comments.Add( DocComment( string.Format( "The XML name of the <see cref=\"{0}\"/> property.", property.Name ) ) );
            xmlPropertyNameField.Comments.Add( DocComment( "</summary>" ) );
            xmlPropertyNameField.CustomAttributes.Add( _generatedCodeAttribute );
            xmlPropertyNameField.Attributes = MemberAttributes.Assembly | MemberAttributes.Const;
            xmlPropertyNameField.InitExpression = new CodePrimitiveExpression( property.XmlName );

            // Property's property
            CodeMemberProperty propertyProperty = new CodeMemberProperty();

            elementClass.Members.Add( propertyProperty );
            //MemberAttributes.
            // Set modifiers, type and name
            // TODO: Property code is being generated with virtual here, but for the life of me, I cannot figure out WHY. It virtual some sort of default???
            // This is known behavior with code generator, with no known solution. Maybe there is a reason for this...
            propertyProperty.Attributes = MemberAttributes.Public; // | MemberAttributes.Final; // [a.moore] Removed to allow for TDD.
            propertyProperty.Type = (property is ElementProperty) ? GlobalSelfReference( property.TypeName ) : GlobalReference( property.TypeName );
            propertyProperty.Name = property.Name;

            propertyProperty.Comments.Add( DocComment( "<summary>" ) );
            propertyProperty.Comments.Add( DocComment( EscapeStringChars( property.DocumentationPropertyText ) ) );
            propertyProperty.Comments.Add( DocComment( "</summary>" ) );
            propertyProperty.CustomAttributes.Add( _generatedCodeAttribute );
            propertyProperty.CustomAttributes.AddRange( customAttributes );

            // Add conditional metadata attributes
            if( !string.IsNullOrEmpty( property.DocumentationText ) )
                propertyProperty.CustomAttributes.Add( new CodeAttributeDeclaration( GlobalReference( typeof( System.ComponentModel.DescriptionAttribute ) ), new CodeAttributeArgument( new CodePrimitiveExpression( EscapeStringChars( property.DocumentationText ) ) ) ) );
            if( !property.Browsable )
                propertyProperty.CustomAttributes.Add( new CodeAttributeDeclaration( GlobalReference( typeof( System.ComponentModel.BrowsableAttribute ) ), new CodeAttributeArgument( new CodePrimitiveExpression( false ) ) ) );
            if( property.IsReadOnly )
                propertyProperty.CustomAttributes.Add( new CodeAttributeDeclaration( GlobalReference( typeof( System.ComponentModel.ReadOnlyAttribute ) ), new CodeAttributeArgument( new CodePrimitiveExpression( true ) ) ) );
            if( !string.IsNullOrEmpty( property.Category ) )
                propertyProperty.CustomAttributes.Add( new CodeAttributeDeclaration( GlobalReference( typeof( System.ComponentModel.CategoryAttribute ) ), new CodeAttributeArgument( new CodePrimitiveExpression( property.Category ) ) ) );
            if( !string.IsNullOrEmpty( property.DisplayName ) )
                propertyProperty.CustomAttributes.Add( new CodeAttributeDeclaration( GlobalReference( typeof( System.ComponentModel.DisplayNameAttribute ) ), new CodeAttributeArgument( new CodePrimitiveExpression( property.DisplayName ) ) ) );
            if( property is AttributeProperty )
            {
                AttributeProperty attributeProperty = (AttributeProperty)property;
                if( attributeProperty.CustomEditor == CustomEditors.FileNameEditor )
                    propertyProperty.CustomAttributes.Add(
                        new CodeAttributeDeclaration(
                            GlobalReference( typeof( System.ComponentModel.EditorAttribute ) ),
                            new CodeAttributeArgument( _typeof( "System.Windows.Forms.Design.FileNameEditor" ) ),
                            new CodeAttributeArgument( _typeof( "System.Drawing.Design.UITypeEditor" ) )
                        )
                    );
                else if( attributeProperty.CustomEditor == CustomEditors.FolderNameEditor )
                    propertyProperty.CustomAttributes.Add(
                        new CodeAttributeDeclaration(
                            GlobalReference( typeof( System.ComponentModel.EditorAttribute ) ),
                            new CodeAttributeArgument( _typeof( "System.Windows.Forms.Design.FolderNameEditor" ) ),
                            new CodeAttributeArgument( _typeof( "System.Drawing.Design.UITypeEditor" ) )
                        )
                    );
            }

            // Add validator
            if( property.Validator != null )
            {
                GenerateValidator( element, elementClass, propertyProperty, property );
            }

            // Add converter
            if( property.TypeConverter != TypeConverters.None )
            {
                GenerateConverter( propertyProperty, property );
            }

            // Add ConfigurationPropertyAttribute
            CodeAttributeDeclaration configPropertyAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.ConfigurationPropertyAttribute ) ),
                new CodeAttributeArgument( new CodeFieldReferenceExpression( GlobalSelfReferenceExpression( string.Format( "{0}.{1}", element.ActualNamespace, element.Name ) ), xmlPropertyNameField.Name ) ),
                new CodeAttributeArgument( "IsRequired", new CodePrimitiveExpression( property.IsRequired ) ),
                new CodeAttributeArgument( "IsKey", new CodePrimitiveExpression( property.IsKey ) ),
                new CodeAttributeArgument( "IsDefaultCollection", new CodePrimitiveExpression( property.IsDefaultCollection ) ) );
            if( property is AttributeProperty )
            {
                AttributeProperty attributeProperty = (AttributeProperty)property;
                if( !string.IsNullOrEmpty( attributeProperty.DefaultValue ) )
                    configPropertyAttribute.Arguments.Add( new CodeAttributeArgument( "DefaultValue", new CodeSnippetExpression( attributeProperty.DefaultValue ) ) );
            }
            propertyProperty.CustomAttributes.Add( configPropertyAttribute );

            CodeTypeReference castType;
            if( property is ElementProperty )
                castType = GlobalSelfReference( property.TypeName );
            else
                castType = GlobalReference( property.TypeName );

            // Get method
            propertyProperty.HasGet = true;
            propertyProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeCastExpression(
                        castType,
                        new CodeIndexerExpression(
                            _base,
                            new CodeFieldReferenceExpression(
                                GlobalSelfReferenceExpression( string.Format( "{0}.{1}", element.ActualNamespace, element.Name ) ),
                                xmlPropertyName
                            )
                        )
                    )
                )
            );

            if( !property.IsReadOnly )
            {
                // Set method
                propertyProperty.HasSet = true;
                propertyProperty.SetStatements.Add(
                    new CodeAssignStatement(
                        new CodeIndexerExpression(
                            _base,
                            new CodeFieldReferenceExpression(
                                GlobalSelfReferenceExpression( string.Format( "{0}.{1}", element.ActualNamespace, element.Name ) ),
                                xmlPropertyName
                            )
                        ),
                        new CodePropertySetValueReferenceExpression()
                    )
                );
            }

            propertyProperty.EndDirectives.Add( EndRegion() );
        }

        private void GenerateAutomaticKeyProperty(ConfigurationElement element, CodeTypeDeclaration elementClass)
        {
            const string FIELD_NAME = "_autoKey";

            CodeMemberField keyField = new CodeMemberField(typeof(Guid), FIELD_NAME);

            keyField.InitExpression = new CodeMethodInvokeExpression(
                                            new CodeMethodReferenceExpression(
                                                new CodeTypeReferenceExpression(typeof(Guid)), "NewGuid"));

            CodeMemberProperty keyProperty = new CodeMemberProperty();

            // Set modifiers, type and name
            keyProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            keyProperty.Type = new CodeTypeReference(typeof(Guid));
            keyProperty.Name = "AutoGeneratedKey";

            keyProperty.Comments.Add(DocComment("<summary>"));
            keyProperty.Comments.Add(DocComment("An automatically generated key for this element."));
            keyProperty.Comments.Add(DocComment("</summary>"));

            keyProperty.HasGet = true;

            keyProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), FIELD_NAME)));

            keyField.StartDirectives.Add(Region(string.Format("{0} Property", keyProperty.Name)));
            keyProperty.EndDirectives.Add(EndRegion());

            elementClass.Members.Add(keyField);
            elementClass.Members.Add(keyProperty);
        }
    }
}
