using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using System.Text.RegularExpressions;

namespace ConfigurationSectionDesigner
{
    [ValidationState( ValidationState.Enabled )]
    public partial class RegexStringValidator
    {
        #region Validation

        /// <summary>
        /// Validates the regex
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod( ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save )]
        private void ValidateRegex( ValidationContext context )
        {
            try
            {
                new Regex( this.RegularExpression );
            }
            catch( ArgumentException e )
            {
                context.LogError( string.Format( "Invalid regular expression: {0}", e.Message ), "InvalidRegex", this );
            }
        }

        #endregion
    }
}
