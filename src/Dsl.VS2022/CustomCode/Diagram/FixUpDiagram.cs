using Microsoft.VisualStudio.Modeling;

namespace ConfigurationSectionDesigner
{
    internal partial class FixUpDiagram
    {
        private ModelElement GetParentForElementPropertyHasType( ElementPropertyHasType childLink )
        {
            // Jump from the link to the Element property and up to its "owning" Configuration Element.
            return childLink.ElementProperty.ConfigurationElement;
        }

        private ModelElement GetParentForConfigurationSectionPropertyHasConfigurationSection( ConfigurationSectionPropertyHasConfigurationSection childLink )
        {
            // Jump from the link to the Configuration Section property and up to its "owning" Configuration Section Group.
            return childLink.ConfigurationSectionProperty.ConfigurationSectionGroup;
        }

        private ModelElement GetParentForConfigurationSectionGroupPropertyHasConfigurationSectionGroup( ConfigurationSectionGroupPropertyHasConfigurationSectionGroup childLink )
        {
            // Jump from the link to the Configuration Section Group property and up to its "owning" Configuration Section Group.
            return childLink.ConfigurationSectionGroupProperty.ConfigurationSectionGroup;
        }
    }
}