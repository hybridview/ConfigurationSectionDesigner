using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    [ValidationState( ValidationState.Enabled )]
    public partial class IntegerValidator
    {
        #region Validation

        /// <summary>
        /// Validates the values
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod( ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save )]
        private void ValidateValues( ValidationContext context )
        {
            if( this.MaxValue < this.MinValue )
            {
                context.LogError( "The MaxValue cannot be less than the MinValue", "InvalidRange", this );
            }
        }

        #endregion
    }
}
