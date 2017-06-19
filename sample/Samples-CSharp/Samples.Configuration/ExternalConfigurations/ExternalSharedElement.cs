using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Configuration.ExternalConfigurations
{
    /// <summary>
    /// This would be the shared element located in another CSD file.
    /// </summary>
    public partial class ExternalSharedElement : global::System.Configuration.ConfigurationElement
    {

        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion

        #region Foo Property
        /// <summary>
        /// The XML name of the <see cref="Foo"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        internal const string FooPropertyName = "foo";

        /// <summary>
        /// Gets or sets the Foo.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        [global::System.ComponentModel.DescriptionAttribute("The Foo.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Samples.Configuration.ExternalConfigurations.ExternalSharedElement.FooPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual int Foo
        {
            get
            {
                return ((int)(base[global::Samples.Configuration.ExternalConfigurations.ExternalSharedElement.FooPropertyName]));
            }
            set
            {
                base[global::Samples.Configuration.ExternalConfigurations.ExternalSharedElement.FooPropertyName] = value;
            }
        }
        #endregion

        #region Bar Property
        /// <summary>
        /// The XML name of the <see cref="Bar"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        internal const string BarPropertyName = "bar";

        /// <summary>
        /// Gets or sets the Bar.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        [global::System.ComponentModel.DescriptionAttribute("The Bar.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Samples.Configuration.ExternalConfigurations.ExternalSharedElement.BarPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual string Bar
        {
            get
            {
                return ((string)(base[global::Samples.Configuration.ExternalConfigurations.ExternalSharedElement.BarPropertyName]));
            }
            set
            {
                base[global::Samples.Configuration.ExternalConfigurations.ExternalSharedElement.BarPropertyName] = value;
            }
        }
        #endregion
    }
}
