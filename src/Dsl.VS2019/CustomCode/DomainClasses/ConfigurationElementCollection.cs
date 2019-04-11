using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationSectionDesigner.Properties;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    public partial class ConfigurationElementCollection
    {
        #region Convenience Properties

        /// <summary>
        /// Gets a full documentation text based on the user-given <see cref="Documentation"/>.
        /// </summary>
        /// <remarks>
        /// If no user-given documentation is present, a "good enough" default description is returned.
        /// </remarks>
        public override string DocumentationText
        {
            get
            {
                if (string.IsNullOrEmpty(this.Documentation) && this.ItemType == null)
                {
                    // prevent null ref exception when saving csd before setting element type.
                    return "";
                }
                return (string.IsNullOrEmpty(this.Documentation) ? string.Format("A collection of {0} instances.", this.ItemType.Name) : this.Documentation);
            }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates the XML Name.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateXmlName(ValidationContext context)
        {
            string msg = "";
            if (!NamingHelper.TryValidateElementsItemNameProperty(this.Name, NamingHelper.ValidationOptions.IsRequired | NamingHelper.ValidationOptions.IsXml, out msg))
            {
                context.LogError(msg, "RequiredProperty", this);
            }
        }

        /// <summary>
        /// Validates the Item Type.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateItemType(ValidationContext context)
        {
            if (this.ItemType == null)
            {
                context.LogError("The Item Type is required and cannot be null.", "RequiredProperty", this);
            }
            else if (this.ItemType.InheritanceModifier == InheritanceModifiers.Abstract)
            {
                context.LogError("The collection's Item Type cannot be abstract.", "InvalidProperty", this, this.ItemType);
            }
            else
            {
                // Make sure that the type of the item in this collection has exactly one key property.

                int numKeys = this.ItemType.KeyProperties.Count;

                if (numKeys == 0 && !this.UseHashForElementKey)
                {
                    context.LogError("The Item Type of this collection does not have a key property.", "InvalidItemKey", this, this.ItemType);
                }
                else if (numKeys > 1)
                {
                    context.LogError("The Item Type of this collection has more than one key property.", "InvalidItemKey", this, this.ItemType);
                }
            }
        }

        #endregion
    }
}