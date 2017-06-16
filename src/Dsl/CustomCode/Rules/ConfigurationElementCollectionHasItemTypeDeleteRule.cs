using Microsoft.VisualStudio.Modeling;

namespace ConfigurationSectionDesigner
{
    [RuleOn(typeof(ConfigurationElementCollectionHasItemType), FireTime = TimeToFire.TopLevelCommit)]
    public sealed class ConfigurationElementCollectionHasItemTypeDeleteRule : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            ConfigurationElementCollectionHasItemType link = e.ModelElement as ConfigurationElementCollectionHasItemType;
            if (link != null && !link.Store.TransactionManager.CurrentTransaction.IsSerializing)
            {
                if (!link.ConfigurationElementCollection.IsDeleted && !link.ConfigurationElementCollection.IsDeleting)
                {
                    // When the item type changes, set the XML item name to the item type's name but with a lowercase first letter.
                    link.ConfigurationElementCollection.XmlItemName = null;
                }
            }
        }
    }
}