using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    [ValidationState( ValidationState.Enabled )]
    public partial class StringValidator
    {
        #region Validation

        /// <summary>
        /// Validates the values
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod( ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save )]
        private void ValidateValues( ValidationContext context )
        {
            if( this.MaxLength < this.MinLength )
            {
                context.LogError( "The MaxLength cannot be less than the MinLength", "InvalidRange", this );
            }
        }

        #endregion
    }
}
