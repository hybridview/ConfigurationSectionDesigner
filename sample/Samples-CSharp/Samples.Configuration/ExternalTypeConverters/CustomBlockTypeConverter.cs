using System;
using Samples.Configuration.ExternalTypes;

//namespace Samples.Configuration.ExternalTypeConverters


/* TODO: The fact that the namespace has to match the csd namespace is a limitation that should be addressed. */
namespace Samples.Configuration.ExternalTypeSample
{
    /// <summary>
    /// Partial class with our type converter implementation that is to be used in the generated code file.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: The NAME entered in the "Custom Type Converters" found in "Configuration Explorer" is used to 
    /// generate a partial class in the generated code file (even if not assigned to anything in the diagram). 
    /// In this case, "RectangleTypeConverter" was entered in Name, so a partial class named "RectangleTypeConverter" 
    /// was generated. 
    /// In this file, you create the same partial class definition, but create the actual implementation. The 
    /// generated partial class creates a stub to make this easier. 
    /// 
    /// This method defination was copied from the generated ExternalTypeSample.csd.cs, which 
    /// created a stub for us. The name and namespace of this class MUST be the same as the type converter class 
    /// that was generated (it is partial).
    /// </remarks>
    public partial class CustomBlockTypeConverter
    {
        /// <summary>
        /// Converts the string representation of the custom type contained in the config value into the 
        /// proper class.
        /// 
        /// The RectangleTypeConverter partial class contained in the GENERATED code has a method called "ConvertFrom".
        /// This "ConvertFrom" method makes a call to "ConvertFromStringToRectangle". We actually implement the 
        /// "ConvertFromStringToRectangle" method here.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private global::Samples.Configuration.ExternalTypes.CustomBlock ConvertFromStringToCustomBlock(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, string value)
        {
            var ct = new CustomBlock();

            string[] values = value.Split(',');
            if (values.Length != 3)
                throw new ArgumentException("Invalid input string");

            //H: 42, W: 314, D: 312

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

        /// <summary>
        /// Converts the class object representation of the custom type into a compatible string.
        /// 
        /// The RectangleTypeConverter partial class contained in the GENERATED code has a method called "ConvertTo".
        /// This "ConvertTo" method makes a call to "ConvertToRectangleFromString". We actually implement the 
        /// "ConvertToRectangleFromString" method here.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ConvertToCustomBlockFromString(
            global::System.ComponentModel.ITypeDescriptorContext context, 
            global::System.Globalization.CultureInfo culture,
            global::Samples.Configuration.ExternalTypes.CustomBlock value, 
            global::System.Type type)
        {
            return string.Format("H: {0}, W: {1}, D: {2}", value.Height, value.Width, value.Depth);
        }
    }
}
