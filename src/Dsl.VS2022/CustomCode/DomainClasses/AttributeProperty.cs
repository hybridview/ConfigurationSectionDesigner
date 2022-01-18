using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;

namespace ConfigurationSectionDesigner
{
    public partial class AttributeProperty
    {
        #region Convenience Properties

        /// <summary>
        /// Gets the type name of this property.
        /// </summary>
        public override string TypeName
        {
            get
            {

                if (this.Type == null)
                {
                    // prevent null ref exception when saving csd before setting type.
                    return "";
                }
                // Return the type name of the TypeDefinition that is set as this Attribute's type.
                return string.Format("{0}.{1}", this.Type.Namespace, this.Type.Name);
            }
        }

        #endregion
    }
}