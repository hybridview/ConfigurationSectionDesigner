using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ConfigurationSectionDesigner.Properties;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    [ValidationState(ValidationState.Enabled)]
    public partial class TypeDefinition
    {
        #region Validation

        /// <summary>
        /// Validates the Name property.
        /// 
        /// Called through use of ValidationMethod attribute.
        /// 
        /// Only called for primitives...
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateName(ValidationContext context)
        {

            #region "DSL Tip"

            /*
             * This method is private, so how is it executed?
             * 
             * ANSWER:
             * ALL methods in this class that have attribute "ValidationMethod" and argument of type ValidationContext 
             * will be executed when one or more actions are performed. These actions are specified as ValidationCategories 
             * flags in the ValidationMethod attribute args.
             * 
             * */


            #endregion

            Debug.WriteLine("TypeDefinition.ValidateName called for " + this.Name); // CALLED!
            if (string.IsNullOrEmpty(this.Name))
            {
                context.LogError("The Name is required and cannot be an empty string.", "RequiredProperty", this);
            }
        }

        /// <summary>
        /// Validates the Namespace property.
        /// 
        /// Namespace only used by Section, SectionGroup, Element, ElementCollection.
        /// 
        /// HOWEVER: This is only called for primitive types in reality!
        /// </summary>
        /// <param name="context">The validation context.</param>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateNamespace(ValidationContext context)
        {
            Debug.WriteLine("TypeDefinition.ValidateNamespace called for " + this.Namespace);  // CALLED!
            string msg = "";
            if (!NamingHelper.TryValidateNamespace(this, this.Name, this.Namespace, NamingHelper.ValidationOptions.IsRequired, out msg))
            {
                context.LogError(msg, "RequiredProperty", this);
            }
        }

        #endregion
    }
}