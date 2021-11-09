using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigurationSectionDesigner
{
	public partial class ConfigurationSectionGroupProperty
    {
        /// <summary>
        /// Gets the value of the calculated ConfigurationSectionGroupName property.
        /// </summary>
        /// <remarks>
        /// This property is used to show the name of the ConfigurationSectionGroups that the ConfigurationSectionGroup
        /// contains, as it is the only thing that makes sense to show. If a Configuration Section Group hasn't been selected yet,
        /// it will just show "(none)".
        /// </remarks>
        private string GetConfigurationSectionGroupNameValue()
        {
            if( ContainedConfigurationSectionGroup != null )
                return ContainedConfigurationSectionGroup.Name;

            return "(none)";
        }
    }
}
