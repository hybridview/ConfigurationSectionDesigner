using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace ConfigurationSectionDesigner
{
    public partial class ConfigurationSectionModelSerializer
    {
        private void CustomConstructor()
        {
            DefaultConstructor();
        }

        private string CustomXmlTagName { get { return DefaultXmlTagName; } }
        private string CustomMonikerTagName { get { return DefaultMonikerTagName; } }
        private string CustomMonikerAttributeName { get { return DefaultMonikerAttributeName; } }

        private void CustomRead( DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlReader reader )
        {
            DefaultRead( serializationContext, element, reader );
            
            ConfigurationSectionModel model = (ConfigurationSectionModel)element;

            // Make sure there's always a Validators instance in the model
            if( model.PropertyValidators == null )
            {
                model.PropertyValidators = new PropertyValidators( model.Store );
            }
        }

        private void CustomReadPropertiesFromAttributes( DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlReader reader )
        {
            DefaultReadPropertiesFromAttributes( serializationContext, element, reader );
        }

        private void CustomReadElements( DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlReader reader )
        {
            DefaultReadElements( serializationContext, element, reader );
        }

        private DslModeling::ModelElement CustomTryCreateInstance( DslModeling::SerializationContext serializationContext, global::System.Xml.XmlReader reader, DslModeling::Partition partition )
        {
            return DefaultTryCreateInstance( serializationContext, reader, partition );
        }

        private DslModeling::ModelElement CustomCreateInstance(DslModeling::SerializationContext serializationContext, global::System.Xml.XmlReader reader, DslModeling::Partition partition)
        {
            return DefaultCreateInstance( serializationContext, reader, partition );
        }

        private DslModeling::Moniker CustomTryCreateMonikerInstance( DslModeling::SerializationContext serializationContext, global::System.Xml.XmlReader reader, DslModeling::ModelElement sourceRolePlayer, global::System.Guid relDomainClassId, DslModeling::Partition partition )
        {
            return DefaultTryCreateMonikerInstance( serializationContext, reader, sourceRolePlayer, relDomainClassId, partition );
        }

        private DslModeling::Moniker CustomCreateMonikerInstance( DslModeling::SerializationContext serializationContext, global::System.Xml.XmlReader reader, DslModeling::ModelElement sourceRolePlayer, global::System.Guid relDomainClassId, DslModeling::Partition partition )
        {
            return DefaultCreateMonikerInstance( serializationContext, reader, sourceRolePlayer, relDomainClassId, partition );
        }

        private void CustomWriteMoniker( DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlWriter writer, DslModeling::ModelElement sourceRolePlayer, DslModeling::DomainRelationshipXmlSerializer relSerializer )
        {
            DefaultWriteMoniker( serializationContext, element, writer, sourceRolePlayer, relSerializer );
        }

        private void CustomWrite( DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlWriter writer, DslModeling::RootElementSettings rootElementSettings )
        {
            DefaultWrite( serializationContext, element, writer, rootElementSettings );
        }

        private  void CustomWritePropertiesAsAttributes( DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlWriter writer )
        {
            DefaultWritePropertiesAsAttributes( serializationContext, element, writer );
        }

        private  void CustomWriteElements( DslModeling::SerializationContext serializationContext, DslModeling::ModelElement element, global::System.Xml.XmlWriter writer )
        {
            DefaultWriteElements( serializationContext, element, writer );
        }

        private string CustomCalculateQualifiedName( DslModeling::DomainXmlSerializerDirectory directory, DslModeling::ModelElement element )
        {
            return DefaultCalculateQualifiedName( directory, element );
        }

        private string CustomGetMonikerQualifier( DslModeling::DomainXmlSerializerDirectory directory, DslModeling::ModelElement element )
        {
            return DefaultGetMonikerQualifier( directory, element );
        }
    }
}
