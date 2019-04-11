using ConfigurationSectionDesigner.Properties;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    [ValidationState(ValidationState.Enabled)]
    public partial class EnumerationLiteral
    {
        #region Validation

        /// <summary>
        /// Validates the Name property.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateName(ValidationContext context)
        {
            string msg = "";
            if (!NamingHelper.TryValidateBaseName(this, this.Name, NamingHelper.ValidationOptions.IsRequired, out msg))
            {
                context.LogError(msg, "RequiredProperty", this);
            }

        }

        #endregion

        #region Convenience Properties

        /// <summary>
        /// Gets a full documentation text based on the user-given <see cref="Documentation"/>.
        /// </summary>
        /// <remarks>
        /// If no user-given documentation is present, a "good enough" default description is returned.
        /// </remarks>
        public string DocumentationText
        {
            get
            {
                return (string.IsNullOrEmpty(this.Documentation) ? string.Format("{0}.", this.Name) : this.Documentation);
            }
        }

        #endregion
    }
}