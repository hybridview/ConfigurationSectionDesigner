using Microsoft.VisualStudio.Modeling.Extensibility;
using Microsoft.VisualStudio.Modeling.Validation;
using System;
using System.Diagnostics;
using System.Text;

namespace ConfigurationSectionDesigner
{

    // IMPORTANT: This represents an Attribute! Caused confusion...

    [ValidationState(ValidationState.Enabled)]
    public partial class ConfigurationProperty
    {





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
                return (string.IsNullOrEmpty(this.Documentation) ? string.Format("The {0}.", this.Name) : this.Documentation);
            }
        }

        /// <summary>
        /// Gets a full property documentation text based on the user-given <see cref="Documentation"/>.
        /// </summary>
        /// <remarks>
        /// This is used to document a .NET property in code (i.e. adds a "Gets" or "Gets or sets" prefix
        /// depending if the property is read-only).
        /// </remarks>
        public string DocumentationPropertyText
        {
            get
            {
                string propertyDocName = this.DocumentationText;
                string propertyDoc = (this.IsReadOnly ? "Gets " : "Gets or sets ");
                propertyDoc += NamingHelper.ToCamelCase(propertyDocName);
                return propertyDoc;
            }
        }

        /// <summary>
        /// Gets the type name of this property.
        /// </summary>
        public abstract string TypeName
        {
            get;
        }

        #endregion

        #region Calculated value for Custom Attributes

        private string GetCustomAttributesValue()
        {
            StringBuilder sb = new StringBuilder();
            if (Attributes.Count > 0)
            {
                // List each attribute separated with a comma and a space
                foreach (Attribute attribute in Attributes)
                    sb.AppendFormat("{0}, ", attribute.ToString());

                // Remove the final comma and space
                sb.Remove(sb.Length - 2, 2);

            }
            return sb.ToString();
        }

        #endregion

        #region "Name Validation and Automatically Set XML Name - These execute whenever property is set, either from CSD load or user change."



        // Value handler for the ConfigurationProperty.Name domain property (See in DomainClasses).
        internal sealed partial class NamePropertyHandler
        {

            protected override void OnValueChanged(ConfigurationProperty element, string oldValue, string newValue)
            {

                /*
                // Accessing validator.
                Microsoft.VisualStudio.Modeling.Shell.VsValidationController validator = new Microsoft.VisualStudio.Modeling.Shell.VsValidationController(element.Store);
                if (!validator.Validate(element.Store, ValidationCategories.Menu))
                {
                    element.Store.TransactionManager.CurrentTransaction.Rollback();
                    System.Windows.Forms.MessageBox.Show(validator.ValidationMessages.First().Description);
                }
                */

                //bool isParentCollection = this.FindAncestor<ConfigurationElementCollection>(

                // Don't run in an undo or when store is loading from file (CSD with issue could never open!).
                if (!element.Store.InUndoRedoOrRollback && !element.Store.InSerializationTransaction)
                {
                    newValue = newValue.Trim();
                    // Trim and set new value in case user adds spaces by accident.
                    element.Name = newValue;
                    // Hard validation of the new name. 
                    //NamingHelper.ValidateRequiredName(newValue);

                    // TODO: Should verify IsDefaultCollection validition is enforced or check if parent element is a collection.
                    NamingHelper.ValidationOptions options = NamingHelper.ValidationOptions.None;
                    if (NamingHelper.RequiresValidation(newValue, element, out options))
                    {
                        string msg = "";
                        if (!NamingHelper.TryValidateAttributesItemNameProperty(newValue, newValue, options, out msg))
                        {
                            throw new ArgumentException(msg, "RequiredProperty");
                        }
                    }

                    // When the name changes, set the XML name to the same name but camelCased.
                    element.XmlName = NamingHelper.ToCamelCase(element.Name);
                }

                // Always call the base class method.
                base.OnValueChanged(element, oldValue, newValue);
            }
        }

        #endregion

        #region Automatically Set Required If Key

        internal sealed partial class IsKeyPropertyHandler
        {
            protected override void OnValueChanged(ConfigurationProperty element, bool oldValue, bool newValue)
            {
                if (!element.Store.InUndoRedoOrRollback)
                {
                    // If the Key property was set to true, make it Required as well.
                    if (newValue)
                    {
                        element.IsRequired = true;
                    }
                }

                // Always call the base class method.
                base.OnValueChanged(element, oldValue, newValue);
            }
        }

        #endregion

        #region "Validation - These validations are called when CSD is SAVED."

        /// <summary>
        /// Validates the Name property.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateName(ValidationContext context)
        {
            Debug.WriteLine("ConfigurationProperty.ValidateName called for " + this.Name); // CALLED!


            // TODO: Should verify IsDefaultCollection validition is enforced or check if parent element is a collection.
            NamingHelper.ValidationOptions options = NamingHelper.ValidationOptions.None;
            if (NamingHelper.RequiresValidation(this.Name, this, out options))
            {
                Debug.WriteLine(" - ConfigurationProperty.ValidateName: Reqiures validation!");
                string msg = "";
                if (!NamingHelper.TryValidateAttributesItemNameProperty(this.Name, this.Name, options, out msg))
                {
                    context.LogError(msg, "RequiredProperty", this);
                }
            }
        }

        /// <summary>
        /// Validates the XML Name.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateXmlName(ValidationContext context)
        {
            Debug.WriteLine("ConfigurationProperty.ValidateXmlName called for " + this.XmlName); // CALLED!
            //this.XmlName = this.XmlName.Trim();

            // TODO: Should verify IsDefaultCollection validition is enforced or check if parent element is a collection.
            Debug.WriteLine("------------- > GetBaseElement() = " + this.GetBaseElement().GetType().Name);

            NamingHelper.ValidationOptions options = NamingHelper.ValidationOptions.None;
            if (NamingHelper.RequiresValidation(this.XmlName, this, out options))
            {
                Debug.WriteLine(" - ConfigurationProperty.ValidateXmlName: Reqiures validation!");
                string msg = "";
                if (!NamingHelper.TryValidateAttributesItemNameProperty(this.Name, this.XmlName, options, out msg))
                {
                    context.LogError(msg, "RequiredProperty", this);
                }
            }




        }

        /// <summary>
        /// Validates the fact that a Key property should also be Required.
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateRequiredIfKey(ValidationContext context)
        {
            if (this.IsKey && !this.IsRequired)
            {
                context.LogError("A Key property must have Required set to True.", "KeyPropertyMustBeRequired", this);
            }
        }

        #endregion
    }
}