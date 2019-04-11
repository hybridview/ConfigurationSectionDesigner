using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.IO;
using System.CodeDom.Compiler;

namespace ConfigurationSectionDesigner
{
    internal partial class OptionsCodeFileGenerator
    {
        private static CodeCommentStatement Comment( string comment )
        {
            return new CodeCommentStatement( comment );
        }

        private static CodeCommentStatement DocComment( string comment )
        {
            return new CodeCommentStatement( comment, true );
        }

        private static CodeTypeReference GlobalReference( Type type )
        {
            return new CodeTypeReference( type, CodeTypeReferenceOptions.GlobalReference );
        }

        private static CodeTypeReference GlobalReference( string type )
        {
            return new CodeTypeReference( type, CodeTypeReferenceOptions.GlobalReference );
        }

        private static CodeTypeReference GlobalReference( TypeDefinition type )
        {
            return GlobalReference( string.Format( "{0}.{1}", type.Namespace, type.Name ) );
        }

        private static CodeTypeReferenceExpression GlobalReferenceExpression( Type type )
        {
            return new CodeTypeReferenceExpression( GlobalReference( type ) );
        }

        private static CodeTypeReferenceExpression GlobalReferenceExpression( string type )
        {
            return new CodeTypeReferenceExpression( GlobalReference( type ) );
        }

        private CodeTypeReference GlobalSelfReference( string type )
        {
            if( CodeDomProvider.GetLanguageFromExtension( _codeDomProvider.FileExtension ) == "vb" )
            {
                if( !string.IsNullOrEmpty( _rootNamespace ) )
                    return new CodeTypeReference( string.Join( ".", new string[] { _rootNamespace, type } ), CodeTypeReferenceOptions.GlobalReference );
                else
                    return GlobalReference( type );
            }
            else
                return GlobalReference( type );
        }

        private CodeTypeReferenceExpression GlobalSelfReferenceExpression( string type )
        {
            return new CodeTypeReferenceExpression( GlobalSelfReference( type ) );
        }

        private static CodeRegionDirective Region( string regionText )
        {
            return new CodeRegionDirective( CodeRegionMode.Start, regionText );
        }

        private static readonly CodeRegionDirective _endRegion = new CodeRegionDirective( CodeRegionMode.End, string.Empty );
        private static CodeRegionDirective EndRegion()
        {
            return _endRegion;
        }

        private static CodeTypeOfExpression _typeof( Type type )
        {
            return new CodeTypeOfExpression( GlobalReference( type ) );
        }

        private static CodeTypeOfExpression _typeof( string type )
        {
            return new CodeTypeOfExpression( GlobalReference( type ) );
        }

        private static CodeTypeOfExpression _typeof( TypeDefinition type )
        {
            return new CodeTypeOfExpression( GlobalReference( type ) );
        }

        private CodeTypeOfExpression _selftypeof( string type )
        {
            return new CodeTypeOfExpression( GlobalSelfReference( type ) );
        }

        private string GetTypeReferenceString( CodeTypeReference type )
        {
            CodeTypeReferenceExpression configurationElementCollectionTypeType = new CodeTypeReferenceExpression( type );
            StringWriter sw = new StringWriter();
            _generator.GenerateCodeFromExpression( configurationElementCollectionTypeType, sw, _options );
            return sw.ToString();
        }

        private string GetTypeReferenceString( Type type )
        {
            CodeTypeReferenceExpression configurationElementCollectionTypeType = GlobalReferenceExpression( type );
            StringWriter sw = new StringWriter();
            _generator.GenerateCodeFromExpression( configurationElementCollectionTypeType, sw, _options );
            return sw.ToString();
        }

        private string GetTypeReferenceString( string type )
        {
            CodeTypeReferenceExpression configurationElementCollectionTypeType = GlobalReferenceExpression( type );
            StringWriter sw = new StringWriter();
            _generator.GenerateCodeFromExpression( configurationElementCollectionTypeType, sw, _options );
            return sw.ToString();
        }

        private string GetTypeSelfReferenceString( string type )
        {
            CodeTypeReferenceExpression configurationElementCollectionTypeType = GlobalSelfReferenceExpression( type );
            StringWriter sw = new StringWriter();
            _generator.GenerateCodeFromExpression( configurationElementCollectionTypeType, sw, _options );
            return sw.ToString();
        }

        private string CodeTypeDeclarationToString( CodeTypeDeclaration codeType )
        {
            StringWriter sw = new StringWriter();
            _generator.GenerateCodeFromType( codeType, sw, _options );

            string generatedCode = sw.ToString();
            return generatedCode;
        }

        /// <summary>
        /// Removes the "global::" prefix from Xml param documentation to stop the intellisense errors.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string FormatTypeReferenceStringForDoc(string type)
        {
            string globalRefStr = "global::";
            if (!string.IsNullOrEmpty(type) && type.IndexOf(globalRefStr) == 0)
            {
                return type.Remove(0, globalRefStr.Length);
            }
            else
            {
                return type;
            }
        }

        // http://stackoverflow.com/a/2662027/161250
        private static string EscapeStringChars( string txt )
        {
            if( string.IsNullOrEmpty( txt ) ) return txt;

            StringBuilder retval = new StringBuilder( txt.Length );
            for( int ix = 0; ix < txt.Length; )
            {
                int jx = txt.IndexOf( '\\', ix );
                if( jx < 0 || jx == txt.Length - 1 ) jx = txt.Length;

                retval.Append( txt, ix, jx - ix );
                if( jx >= txt.Length ) break;

                switch( txt[jx + 1] )
                {
                    case 'n': retval.Append( '\n' ); break;  // Line feed
                    case 'r': retval.Append( '\r' ); break;  // Carriage return
                    case 't': retval.Append( '\t' ); break;  // Tab
                    case '\\': retval.Append( '\\' ); break; // Don't escape
                    default:                                 // Unrecognized, copy as-is
                        retval.Append( '\\' ).Append( txt[jx + 1] ); break;
                }
                ix = jx + 2;
            }

            return retval.ToString();
        }

    }
}
