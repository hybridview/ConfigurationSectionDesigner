<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="7e61a27f-b52c-4714-a781-b4ecd57048f8" namespace="Samples.UI.ApplicationConfig" xmlSchemaNamespace="urn:Samples.UI.ApplicationConfig" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="SamplesSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="samplesSection">
      <elementProperties>
        <elementProperty name="SampleGroups" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="sampleGroups" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/SampleGroupList" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="SampleGroup">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Label" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="label" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Samples" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="samples" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/SampleList" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="SampleGroupList" xmlItemName="sampleGroup" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/SampleGroup" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="SampleList" xmlItemName="sample" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/Sample" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Sample">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Label" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="label" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="FileName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="fileName" isReadOnly="false" documentation="Leave blank if this sample is generated on the fly (temporary).">
          <type>
            <externalTypeMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Description" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="description" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsValid" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isValid" isReadOnly="false" documentation="Indicates whether this sample should be demonstrating an error condition." defaultValue="&quot;true&quot;">
          <type>
            <externalTypeMoniker name="/7e61a27f-b52c-4714-a781-b4ecd57048f8/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>