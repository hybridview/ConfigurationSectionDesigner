using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    [ValidationState( ValidationState.Enabled )]
    public partial class LongValidator
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

        #region Custom Storage

        // This is necessary because the DSL designer cannot handle large Int64 values properly

        long maxValue = long.MaxValue;

        public long GetMaxValueValue()
        {
            return maxValue;
        }

        public void SetMaxValueValue( long value )
        {
            this.maxValue = value;
        }

        long minValue = long.MinValue;

        public long GetMinValueValue()
        {
            return minValue;
        }

        public void SetMinValueValue( long value )
        {
            this.minValue = value;
        }

        #endregion
    }
}
