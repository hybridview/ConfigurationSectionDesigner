using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.IO;

namespace ConfigurationSectionDesigner
{
    internal partial class OptionsCodeFileGenerator
    {
        private void GenerateCustomChildElementHandler( CodeTypeDeclaration elementClass )
        {
            // Add OnDeserializeUnrecognizedElement method.
            CodeMemberMethod onDeserializeUnrecognizedElementMethod = new CodeMemberMethod();
            elementClass.Members.Add( onDeserializeUnrecognizedElementMethod );
            onDeserializeUnrecognizedElementMethod.StartDirectives.Add( Region( "Custom Child Elements" ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( "<summary>" ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( "Gets a value indicating whether an unknown element is encountered during deserialization." ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( "</summary>" ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( "<param name=\"elementName\">The name of the unknown subelement.</param>" ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( string.Format( "<param name=\"reader\">The <see cref=\"{0}\"/> being used for deserialization.</param>", GetTypeReferenceString( typeof( System.Xml.XmlReader ) ) ) ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( "<returns>" ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( "<see langword=\"true\"/> when an unknown element is encountered while deserializing; otherwise, <see langword=\"false\"/>." ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( "</returns>" ) );
            onDeserializeUnrecognizedElementMethod.Comments.Add( DocComment( string.Format( "<exception cref=\"{0}\">The element identified by <paramref name=\"elementName\"/> is locked.- or -One or more of the element's attributes is locked.- or -<paramref name=\"elementName\"/> is unrecognized, or the element has an unrecognized attribute.- or -The element has a Boolean attribute with an invalid value.- or -An attempt was made to deserialize a property more than once.- or -An attempt was made to deserialize a property that is not a valid member of the element.- or -The element cannot contain a CDATA or text element.</exception>", GetTypeReferenceString( typeof( System.Configuration.ConfigurationErrorsException ) ) ) ) );
            onDeserializeUnrecognizedElementMethod.CustomAttributes.Add( _generatedCodeAttribute );
            onDeserializeUnrecognizedElementMethod.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            onDeserializeUnrecognizedElementMethod.ReturnType = _bool;
            onDeserializeUnrecognizedElementMethod.Name = "OnDeserializeUnrecognizedElement";
            onDeserializeUnrecognizedElementMethod.Parameters.Add( new CodeParameterDeclarationExpression( _string, "elementName" ) );
            onDeserializeUnrecognizedElementMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Xml.XmlReader ) ), "reader" ) );
            onDeserializeUnrecognizedElementMethod.Statements.Add( Comment( "IMPORTANT NOTE: The code below does not build by default." ) );
            onDeserializeUnrecognizedElementMethod.Statements.Add( Comment( "You have indicated that this configuration element has" ) );
            onDeserializeUnrecognizedElementMethod.Statements.Add( Comment( "custom child elements. Copy the commented code below to" ) );
            onDeserializeUnrecognizedElementMethod.Statements.Add( Comment( "a separate file and implement the method." ) );
            onDeserializeUnrecognizedElementMethod.Statements.Add( Comment( "" ) );

            // Generate a partial class with a HandleUnrecognizedElement method, then put that
            // generated code into the comments for the OnDeserializeUnrecognizedElement
            // method as an instruction to the user on how to handle the unrecognized elements.
            {
                CodeTypeDeclaration partialElementDeclaration = new CodeTypeDeclaration( elementClass.Name );
                partialElementDeclaration.TypeAttributes = elementClass.TypeAttributes;
                partialElementDeclaration.IsPartial = true;
                partialElementDeclaration.IsClass = true;

                CodeMemberMethod handleUnrecognizedElementMethod = new CodeMemberMethod();
                partialElementDeclaration.Members.Add( handleUnrecognizedElementMethod );
                handleUnrecognizedElementMethod.Attributes = MemberAttributes.Private | MemberAttributes.Final;
                handleUnrecognizedElementMethod.ReturnType = _bool;
                handleUnrecognizedElementMethod.Name = "HandleUnrecognizedElement";
                handleUnrecognizedElementMethod.Parameters.Add( new CodeParameterDeclarationExpression( _string, "elementName" ) );
                handleUnrecognizedElementMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Xml.XmlReader ) ), "reader" ) );

                // Throw NotImplementedException as per what is normal in generated methods that
                // are intended to be filled.
                handleUnrecognizedElementMethod.Statements.Add(
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
                    onDeserializeUnrecognizedElementMethod.Statements.Add( Comment( line ) );
                }
            }
            onDeserializeUnrecognizedElementMethod.Statements.Add( new CodeMethodReturnStatement( new CodeMethodInvokeExpression( _this, "HandleUnrecognizedElement", new CodeArgumentReferenceExpression( "elementName" ), new CodeArgumentReferenceExpression( "reader" ) ) ) );
            onDeserializeUnrecognizedElementMethod.Statements.Add(Comment("REMEMBER: The method above does NOT exist until you implement it in a partial class as intstructed."));
            
            onDeserializeUnrecognizedElementMethod.EndDirectives.Add( EndRegion() );
        }
    }
}
