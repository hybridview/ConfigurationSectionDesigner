using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Drawing.Drawing2D;
using Microsoft.VisualStudio.PlatformUI;

namespace ConfigurationSectionDesigner
{
    [ValidationState(ValidationState.Enabled)]
    public abstract partial class BaseConfigurationType
    {
        #region Convenience Properties

        /// <summary>
        /// Gets the value of the calculated TypeName property.
        /// </summary>
        /// <remarks>
        /// This property is used to show the actual type of the MEL in the shape,
        /// i.e. ConfigurationElement, ConfigurationElementCollection or ConfigurationSection.
        /// </remarks>
        public string GetTypeNameValue()
        {
            return this.GetType().Name;
        }

        /// <summary>
        /// Gets the full type name of this configuration type.
        /// </summary>
        public override string FullName
        {
            get
            {
                return string.Format("{0}.{1}", this.ActualNamespace, this.Name);
            }
        }
        /// <summary>
        /// Gets the actual namespace this configuration type should be declared in.
        /// </summary>
        public string ActualNamespace
        {
            get
            {
                return string.IsNullOrEmpty(this.Namespace) ? this.ConfigurationSectionModel.Namespace : this.Namespace;
            }
        }

        #endregion

        #region Validation

        /// <summary>
        /// Makes sure that the inheritance settings for this type are valid
        /// </summary>
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateInheritance(ValidationContext context)
        {
            if (this.BaseClass != null)
            {
                if (this.BaseClass.GetType() != this.GetType())
                    context.LogError(string.Format("A {0} can only have another {0} as its base class.", this.GetType().Name), "InvalidInheritance", this, this.BaseClass);

                if (this.BaseClass.InheritanceModifier == InheritanceModifiers.Sealed)
                    context.LogError("A type cannot have a sealed type as its base class", "InvalidInheritance", this, this.BaseClass);

                if (this.AccessModifier == AccessModifiers.Public && this.BaseClass.AccessModifier == AccessModifiers.Internal)
                    context.LogError("A type with a public access modifier cannot inherit from an type with an internal access modifier", "InvalidInheritance", this, this.BaseClass);

                IEnumerable<BaseConfigurationType> ancestors;
                if (GetAncestors(out ancestors))
                    context.LogError(string.Format("There's a loop in the inheritance of {0}", this.Name), "InvalidInheritance", ancestors.ToArray());
            }
        }



        /// <summary>
        /// Returns the ancestors of this BaseConfigurationType (includes this type)
        /// </summary>
        /// <param name="ancestors">The list of ancestors</param>
        /// <returns><see langref="false"/> if there are no loops in the inheritance graph, else <see langref="true"/></returns>
        protected internal bool GetAncestors(out IEnumerable<BaseConfigurationType> ancestors)
        {
            LinkedList<BaseConfigurationType> list = new LinkedList<BaseConfigurationType>();
            ancestors = list;
            for (BaseConfigurationType ancestor = this; ancestor != null; ancestor = ancestor.BaseClass)
            {
                if (ancestors.Contains(ancestor))
                {
                    return true;
                }
                list.AddLast(ancestor);
            }
            return false;
        }

        #endregion

        #region InheritanceModifierChanged Event (for validation and for derived classes to react on change)

        /// <summary>
        /// Occurs when the inheritance modifier of this type is changed.
        /// </summary>
        protected event EventHandler<EventArgs> InheritanceModifierChanged;

        /// <summary>
        /// Raises the <see cref="E:InheritanceModifierChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnInheritanceModifierChanged(EventArgs e)
        {
            if (this.InheritanceModifierChanged != null)
            {
                this.InheritanceModifierChanged(this, EventArgs.Empty);
            }
        }

        internal sealed partial class InheritanceModifierPropertyHandler
        {
            protected override void OnValueChanged(BaseConfigurationType element, InheritanceModifiers oldValue, InheritanceModifiers newValue)
            {
                if (!element.Store.InUndoRedoOrRollback)
                {
                    // Raise the NameChanged event for derived classes to act upon.
                    element.OnInheritanceModifierChanged(EventArgs.Empty);
                }

                #region Inheritance shape styling

                if (PresentationViewsSubject.GetPresentation(element).Count == 1)
                {
                    ConfigurationShape shape = PresentationViewsSubject.GetPresentation(element)[0] as ConfigurationShape;

                    // Set the appropriate outline style depending on the inheritance modifier
                    switch (element.InheritanceModifier)
                    {
                        case InheritanceModifiers.None:
                            shape.OutlineDashStyle = DashStyle.Solid;
                            shape.OutlineThickness = 0.01F;
                            break;

                        case InheritanceModifiers.Abstract:
                            shape.OutlineDashStyle = DashStyle.Dash;
                            shape.OutlineThickness = 0.01F;
                            break;

                        case InheritanceModifiers.Sealed:
                            shape.OutlineDashStyle = DashStyle.Solid;
                            shape.OutlineThickness = 0.01F;
                            break;
                    }
                }

                #endregion

                // Always call the base class method.
                base.OnValueChanged(element, oldValue, newValue);
            }
        }

        #endregion
    }
}
