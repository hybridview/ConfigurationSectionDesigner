using System;

namespace ConfigurationSectionDesigner
{
    public partial class ConfigurationSectionDesignerDomainModel
    {
        protected override Type[] GetCustomDomainModelTypes()
        {
            return new[] 
            {
                typeof(ConfigurationElementCollectionHasItemTypeAddRule),
                typeof(ConfigurationElementCollectionHasItemTypeDeleteRule),
                typeof(ConfigurationElementCollectionHasItemTypeRolePlayerChangeRule)
            };
        }
    }
}