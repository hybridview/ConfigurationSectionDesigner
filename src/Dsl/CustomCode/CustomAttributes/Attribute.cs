using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace ConfigurationSectionDesigner
{
    /// <summary>
    /// Represents an attribute.
    /// </summary>
    public partial class Attribute
    {

        /// <summary>
        /// Converts the attribute into its string representation.
        /// </summary>
        /// <returns>A string representation of this attribute.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( "[" );
            sb.Append( Name );
            if( Parameters.Count > 0 )
            {
                sb.Append( "(" );
                foreach( AttributeParameter parameter in Parameters )
                    sb.AppendFormat( "{0}, ", parameter.ToString() );
                sb.Remove( sb.Length - 2, 2 );
                sb.Append( ")" );
            }
            sb.Append( "]" );

            return sb.ToString();
        }
    }

    /// <summary>
    /// Represents a parameter in an attribute.
    /// </summary>
    public partial class AttributeParameter
    {
        /// <summary>
        /// Converts the parameter into its string representation.
        /// </summary>
        /// <returns>A string representation of this parameter.</returns>
        public override string ToString()
        {
            if( !string.IsNullOrEmpty( Name ) )
                return string.Format( "{0} = {1}", Name, Value );
            else
                return Value;
        }
    }
}
