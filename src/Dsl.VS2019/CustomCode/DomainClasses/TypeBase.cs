using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigurationSectionDesigner.Properties;
using Microsoft.VisualStudio.Modeling;

namespace ConfigurationSectionDesigner
{
    public abstract partial class TypeBase
    {
        #region Convenience Properties

        /// <summary>
        /// Gets the full type name of this configuration type.
        /// </summary>
        public virtual string FullName
        {
            get
            {
                return string.Format("{0}.{1}", this.Namespace, this.Name);
            }
        }

        #endregion

        #region NameChanged Event (for validation and for derived classes to set XML name) - These execute whenever property is set, either from CSD load or user change.

        /// <summary>
        /// Occurs when the name of the ConfigurationElement changed.
        /// </summary>
        protected event EventHandler<EventArgs> NameChanged;

        /// <summary>
        /// Raises the <see cref="E:NameChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnNameChanged(EventArgs e)
        {
            if (this.NameChanged != null)
            {
                this.NameChanged(this, EventArgs.Empty);
            }
        }

        internal sealed partial class NamePropertyHandler
        {
            protected override void OnValueChanged(TypeBase element, string oldValue, string newValue)
            {
                // Don't run in an undo or when store is loading from file (CSD with issue could never open!).
                if (!element.Store.InUndoRedoOrRollback && !element.Store.InSerializationTransaction)
                {

                    newValue = newValue.Trim();
                    // Trim and set new value in case user adds spaces by accident.
                    element.Name = newValue;
                    // We will want to use more specific validation if possible, which is done in overrides (property vs element vs section).
                    // Hard validation of the new name. 
                    string msg = "";
                    if (!NamingHelper.TryValidateBaseName(element, newValue, NamingHelper.ValidationOptions.None, out msg))
                    {
                        throw new ArgumentException(msg, element.GetType().Name);
                    }

                    // Raise the NameChanged event for derived classes to act upon.
                    element.OnNameChanged(EventArgs.Empty);
                }

                // Always call the base class method.
                base.OnValueChanged(element, oldValue, newValue);
            }
        }

        #endregion
    }
}
