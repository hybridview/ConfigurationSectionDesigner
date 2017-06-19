
namespace Samples.Configuration.SharedElementsSample
{
    
    /// <summary>
    /// Once the feature allowing user to share elements is implemented, the generated class for this CSD diagram will look like this. 
    /// Instead of declaring the ExternalSharedElement class, it will reference the class generated from the shared CSD diagram.
    /// 
    /// External class is: Samples.Configuration.ExternalConfigurations.ExternalSharedElement
    /// </summary>
    public partial class SharedConsumerSection : global::System.Configuration.ConfigurationSection
    {
        
        #region Singleton Instance
        /// <summary>
        /// The XML name of the SharedConsumerSection Configuration Section.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        internal const string SharedConsumerSectionSectionName = "sharedConsumerSection";
        
        /// <summary>
        /// The XML path of the SharedConsumerSection Configuration Section.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        internal const string SharedConsumerSectionSectionPath = "sharedConsumerSection";
        
        /// <summary>
        /// Gets the SharedConsumerSection instance.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        public static global::Samples.Configuration.SharedElementsSample.SharedConsumerSection Instance
        {
            get
            {
                return ((global::Samples.Configuration.SharedElementsSample.SharedConsumerSection)(global::System.Configuration.ConfigurationManager.GetSection(global::Samples.Configuration.SharedElementsSample.SharedConsumerSection.SharedConsumerSectionSectionPath)));
            }
        }
        #endregion
        
        #region Xmlns Property
        /// <summary>
        /// The XML name of the <see cref="Xmlns"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        internal const string XmlnsPropertyName = "xmlns";
        
        /// <summary>
        /// Gets the XML namespace of this Configuration Section.
        /// </summary>
        /// <remarks>
        /// This property makes sure that if the configuration file contains the XML namespace,
        /// the parser doesn't throw an exception because it encounters the unknown "xmlns" attribute.
        /// </remarks>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Samples.Configuration.SharedElementsSample.SharedConsumerSection.XmlnsPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public string Xmlns
        {
            get
            {
                return ((string)(base[global::Samples.Configuration.SharedElementsSample.SharedConsumerSection.XmlnsPropertyName]));
            }
        }
        #endregion
        
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
        
        #region MyElement Property
        /// <summary>
        /// The XML name of the <see cref="MyElement"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        internal const string MyElementPropertyName = "myElement";
        
        /// <summary>
        /// Gets or sets the MyElement.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.1.7")]
        [global::System.ComponentModel.DescriptionAttribute("The MyElement.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Samples.Configuration.SharedElementsSample.SharedConsumerSection.MyElementPropertyName, IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public virtual global::Samples.Configuration.ExternalConfigurations.ExternalSharedElement MyElement
        {
            get
            {
                return ((global::Samples.Configuration.ExternalConfigurations.ExternalSharedElement)(base[global::Samples.Configuration.SharedElementsSample.SharedConsumerSection.MyElementPropertyName]));
            }
            set
            {
                base[global::Samples.Configuration.SharedElementsSample.SharedConsumerSection.MyElementPropertyName] = value;
            }
        }
        #endregion
    }
}

