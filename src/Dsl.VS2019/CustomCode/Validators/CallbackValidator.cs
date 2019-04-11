using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    [ValidationState( ValidationState.Enabled )]
    public partial class CallbackValidator
    {
        #region Validation

        /// <summary>
        /// Validates the method name.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod( ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save )]
        private void ValidateMethodName( ValidationContext context )
        {
            if( string.IsNullOrEmpty( this.Callback ) )
            {
                context.LogError( "The Callback is required and cannot be an empty string.", "RequiredProperty", this );
            }
            else if( !this.Callback.IsValidIdentifier() )
            {
                context.LogError( string.Format( "'{0}' is not a valid callback method name.", this.Callback ), "RequiredProperty", this );
            }
        }

        #endregion
    }
}
