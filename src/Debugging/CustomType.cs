using System;
using System.Collections.Generic;
using System.Text;
using Debugging;

namespace Debugging
{
    /// <summary>
    /// 
    /// </summary>
    public struct CustomType
    {
        /// <summary>
        /// 
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int Depth { set; get; }
    }
}

namespace Sample
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CustomTypeTypeConverter
    {
        private global::Debugging.CustomType ConvertFromStringToCustomType(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, string value)
        {
            CustomType ct = new CustomType();

            string[] values = value.Split(',');
            if (values.Length != 3)
                throw new ArgumentException("Invalid input string");

            foreach (string val in values)
            {
                string[] keyValue = val.Split(':');
                switch (keyValue[0].Trim().ToUpper())
                {
                    case "H":
                        ct.Height = int.Parse(keyValue[1]);
                        break;

                    case "W":
                        ct.Width = int.Parse(keyValue[1]);
                        break;

                    case "D":
                        ct.Depth = int.Parse(keyValue[1]);
                        break;

                    default:
                        throw new ArgumentException("Invalid input string");
                }
            }

            return ct;
        }

        private string ConvertToCustomTypeFromString(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, global::Debugging.CustomType value, global::System.Type type)
        {
            return string.Format("H: {0}, W: {1}, D: {2}", value.Height, value.Width, value.Depth);
        }
    }
}
