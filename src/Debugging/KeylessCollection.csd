<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="42122c52-2238-4a82-96dc-06ee95bfa543" namespace="Debugging" xmlSchemaNamespace="urn:Debugging" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="KeylessCollectionSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="keylessCollectionSection">
      <elementProperties>
        <elementProperty name="Collection" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="collection" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/42122c52-2238-4a82-96dc-06ee95bfa543/Collection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="Collection" xmlItemName="keylessElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods" hashElementKey="true">
      <itemType>
        <configurationElementMoniker name="/42122c52-2238-4a82-96dc-06ee95bfa543/KeylessElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="KeylessElement" />
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>