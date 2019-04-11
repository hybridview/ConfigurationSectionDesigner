using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace ConfigurationSectionDesigner
{
    internal partial class OptionsCodeFileGenerator
    {
        private void GenerateValidator( ConfigurationElement element, CodeTypeDeclaration elementClass, CodeMemberProperty propertyProperty, ConfigurationProperty property )
        {
            PropertyValidator validator = property.Validator;
            if( validator is CallbackValidator )
            {
                CallbackValidator callbackValidator = validator as CallbackValidator;

                CodeAttributeDeclaration callbackValidatorAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.CallbackValidatorAttribute ) ) );
                propertyProperty.CustomAttributes.Add( callbackValidatorAttribute );
                
                string callbackClassName = string.Format( "{0}CallbackValidatorClass", callbackValidator.Name );
                string callbackMethodName = string.Format( "{0}Callback", callbackValidator.Callback );
                string FQ = string.Format( "{0}.{1}", element.ConfigurationSectionModel.Namespace, callbackClassName );

                callbackValidatorAttribute.Arguments.Add(
                    new CodeAttributeArgument(
                        "Type",
                        new CodeTypeOfExpression( GlobalSelfReference( FQ ) )
                    )
                );

                callbackValidatorAttribute.Arguments.Add(
                    new CodeAttributeArgument(
                        "CallbackMethodName",
                        new CodePrimitiveExpression( callbackMethodName )
                    )
                );
            }
            else if( validator is IntegerValidator )
            {
                IntegerValidator integerValidator = validator as IntegerValidator;

                CodeAttributeDeclaration integerValidatorAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.IntegerValidatorAttribute ) ),
                    new CodeAttributeArgument( "ExcludeRange", new CodePrimitiveExpression( integerValidator.ExcludeRange ) ),
                    new CodeAttributeArgument( "MaxValue", new CodePrimitiveExpression( integerValidator.MaxValue ) ),
                    new CodeAttributeArgument( "MinValue", new CodePrimitiveExpression( integerValidator.MinValue ) )
                );
                propertyProperty.CustomAttributes.Add( integerValidatorAttribute );
            }
            else if( validator is LongValidator )
            {
                LongValidator longValidator = validator as LongValidator;
                
                CodeAttributeDeclaration longValidatorAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.LongValidatorAttribute ) ),
                    new CodeAttributeArgument( "ExcludeRange", new CodePrimitiveExpression( longValidator.ExcludeRange ) ),
                    new CodeAttributeArgument( "MaxValue", new CodePrimitiveExpression( longValidator.MaxValue ) ),
                    new CodeAttributeArgument( "MinValue", new CodePrimitiveExpression( longValidator.MinValue ) )
                );
                propertyProperty.CustomAttributes.Add( longValidatorAttribute );
            }
            else if( validator is TimeSpanValidator )
            {
                TimeSpanValidator timeSpanValidator = validator as TimeSpanValidator;

                CodeAttributeDeclaration timeSpanValidatorAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.TimeSpanValidatorAttribute ) ),
                    new CodeAttributeArgument( "ExcludeRange", new CodePrimitiveExpression( timeSpanValidator.ExcludeRange ) ),
                    new CodeAttributeArgument( "MaxValueString", new CodePrimitiveExpression( timeSpanValidator.MaxValue.ToString() ) ),
                    new CodeAttributeArgument( "MinValueString", new CodePrimitiveExpression( timeSpanValidator.MinValue.ToString() ) )
                );
                propertyProperty.CustomAttributes.Add( timeSpanValidatorAttribute );
            }
            else if( validator is PositiveTimeSpanValidator )
            {
                CodeAttributeDeclaration positiveTimeSpanValidatorAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.PositiveTimeSpanValidatorAttribute ) ) );
                propertyProperty.CustomAttributes.Add( positiveTimeSpanValidatorAttribute );
            }
            else if( validator is RegexStringValidator )
            {
                RegexStringValidator regexStringValidator = validator as RegexStringValidator;

                CodeAttributeDeclaration regexStringValidatorAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.RegexStringValidatorAttribute ) ),
                    new CodeAttributeArgument( new CodePrimitiveExpression( regexStringValidator.RegularExpression ) )
                );
                propertyProperty.CustomAttributes.Add( regexStringValidatorAttribute );
            }
            else if( validator is StringValidator )
            {
                StringValidator stringValidator = validator as StringValidator;

                CodeAttributeDeclaration stringValidatorAttribute = new CodeAttributeDeclaration( GlobalReference( typeof( System.Configuration.StringValidatorAttribute ) ),
                    new CodeAttributeArgument( "InvalidCharacters", new CodePrimitiveExpression( stringValidator.InvalidCharacters ) ),
                    new CodeAttributeArgument( "MaxLength", new CodePrimitiveExpression( stringValidator.MaxLength ) ),
                    new CodeAttributeArgument( "MinLength", new CodePrimitiveExpression( stringValidator.MinLength ) )
                );
                propertyProperty.CustomAttributes.Add( stringValidatorAttribute );
            }
        }

        private void GenerateValidators( ConfigurationSectionModel model, CodeCompileUnit generationUnit )
        {
            bool hasCallback = false;
            foreach( PropertyValidator validator in model.PropertyValidators.Validators )
            {
                if( validator is CallbackValidator )
                {
                    hasCallback = true;
                    break;
                }
            }
            if( hasCallback )
            {

                // Create the namespace block
                CodeNamespace callbackValidatorNamespace = new CodeNamespace( model.Namespace );
                generationUnit.Namespaces.Add( callbackValidatorNamespace );

                foreach( PropertyValidator validator in model.PropertyValidators.Validators )
                {
                    CallbackValidator callbackValidator = validator as CallbackValidator;
                    if( callbackValidator == null ) continue;

                    string callbackClassName = string.Format( "{0}CallbackValidatorClass", callbackValidator.Name );
                    string callbackMethodName = string.Format( "{0}Callback", callbackValidator.Callback );
                    string FQ = string.Format( "{0}.{1}", model.Namespace, callbackClassName );

                    CodeTypeDeclaration validationCallbackClass = new CodeTypeDeclaration( callbackClassName );
                    callbackValidatorNamespace.Types.Add( validationCallbackClass );
                    validationCallbackClass.Comments.Add( DocComment( "<summary>" ) );
                    validationCallbackClass.Comments.Add( DocComment( string.Format( "Class for the {0} callback validator", callbackValidator.Name ) ) );
                    validationCallbackClass.Comments.Add( DocComment( "</summary>" ) );
                    validationCallbackClass.Attributes = MemberAttributes.Public;
                    validationCallbackClass.IsPartial = true;
                    validationCallbackClass.IsClass = true;

                    // Generate callback method
                    CodeMemberMethod callbackCallbackMethod = new CodeMemberMethod();
                    callbackCallbackMethod.Comments.Add( DocComment( "<summary>" ) );
                    callbackCallbackMethod.Comments.Add( DocComment( string.Format( "Validation callback for the {0} callback validator", callbackValidator.Name ) ) );
                    callbackCallbackMethod.Comments.Add( DocComment( "</summary>" ) );
                    callbackCallbackMethod.Comments.Add( DocComment( "<param name=\"value\">The value to validate.</param>" ) );
                    callbackCallbackMethod.Comments.Add( DocComment( string.Format( "<exception cref=\"{0}\">The value was not valid.</exception>", GetTypeReferenceString( typeof( System.ArgumentException ) ) ) ) );
                    callbackCallbackMethod.CustomAttributes.Add( _generatedCodeAttribute );
                    callbackCallbackMethod.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final;
                    callbackCallbackMethod.ReturnType = _void;
                    callbackCallbackMethod.Name = callbackMethodName;
                    callbackCallbackMethod.Parameters.Add( new CodeParameterDeclarationExpression( _object, "value" ) );
                    callbackCallbackMethod.Statements.Add( Comment( "IMPORTANT NOTE: The code below does not build by default." ) );
                    callbackCallbackMethod.Statements.Add( Comment( "You have placed a callback validator on this property." ) );
                    callbackCallbackMethod.Statements.Add( Comment( "Copy the commented code below to a separate file and " ) );
                    callbackCallbackMethod.Statements.Add( Comment( "implement the method." ) );
                    callbackCallbackMethod.Statements.Add( Comment( "" ) );

                    // Generate a partial class with a callback method, then put that
                    // generated code into the comments for the callback
                    // method as an instruction to the user on how to handle the callback.
                    {
                        CodeTypeDeclaration partialElementDeclaration = new CodeTypeDeclaration( validationCallbackClass.Name );
                        partialElementDeclaration.Attributes = MemberAttributes.Public;
                        partialElementDeclaration.IsPartial = true;
                        partialElementDeclaration.IsClass = true;

                        CodeMemberMethod callbackMethod = new CodeMemberMethod();
                        partialElementDeclaration.Members.Add( callbackMethod );
                        callbackMethod.Attributes = callbackCallbackMethod.Attributes;
                        callbackMethod.ReturnType = _void;
                        callbackMethod.Name = callbackValidator.Callback;
                        callbackMethod.Parameters.Add( new CodeParameterDeclarationExpression( _object, "value" ) );

                        // Throw NotImplementedException as per what is normal in generated methods that
                        // are intended to be filled.
                        callbackMethod.Statements.Add(
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
                            callbackCallbackMethod.Statements.Add( Comment( line ) );
                        }

                        callbackCallbackMethod.Statements.Add(
                            new CodeMethodInvokeExpression(
                                new CodeTypeReferenceExpression( GlobalSelfReference( FQ ) ),
                                callbackValidator.Callback,
                                new CodeArgumentReferenceExpression( "value" )
                            )
                        );
                    }

                    validationCallbackClass.Members.Add( callbackCallbackMethod );
                }
            }
        }
    }
}
