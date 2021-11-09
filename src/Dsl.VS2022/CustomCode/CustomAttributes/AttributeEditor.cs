using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using Microsoft.VisualStudio.Modeling.Design;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.DslDefinition;
using ClaAttributeParameter = Microsoft.VisualStudio.Modeling.DslDefinition.AttributeParameter;
using System.Reflection;

namespace ConfigurationSectionDesigner
{
    /// <summary>
    /// Used on the custom attributes ConfigurationProperty in order to
    /// provide a user interface for editing custom attributes.
    /// </summary>
    public class AttributeEditor : UITypeEditor
    {
        private readonly Type _clrAttributeFormType =
            Type.GetType("Microsoft.VisualStudio.Modeling.DslDefinition.Design.ClrAttributesForm, Microsoft.VisualStudio.Modeling.Sdk.DslDefinition.12.0", false);

        [SecurityPermission(SecurityAction.LinkDemand)]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            ElementPropertyDescriptor propertyDescriptor = context.PropertyDescriptor as ElementPropertyDescriptor;
            if ((propertyDescriptor != null) && ((propertyDescriptor.ModelElement is ConfigurationProperty)))
            {
                ConfigurationProperty configElement = propertyDescriptor.ModelElement as ConfigurationProperty;
                LinkedElementCollection<Attribute> attributes = configElement.Attributes;

                if (_clrAttributeFormType == null || !TryEditValueUsingNewEditor(configElement, attributes, provider))
                {
                    // Fall back to old attribute editor if something is not working properly
                    EditValueUsingOldEditor(configElement, attributes);
                }
            }

            return value;
        }

        // Kind of hack to use the cool built in, Microsoft provided attribute editor used in DSL
        private bool TryEditValueUsingNewEditor(ConfigurationProperty configElement, LinkedElementCollection<Attribute> attributes, IServiceProvider provider)
        {
            // 1) Create a fake store with correct metadata
            // 2) Map Attributes to ClrAttributes
            // 3) Invoke the new editor with clrAttributes collection (this is required because the editor operates on ClrAttribute type.... WTF)
            // 4) Map back (if necessary) the attributes from clrAttributes

            var store = new Store(typeof(DslDefinitionModelDomainModel));
            var clrAttributes = MapAttributesToClrAttributes(attributes, store);

            var attributeFormCtor = AttributeEditorFormConstructor;
            if (attributeFormCtor != null)
            {
                var form = attributeFormCtor.Invoke(new object[] { store, clrAttributes, provider }) as Form;
                if (form != null)
                {
                    DialogResult result;
                    using (form)
                    {
                        result = form.ShowDialog();
                    }

                    if (result == DialogResult.OK)
                    {
                        using (Transaction transaction = configElement.Store.TransactionManager.BeginTransaction("Edit custom attributes"))
                        {
                            MapClrAttributesToAttributes(clrAttributes, attributes, configElement.Store);

                            if (transaction.HasPendingChanges)
                            {
                                transaction.Commit();
                            }
                            else
                            {
                                transaction.Rollback();
                            }
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        private void MapClrAttributesToAttributes(LinkedElementCollection<ClrAttribute> clrAttributes, LinkedElementCollection<Attribute> attributes, Store store)
        {
            attributes.Clear();

            foreach (var clrAttribute in clrAttributes)
            {
                var attribute = new Attribute(store);
                attribute.Name = clrAttribute.Name;

                foreach (var clrAttributeParameter in clrAttribute.Parameters)
                {
                    var attributeParameter = new AttributeParameter(store);
                    attributeParameter.Name = clrAttributeParameter.Name;
                    attributeParameter.Value = clrAttributeParameter.Value;

                    attribute.Parameters.Add(attributeParameter);
                }

                attributes.Add(attribute);
            }
        }

        private ConstructorInfo AttributeEditorFormConstructor
        {
            get
            {
                return _clrAttributeFormType == null ? null : _clrAttributeFormType.GetConstructor(new Type[] { typeof(Store), typeof(LinkedElementCollection<ClrAttribute>), typeof(IServiceProvider) });
            }
        }

        private LinkedElementCollection<ClrAttribute> MapAttributesToClrAttributes(LinkedElementCollection<Attribute> attributes, Store store)
        {
            using (Transaction transaction = store.TransactionManager.BeginTransaction("Map attributes to ClrAttributes"))
            {
                var fakeProperty = new DomainProperty(store);
                var result = fakeProperty.Attributes;

                foreach (var attribute in attributes)
                {
                    var clrAttribute = new ClrAttribute(store);
                    clrAttribute.Name = attribute.Name;

                    foreach (var attributeParameter in attribute.Parameters)
                    {
                        var clrAttributeParameter = new ClaAttributeParameter(store);
                        clrAttributeParameter.Name = attributeParameter.Name;
                        clrAttributeParameter.Value = attributeParameter.Value;

                        clrAttribute.Parameters.Add(clrAttributeParameter);
                    }

                    result.Add(clrAttribute);
                }

                transaction.Commit();
                return result;
            }
        }

        private void EditValueUsingOldEditor(ConfigurationProperty configElement, LinkedElementCollection<Attribute> attributes)
        {
            using (Transaction transaction = configElement.Store.TransactionManager.BeginTransaction("Edit custom attributes"))
            {
                using (AttributesForm attributeForm = new AttributesForm(configElement.Store, attributes))
                {
                    if (attributeForm.ShowDialog() == DialogResult.OK && transaction.HasPendingChanges)
                        transaction.Commit();
                    else
                        transaction.Rollback();
                }
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand)]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
