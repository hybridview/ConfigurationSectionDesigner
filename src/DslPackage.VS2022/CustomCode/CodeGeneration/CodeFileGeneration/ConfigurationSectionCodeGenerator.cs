using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace ConfigurationSectionDesigner
{
    internal partial class CodeFileGenerator
    {

        
        


        private void GenerateConfigurationSectionCode( ConfigurationElement element, CodeTypeDeclaration elementClass )
        {
            ConfigurationSection configurationSectionElement = (ConfigurationSection)element;

            string sectionName = string.Format( "{0}SectionName", element.Name );
            string FQElementName = string.Format( "{0}.{1}", element.ActualNamespace, element.Name );

            if( (configurationSectionElement.CodeGenOptions & ConfigurationSectionCodeGenOptions.Singleton) == ConfigurationSectionCodeGenOptions.Singleton )
            {
                MemberAttributes doShadowing = 0;
                IEnumerable<BaseConfigurationType> ancestors;
                configurationSectionElement.GetAncestors( out ancestors );
                foreach( BaseConfigurationType ancestor in ancestors )
                {
                    if( ancestor == configurationSectionElement )
                        continue;

                    ConfigurationSection ancestorSection = (ConfigurationSection)ancestor;
                    if( (ancestorSection.CodeGenOptions & ConfigurationSectionCodeGenOptions.Singleton) == ConfigurationSectionCodeGenOptions.Singleton )
                    {
                        doShadowing = MemberAttributes.New;
                        break;
                    }
                }

                // Add XML section name
                CodeMemberField sectionXmlNameField = new CodeMemberField( _string, sectionName );
                elementClass.Members.Add( sectionXmlNameField );
                sectionXmlNameField.StartDirectives.Add( Region( "Singleton Instance" ) );
                sectionXmlNameField.Comments.Add( DocComment( "<summary>" ) );
                sectionXmlNameField.Comments.Add( DocComment( string.Format( "The XML name of the {0} Configuration Section.", element.Name ) ) );
                sectionXmlNameField.Comments.Add( DocComment( "</summary>" ) );
                sectionXmlNameField.CustomAttributes.Add( _generatedCodeAttribute );
                sectionXmlNameField.Attributes = MemberAttributes.Assembly | MemberAttributes.Const;
                sectionXmlNameField.InitExpression = new CodePrimitiveExpression( configurationSectionElement.XmlSectionName );

                
                /**/

                // UPDATE 20140910: Developed function to support nested groups based on recommendation from WI 6053 Patch: 
                
                // Add Configuration Section Path
                string sectionPath = string.Format("{0}SectionPath", element.Name);
                CodeMemberField sectionPathField = new CodeMemberField(_string, sectionPath);
                elementClass.Members.Add(sectionPathField);
                sectionPathField.Comments.Add(DocComment("<summary>"));
                sectionPathField.Comments.Add(DocComment(string.Format("The XML path of the {0} Configuration Section.", element.Name)));
                sectionPathField.Comments.Add(DocComment("</summary>"));
                sectionPathField.CustomAttributes.Add(_generatedCodeAttribute);
                sectionPathField.Attributes = MemberAttributes.Assembly | MemberAttributes.Const;

                if (configurationSectionElement.ReferringConfigurationSectionGroup != null 
                    && !string.IsNullOrEmpty(configurationSectionElement.ReferringConfigurationSectionGroup.ConfigurationSectionGroup.Name))
                {
                    // TODO 20140910: Very limited testing so far.
                    _NestedGroupHelper(element, configurationSectionElement, ref elementClass, ref sectionPathField);
                } else {
                    // Configuration Section Path Value
                    sectionPathField.InitExpression = new CodePrimitiveExpression(configurationSectionElement.XmlSectionName);
                }
                /* */

                // TODO: Code generators should handle errors nicely. Was hard to see an error occured when there was a bug in block above. Currently, 
                // the generator simply appears to hang or stop running without any notice.

                // Add Instance property.
                CodeMemberProperty instanceProperty = new CodeMemberProperty();
                elementClass.Members.Add( instanceProperty );
                instanceProperty.Comments.Add( DocComment( "<summary>" ) );
                instanceProperty.Comments.Add( DocComment( string.Format( "Gets the {0} instance.", element.Name ) ) );
                instanceProperty.Comments.Add( DocComment( "</summary>" ) );
                instanceProperty.CustomAttributes.Add( _generatedCodeAttribute );
                instanceProperty.Attributes = doShadowing | MemberAttributes.Public | MemberAttributes.Static;
                instanceProperty.Type = GlobalSelfReference( element.FullName );
                instanceProperty.Name = "Instance";
                instanceProperty.HasGet = true;
                instanceProperty.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeCastExpression(
                            GlobalSelfReference( element.FullName ),
                            new CodeMethodInvokeExpression(
                                GlobalReferenceExpression( typeof( System.Configuration.ConfigurationManager ) ),
                                "GetSection",
                                new CodeFieldReferenceExpression(
                                    GlobalSelfReferenceExpression( FQElementName ),
                                    sectionPath // Changed from sectionName to support groups.
                                )
                            )
                        )
                    )
                );
                instanceProperty.EndDirectives.Add( EndRegion() );
            }

            if( (configurationSectionElement.CodeGenOptions & ConfigurationSectionCodeGenOptions.XmlnsProperty) == ConfigurationSectionCodeGenOptions.XmlnsProperty )
            {
                // Add Xmlns Property Name field.
                CodeMemberField xmlnsNameField = new CodeMemberField( _string, "XmlnsPropertyName" );
                elementClass.Members.Add( xmlnsNameField );
                xmlnsNameField.StartDirectives.Add( Region( "Xmlns Property" ) );
                xmlnsNameField.Comments.Add( DocComment( "<summary>" ) );
                xmlnsNameField.Comments.Add( DocComment( "The XML name of the <see cref=\"Xmlns\"/> property." ) );
                xmlnsNameField.Comments.Add( DocComment( "</summary>" ) );
                xmlnsNameField.CustomAttributes.Add( _generatedCodeAttribute );
                xmlnsNameField.Attributes = MemberAttributes.Assembly | MemberAttributes.Const;
                xmlnsNameField.InitExpression = new CodePrimitiveExpression( "xmlns" );

                // Add Xmlns property.
                CodeMemberProperty xmlnsProperty = new CodeMemberProperty();
                elementClass.Members.Add( xmlnsProperty );
                xmlnsProperty.Comments.Add( DocComment( "<summary>" ) );
                xmlnsProperty.Comments.Add( DocComment( "Gets the XML namespace of this Configuration Section." ) );
                xmlnsProperty.Comments.Add( DocComment( "</summary>" ) );
                xmlnsProperty.Comments.Add( DocComment( "<remarks>" ) );
                xmlnsProperty.Comments.Add( DocComment( "This property makes sure that if the configuration file contains the XML namespace," ) );
                xmlnsProperty.Comments.Add( DocComment( "the parser doesn't throw an exception because it encounters the unknown \"xmlns\" attribute." ) );
                xmlnsProperty.Comments.Add( DocComment( "</remarks>" ) );
                xmlnsProperty.CustomAttributes.Add( _generatedCodeAttribute );
                CodeAttributeDeclaration configPropertyAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.ConfigurationPropertyAttribute ) ),
                    new CodeAttributeArgument( new CodeFieldReferenceExpression( GlobalSelfReferenceExpression( FQElementName ), "XmlnsPropertyName" ) ),
                    new CodeAttributeArgument( "IsRequired", new CodePrimitiveExpression( false ) ),
                    new CodeAttributeArgument( "IsKey", new CodePrimitiveExpression( false ) ),
                    new CodeAttributeArgument( "IsDefaultCollection", new CodePrimitiveExpression( false ) ) );
                xmlnsProperty.CustomAttributes.Add( configPropertyAttribute );
                xmlnsProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                xmlnsProperty.Type = _string;
                xmlnsProperty.Name = "Xmlns";
                xmlnsProperty.HasGet = true;
                xmlnsProperty.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeCastExpression(
                            _string,
                            new CodeIndexerExpression(
                                _base,
                                new CodeFieldReferenceExpression(
                                    GlobalSelfReferenceExpression( FQElementName ),
                                    xmlnsNameField.Name
                                )
                            )
                        )
                    )
                );
                xmlnsProperty.EndDirectives.Add( EndRegion() );
            }

            if( (configurationSectionElement.CodeGenOptions & ConfigurationSectionCodeGenOptions.Protection) == ConfigurationSectionCodeGenOptions.Protection )
            {
                // Add Protection Provider field
                CodeMemberField protectionProviderField = new CodeMemberField( _string, "ProtectionProvider" );
                elementClass.Members.Add( protectionProviderField );
                protectionProviderField.StartDirectives.Add( Region( "Protection" ) );
                protectionProviderField.Comments.Add( DocComment( "<summary>" ) );
                protectionProviderField.Comments.Add( DocComment( "The protection provider to use for Configuration Section Protection" ) );
                protectionProviderField.Comments.Add( DocComment( "</summary>" ) );
                protectionProviderField.CustomAttributes.Add( _generatedCodeAttribute );
                protectionProviderField.Attributes = MemberAttributes.Assembly | MemberAttributes.Const;
                if( configurationSectionElement.ProtectionProvider == ProtectionProviders.DataProtectionConfigurationProvider )
                    protectionProviderField.InitExpression = new CodePrimitiveExpression( "DataProtectionConfigurationProvider" );
                else if( configurationSectionElement.ProtectionProvider == ProtectionProviders.RSAProtectedConfigurationProvider )
                    protectionProviderField.InitExpression = new CodePrimitiveExpression( "RSAProtectedConfigurationProvider" );
                else
                    protectionProviderField.InitExpression = new CodePrimitiveExpression( configurationSectionElement.CustomProtectionProvider );

                // Add Protect method
                CodeMemberMethod protectMethod = new CodeMemberMethod();
                elementClass.Members.Add( protectMethod );
                protectMethod.Comments.Add( DocComment( "<summary>" ) );
                protectMethod.Comments.Add( DocComment( "Marks this Configuration Section for protection." ) );
                protectMethod.Comments.Add( DocComment( "</summary>" ) );
                protectMethod.CustomAttributes.Add( _generatedCodeAttribute );
                protectMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                protectMethod.ReturnType = _void;
                protectMethod.Name = "Protect";
                protectMethod.Statements.Add(
                    new CodeConditionStatement(
                        new CodeBinaryOperatorExpression(
                            new CodeFieldReferenceExpression(
                                new CodeFieldReferenceExpression( _this, "SectionInformation" ),
                                "IsProtected"
                            ),
                            CodeBinaryOperatorType.ValueEquality,
                            new CodePrimitiveExpression( false )
                        ),
                        new CodeExpressionStatement(
                            new CodeMethodInvokeExpression(
                                new CodeFieldReferenceExpression( _this, "SectionInformation" ),
                                "ProtectSection",
                                new CodeFieldReferenceExpression(
                                    GlobalSelfReferenceExpression( FQElementName ),
                                    protectionProviderField.Name
                                )
                            )
                        )
                    )
                );

                // Add Unprotect method
                CodeMemberMethod unprotectMethod = new CodeMemberMethod();
                elementClass.Members.Add( unprotectMethod );
                unprotectMethod.Comments.Add( DocComment( "<summary>" ) );
                unprotectMethod.Comments.Add( DocComment( "Removes the protected configuration encryption from the associated configuration section." ) );
                unprotectMethod.Comments.Add( DocComment( "</summary>" ) );
                unprotectMethod.CustomAttributes.Add( _generatedCodeAttribute );
                unprotectMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                unprotectMethod.ReturnType = _void;
                unprotectMethod.Name = "Unprotect";
                unprotectMethod.Statements.Add(
                    new CodeConditionStatement(
                        new CodeBinaryOperatorExpression(
                            new CodeFieldReferenceExpression(
                                new CodeFieldReferenceExpression( _this, "SectionInformation" ),
                                "IsProtected"
                            ),
                            CodeBinaryOperatorType.ValueEquality,
                            new CodePrimitiveExpression( true )
                        ),
                        new CodeExpressionStatement(
                            new CodeMethodInvokeExpression(
                                new CodeFieldReferenceExpression( _this, "SectionInformation" ),
                                "UnprotectSection"
                            )
                        )
                    )
                );
                unprotectMethod.EndDirectives.Add( EndRegion() );
            }
        }

        /// <summary>
        /// Note about Groups:
        /// This feature is mainly used to organize the config XML, and this structure will show up there.
        /// When accessing sections that are part of groups via code, the section will be accessible in 
        /// the root namespace, NOT as part of a nested group. When this section is accessed, the code behind 
        /// will properly send the full XML path (with groups included) to config manager.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="configurationSectionElement"></param>
        /// <param name="elementClass"></param>
        /// <param name="sectionPathField"></param>
        private void _NestedGroupHelper(
            ConfigurationElement element,
            ConfigurationSection configurationSectionElement,
            ref CodeTypeDeclaration elementClass,
            ref CodeMemberField sectionPathField)
        {

            if (configurationSectionElement.ReferringConfigurationSectionGroup != null)
            {
                ConfigurationSectionGroup currentGroup = configurationSectionElement
                    .ReferringConfigurationSectionGroup // If this section is in a group, this is the property pointing to the group.
                    .ConfigurationSectionGroup; // This is the group containing the section.

                List<string> paths = new List<string>();

                while (currentGroup != null)
                {
                    // Commented out code below is currently not used in the generated class files and 
                    // does not properly deal with nesting. If neded in future, this can be worked on to resolve.
                    /*string sectionGroupName = string.Format("{0}SectionGroupName", element.Name);
                    // Add Configuration Section Group
                    CodeMemberField sectionGroupField = new CodeMemberField(_string, sectionGroupName);
                    elementClass.Members.Add(sectionGroupField);
                    sectionGroupField.Comments.Add(DocComment("<summary>"));
                    sectionGroupField.Comments.Add(DocComment(string.Format("The XML name of the group of {0} Configuration Section.", element.Name)));
                    sectionGroupField.Comments.Add(DocComment("</summary>"));
                    sectionGroupField.CustomAttributes.Add(_generatedCodeAttribute);
                    sectionGroupField.Attributes = MemberAttributes.Assembly | MemberAttributes.Const;
                    sectionGroupField.InitExpression = new CodePrimitiveExpression(currentGroup.Name);
                     */
                    paths.Insert(0, currentGroup.Name);
                    currentGroup = currentGroup.ReferringConfigurationSectionGroup != null ? currentGroup.ReferringConfigurationSectionGroup.ConfigurationSectionGroup : null;
                }

                // No, we build the XML path required for the groups.
                string initStr = "";
                foreach (string group in paths)
                {
                    initStr += group + "/";
                }

                sectionPathField.InitExpression = new CodePrimitiveExpression(
                    string.Format("{0}{1}",
                    initStr,
                    configurationSectionElement.XmlSectionName));
            }
        }
    }
}
