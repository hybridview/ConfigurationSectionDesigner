using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    [ValidationState( ValidationState.Enabled )]
    public partial class ConfigurationSectionGroup
    {
        #region Validation

        /// <summary>
        /// Validates the ConfigurationSectionGroup property.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod( ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save )]
        private void ValidateConfigurationSectionGroups( ValidationContext context )
        {
            if( this.ConfigurationSectionGroupProperties.Count > 0 )
            {
                // Avoid self-references.
                foreach( ConfigurationSectionGroupProperty property in this.ConfigurationSectionGroupProperties )
                {
                    if( property.ContainedConfigurationSectionGroup == this )
                        context.LogError( "A configuration section group cannot reference itself", "InvalidReference", this );
                }
            }
        }

        #endregion
    }
}
