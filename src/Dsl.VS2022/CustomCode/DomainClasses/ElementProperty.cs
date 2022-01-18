using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    public partial class ElementProperty
    {
        #region Convenience Properties

        /// <summary>
        /// Gets the type name of this property.
        /// </summary>
        public override string TypeName
        {
            get
            {
                // Return the type name of the Configuration Element that is set as this Element's type.
                return this.Type.FullName;
            }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates the Type property.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateType(ValidationContext context)
        {
            if (this.Type == null)
            {
                context.LogError("The Type is required and cannot be null.", "RequiredProperty", this);
            }
            else if( this.Type.InheritanceModifier == InheritanceModifiers.Abstract )
            {
                context.LogError( "The Type cannot be abstract.", "InvalidProperty", this );
            }
        }

        #endregion
    }
}