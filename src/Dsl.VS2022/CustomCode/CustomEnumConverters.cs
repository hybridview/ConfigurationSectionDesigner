using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace ConfigurationSectionDesigner
{
    internal sealed class AccessModifierConverter : CustomEnumConverter
    {
        public AccessModifierConverter()
            : base(typeof(AccessModifiers), new string[] { "internal", "public" })
        {
        }
    }

    internal sealed class InheritanceModifierConverter : CustomEnumConverter
    {
        public InheritanceModifierConverter()
            : base(typeof(InheritanceModifiers), new string[] { "none", "abstract", "sealed" })
        {
        }
    }

    /// <summary>
    /// Ripped from DSL tools with .NET Reflector 
    /// </summary>
    internal abstract class CustomEnumConverter : EnumConverter
    {
        // Fields
        private readonly string[] customValueNames;

        // Methods
        protected CustomEnumConverter(Type type, params string[] customValueNames)
            : base(type)
        {
            this.customValueNames = customValueNames;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (culture != CultureInfo.InvariantCulture)
            {
                string str = value as string;
                if (str != null)
                {
                    int index = Array.IndexOf<string>(this.customValueNames, str.Trim());
                    if (index >= 0)
                    {
                        return index;
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (culture == CultureInfo.InvariantCulture)
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            if ((destinationType != typeof(string)) || (value == null))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            Type underlyingType = Enum.GetUnderlyingType(base.EnumType);
            IConvertible convertible = value as IConvertible;
            if ((convertible != null) && (value.GetType() != underlyingType))
            {
                value = convertible.ToType(underlyingType, culture);
            }
            if (!base.EnumType.IsDefined(typeof(FlagsAttribute), false) && !Enum.IsDefined(base.EnumType, value))
            {
                throw new ArgumentException();
            }
            return this.customValueNames[(int)value];
        }
    }
}
