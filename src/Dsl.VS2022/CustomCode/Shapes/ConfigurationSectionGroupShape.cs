using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using ConfigurationSectionDesigner.Properties;

namespace ConfigurationSectionDesigner
{
    public partial class ConfigurationSectionGroupShape
    {
        #region Provide Icons For Compartment Entries

        protected override CompartmentMapping[] GetCompartmentMappings( Type melType )
        {
            // The ConfigurationSectionGroupShape is defined as Double Derived, so we
            // can override this method and modify the return value to include an icon image getter.

            // First we retrieve the basic compartment mappings as configured in the DSL definition.
            CompartmentMapping[] mappings = base.GetCompartmentMappings( melType );

            // Then, for each compartment, we provide an image getter that determines the icon to display.
            foreach( ElementListCompartmentMapping mapping in mappings )
            {
                mapping.ImageGetter = CompartmentImageProvider;
            }
            return mappings;
        }

        /// <summary>
        /// Determines the icon to show in a compartment entry, based on its properties.
        /// </summary>
        /// <param name="element">The configuration section group property being shown in the compartment.</param>
        /// <returns>The icon to use to represent the configuration section group property.</returns>
        private Image CompartmentImageProvider( ModelElement element )
        {
            // Show icons.
            if( element is ConfigurationSectionGroupProperty )
            {
                return Resources.ConfigurationSectionGroup;
            }
            else if( element is ConfigurationSectionProperty )
            {
                return Resources.ConfigurationSection;
            }
            return null;
        }

        #endregion
    }
}
