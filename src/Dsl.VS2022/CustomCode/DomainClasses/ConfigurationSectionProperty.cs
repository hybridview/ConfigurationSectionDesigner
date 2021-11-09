using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigurationSectionDesigner
{
    public partial class ConfigurationSectionProperty
    {
        /// <summary>
        /// Gets the value of the calculated ConfigurationSectionName property.
        /// </summary>
        /// <remarks>
        /// This property is used to show the name of the ConfigurationSections that the ConfigurationSectionGroup
        /// contains, as it is the only thing that makes sense to show. If a Configuration Section hasn't been selected yet,
        /// it will just show "(none)".
        /// </remarks>
        private string GetConfigurationSectionNameValue()
        {
            if( ContainedConfigurationSection != null )
                return ContainedConfigurationSection.Name;

            return "(none)";
        }
    }
}
