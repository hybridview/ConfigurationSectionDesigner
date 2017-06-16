using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    [ValidationState( ValidationState.Enabled )]
    public partial class TimeSpanValidator
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

        // This is necessary because the DSL designer won't let you set TimeSpan.MaxValue/MinValue as default values

        TimeSpan maxValue = TimeSpan.MaxValue;

        public TimeSpan GetMaxValueValue()
        {
            return maxValue;
        }

        public void SetMaxValueValue( TimeSpan value )
        {
            this.maxValue = value;
        }

        TimeSpan minValue = TimeSpan.MinValue;

        public TimeSpan GetMinValueValue()
        {
            return minValue;
        }

        public void SetMinValueValue( TimeSpan value )
        {
            this.minValue = value;
        }

        #endregion
    }
}
