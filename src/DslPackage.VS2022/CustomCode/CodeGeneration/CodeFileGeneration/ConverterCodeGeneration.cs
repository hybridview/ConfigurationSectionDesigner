using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace ConfigurationSectionDesigner
{
    internal partial class CodeFileGenerator
    {
        private void GenerateConverter( CodeMemberProperty propertyProperty, ConfigurationProperty property )
        {
            CodeAttributeDeclaration converterAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.ComponentModel.TypeConverter ) ) );
            propertyProperty.CustomAttributes.Add( converterAttribute );

            CodeTypeOfExpression converterType = null;
            switch( property.TypeConverter )
            {
                case TypeConverters.Custom:
                    CustomTypeConverter custom = property.CustomTypeConverter;
                    if (custom == null)
                    {
                        throw new ApplicationException("The type converter specified is 'Custom', but no valid 'Custom Type Converter' was selected.");
                    }
                    converterType = new CodeTypeOfExpression( GlobalSelfReference( string.Format( "{0}.{1}", custom.ConfigurationSectionModel.Namespace, custom.Name ) ) );
                    break;

                case TypeConverters.CommaDelimitedStringCollectionConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.CommaDelimitedStringCollectionConverter ) ) );
                    break;

                case TypeConverters.GenericEnumConverter:
                    //converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.GenericEnumConverter ) ) );
                    // The framework automatically calls this for enums, so do not manually set it or else ctor error occurs.
                    // http://stackoverflow.com/questions/9687795/error-reading-custom-configuration-section-no-parameterless-constructor-defined
                    converterType = null;
                    break;

                case TypeConverters.InfiniteIntConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.InfiniteIntConverter ) ) );
                    break;

                case TypeConverters.InfiniteTimeSpanConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.InfiniteTimeSpanConverter ) ) );
                    break;

                case TypeConverters.TimeSpanMinutesConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.TimeSpanMinutesConverter ) ) );
                    break;

                case TypeConverters.TimeSpanMinutesOrInfiniteConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.TimeSpanMinutesOrInfiniteConverter ) ) );
                    break;

                case TypeConverters.TimeSpanSecondsConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.TimeSpanSecondsConverter ) ) );
                    break;

                case TypeConverters.TimeSpanSecondsOrInfiniteConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.TimeSpanSecondsOrInfiniteConverter ) ) );
                    break;

                case TypeConverters.TypeNameConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.TypeNameConverter ) ) );
                    break;

                case TypeConverters.WhiteSpaceTrimStringConverter:
                    converterType = new CodeTypeOfExpression( GlobalReference( typeof( System.Configuration.WhiteSpaceTrimStringConverter ) ) );
                    break;
            }
            if (converterType != null)
            {
                converterAttribute.Arguments.Add(new CodeAttributeArgument(converterType));
            }
        }

        private void GenerateCustomConverters( ConfigurationSectionModel model, CodeCompileUnit generationUnit )
        {
            // Create the namespace block
            CodeNamespace converterNamespace = new CodeNamespace( model.Namespace );
            generationUnit.Namespaces.Add( converterNamespace );

            foreach( CustomTypeConverter converter in model.CustomTypeConverters )
            {
                // Create the converter class
                CodeTypeDeclaration customConverter = new CodeTypeDeclaration( converter.Name );
                converterNamespace.Types.Add( customConverter );
                customConverter.Comments.Add( DocComment( "<summary>" ) );
                if( !string.IsNullOrEmpty( converter.Documentation ) )
                    customConverter.Comments.Add( DocComment( converter.Documentation ) );
                else
                    customConverter.Comments.Add( DocComment( string.Format( "{0} Custom Converter", converter.Name ) ) );
                customConverter.Comments.Add( DocComment( "</summary>" ) );
                customConverter.Attributes = MemberAttributes.Public;
                customConverter.IsPartial = true;
                customConverter.IsClass = true;
                customConverter.Name = converter.Name;
                customConverter.BaseTypes.Add( GlobalReference( typeof( System.Configuration.ConfigurationConverterBase ) ) );

                CodeTypeReference convertType;
                if( converter.Type is TypeDefinition )
                    convertType = GlobalReference( converter.Type.FullName );
                else
                    convertType = GlobalSelfReference( converter.Type.FullName );

                // Override the ConvertFrom method
                CodeMemberMethod convertFromMethod = new CodeMemberMethod();
                customConverter.Members.Add( convertFromMethod );
                convertFromMethod.Comments.Add( DocComment( "<summary>" ) );
                convertFromMethod.Comments.Add( DocComment( string.Format( "Converts from <see cref=\"string\" /> to <see cref=\"{0}\" />.", GetTypeReferenceString( convertType ) ) ) );
                convertFromMethod.Comments.Add( DocComment( "</summary>" ) );
                convertFromMethod.Comments.Add( DocComment( string.Format( "<param name=\"context\">The <see cref=\"{0}\" /> that provides a format context.</param>", GetTypeReferenceString( typeof( System.ComponentModel.ITypeDescriptorContext ) ) ) ) );
                convertFromMethod.Comments.Add( DocComment( string.Format( "<param name=\"culture\">The <see cref=\"{0}\" /> to use as the current culture.</param>", GetTypeReferenceString( typeof( System.Globalization.CultureInfo ) ) ) ) );
                convertFromMethod.Comments.Add( DocComment( "<param name=\"value\">The <see cref=\"string\" /> to convert from.</param>" ) );
                convertFromMethod.CustomAttributes.Add( _generatedCodeAttribute );
                convertFromMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                convertFromMethod.ReturnType = _object;
                convertFromMethod.Name = "ConvertFrom";
                convertFromMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.ComponentModel.ITypeDescriptorContext ) ), "context" ) );
                convertFromMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Globalization.CultureInfo ) ), "culture" ) );
                convertFromMethod.Parameters.Add( new CodeParameterDeclarationExpression( _object, "value" ) );
                convertFromMethod.Statements.Add( Comment( "IMPORTANT NOTE: The code below does not build by default." ) );
                convertFromMethod.Statements.Add( Comment( "This is a custom type validator that must be implemented" ) );
                convertFromMethod.Statements.Add( Comment( "for it to build. Place the following in a separate file" ) );
                convertFromMethod.Statements.Add( Comment( "and implement the method." ) );
                convertFromMethod.Statements.Add( Comment( "" ) );

                // Generate a partial class with a ConvertFromStringToX method, then put that
                // generated code into the comments for the ConvertFrom
                // method as an instruction to the user on how to handle the conversion.
                {
                    CodeTypeDeclaration partialElementDeclaration = new CodeTypeDeclaration( customConverter.Name );
                    partialElementDeclaration.Attributes = MemberAttributes.Public;
                    partialElementDeclaration.IsPartial = true;
                    partialElementDeclaration.IsClass = true;

                    CodeMemberMethod convertFromMethodImplement = new CodeMemberMethod();
                    partialElementDeclaration.Members.Add( convertFromMethodImplement );
                    convertFromMethodImplement.Attributes = MemberAttributes.Private | MemberAttributes.Final;
                    convertFromMethodImplement.ReturnType = convertType;
                    convertFromMethodImplement.Name = string.Format( "ConvertFromStringTo{0}", converter.Type.Name );
                    convertFromMethodImplement.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.ComponentModel.ITypeDescriptorContext ) ), "context" ) );
                    convertFromMethodImplement.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Globalization.CultureInfo ) ), "culture" ) );
                    convertFromMethodImplement.Parameters.Add( new CodeParameterDeclarationExpression( _string, "value" ) );

                    // Throw NotImplementedException as per what is normal in generated methods that
                    // are intended to be filled.
                    convertFromMethodImplement.Statements.Add(
                        new CodeThrowExceptionStatement(
                            new CodeObjectCreateExpression(
                                GlobalReference( typeof( NotImplementedException ) )
                            )
                        )
                    );

                    // Convert to string and place in comments
                    string generatedCode = CodeTypeDeclarationToString( partialElementDeclaration );
                    foreach( string line in generatedCode.Split( new string[] { "\r\n" }, int.MaxValue, StringSplitOptions.None ) )
                    {
                        convertFromMethod.Statements.Add( Comment( line ) );
                    }
                    convertFromMethod.Statements.Add( new CodeMethodReturnStatement( new CodeMethodInvokeExpression( _this, convertFromMethodImplement.Name, new CodeArgumentReferenceExpression( "context" ), new CodeArgumentReferenceExpression( "culture" ), new CodeCastExpression( _string, new CodeArgumentReferenceExpression( "value" ) ) ) ) );
                }

                // Override the ConvertTo method
                CodeMemberMethod convertToMethod = new CodeMemberMethod();
                customConverter.Members.Add( convertToMethod );
                convertToMethod.Comments.Add( DocComment( "<summary>" ) );
                convertToMethod.Comments.Add( DocComment( string.Format( "Converts from <see cref=\"{0}\" /> to <see cref=\"string\" />.", GetTypeReferenceString( convertType ) ) ) );
                convertToMethod.Comments.Add( DocComment( "</summary>" ) );
                convertToMethod.Comments.Add( DocComment( string.Format( "<param name=\"context\">The <see cref=\"{0}\" /> that provides a format context.</param>", GetTypeReferenceString( typeof( System.ComponentModel.ITypeDescriptorContext ) ) ) ) );
                convertToMethod.Comments.Add( DocComment( string.Format( "<param name=\"culture\">The <see cref=\"{0}\" /> to use as the current culture.</param>", GetTypeReferenceString( typeof( System.Globalization.CultureInfo ) ) ) ) );
                convertToMethod.Comments.Add( DocComment( "<param name=\"value\">The <see cref=\"string\" /> to convert from.</param>" ) );
                convertToMethod.Comments.Add( DocComment( string.Format( "<param name=\"type\">The <see cref=\"{0}\" /> to convert the value parameter to.</param>", GetTypeReferenceString( typeof( System.Type ) ) ) ) );
                convertToMethod.CustomAttributes.Add( _generatedCodeAttribute );
                convertToMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                convertToMethod.ReturnType = _object;
                convertToMethod.Name = "ConvertTo";
                convertToMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.ComponentModel.ITypeDescriptorContext ) ), "context" ) );
                convertToMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Globalization.CultureInfo ) ), "culture" ) );
                convertToMethod.Parameters.Add( new CodeParameterDeclarationExpression( _object, "value" ) );
                convertToMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Type ) ), "type" ) );
                convertToMethod.Statements.Add( Comment( "IMPORTANT NOTE: The code below does not build by default." ) );
                convertToMethod.Statements.Add( Comment( "This is a custom type validator that must be implemented" ) );
                convertToMethod.Statements.Add( Comment( "for it to build. Place the following in a separate file" ) );
                convertToMethod.Statements.Add( Comment( "and implement the method." ) );
                convertToMethod.Statements.Add( Comment( "" ) );

                // Generate a partial class with a ConvertToXFromString method, then put that
                // generated code into the comments for the ConvertTo
                // method as an instruction to the user on how to handle the conversion.
                {
                    CodeTypeDeclaration partialElementDeclaration = new CodeTypeDeclaration( customConverter.Name );
                    partialElementDeclaration.Attributes = MemberAttributes.Public;
                    partialElementDeclaration.IsPartial = true;
                    partialElementDeclaration.IsClass = true;

                    CodeMemberMethod convertToMethodImplement = new CodeMemberMethod();
                    partialElementDeclaration.Members.Add( convertToMethodImplement );
                    convertToMethodImplement.Attributes = MemberAttributes.Private | MemberAttributes.Final;
                    convertToMethodImplement.ReturnType = _string;
                    convertToMethodImplement.Name = string.Format( "ConvertTo{0}FromString", converter.Type.Name );
                    convertToMethodImplement.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.ComponentModel.ITypeDescriptorContext ) ), "context" ) );
                    convertToMethodImplement.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Globalization.CultureInfo ) ), "culture" ) );
                    convertToMethodImplement.Parameters.Add( new CodeParameterDeclarationExpression( convertType, "value" ) );
                    convertToMethodImplement.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Type ) ), "type" ) );

                    // Suggest a ToString() call
                    convertToMethodImplement.Statements.Add(
                        new CodeMethodReturnStatement(
                            new CodeMethodInvokeExpression(
                                new CodeArgumentReferenceExpression( "value" ),
                                "ToString"
                            )
                        )
                    );

                    // Convert to string and place in comments
                    string generatedCode = CodeTypeDeclarationToString( partialElementDeclaration );
                    foreach( string line in generatedCode.Split( new string[] { "\r\n" }, int.MaxValue, StringSplitOptions.None ) )
                    {
                        convertToMethod.Statements.Add( Comment( line ) );
                    }
                    convertToMethod.Statements.Add( new CodeMethodReturnStatement( new CodeMethodInvokeExpression( _this, convertToMethodImplement.Name, new CodeArgumentReferenceExpression( "context" ), new CodeArgumentReferenceExpression( "culture" ), new CodeCastExpression( convertType, new CodeArgumentReferenceExpression( "value" ) ), new CodeArgumentReferenceExpression( "type" ) ) ) );
                }
            }
        }
    }
}
