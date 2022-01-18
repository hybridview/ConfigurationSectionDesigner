using Microsoft.VisualStudio.Modeling;

namespace ConfigurationSectionDesigner
{
    [RuleOn(typeof(ConfigurationElementCollectionHasItemType), FireTime = TimeToFire.TopLevelCommit)]
    public sealed class ConfigurationElementCollectionHasItemTypeRolePlayerChangeRule : RolePlayerChangeRule
    {
        public override void RolePlayerChanged(RolePlayerChangedEventArgs e)
        {
            ConfigurationElementCollectionHasItemType link = e.ElementLink as ConfigurationElementCollectionHasItemType;
            if (link != null && !link.Store.TransactionManager.CurrentTransaction.IsSerializing)
            {
                // When the item type changes, set the XML item name to the new item type's name but camelCased.
                link.ConfigurationElementCollection.XmlItemName = NamingHelper.ToCamelCase(link.ConfigurationElement.Name);
            }
        }
    }
}