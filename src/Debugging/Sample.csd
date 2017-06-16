<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel dslVersion="1.0.0.0" Id="ab30db0b-dc8b-4ce7-8183-7764183593ef" namespace="Sample" xmlSchemaNamespace="urn:sample" assemblyName="Sample.Rocks" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
    <enumeratedType name="LetterFlags" namespace="Sample" isFlags="true">
      <literals>
        <enumerationLiteral name="A" value="1" />
        <enumerationLiteral name="B" value="2" />
        <enumerationLiteral name="C" value="4" />
        <enumerationLiteral name="D" value="8" />
      </literals>
    </enumeratedType>
    <externalType name="CustomType" namespace="Debugging" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="SampleConfigurationSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="sampleConfigurationSection">
      <attributeProperties>
        <attributeProperty name="Samples" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="samples" isReadOnly="false">
          <customAttributes />
          <type>
            <externalTypeMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Foo" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="foo" isReadOnly="false">
          <customAttributes />
          <type>
            <configurationElementMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/Foo" />
          </type>
        </elementProperty>
        <elementProperty name="Bars" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="bars" isReadOnly="false">
          <customAttributes />
          <type>
            <configurationElementCollectionMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/Bars" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="Foo">
      <attributeProperties>
        <attributeProperty name="Baz" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="baz" isReadOnly="false" typeConverter="Custom">
          <customAttributes />
          <customTypeConverter>
            <converterMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/CustomTypeTypeConverter" />
          </customTypeConverter>
          <type>
            <externalTypeMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/CustomType" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="Bars" xmlItemName="bar" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/Bar" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Bar">
      <attributeProperties>
        <attributeProperty name="Snap" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="snap" isReadOnly="false">
          <customAttributes />
          <type>
            <externalTypeMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Crackle" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="crackle" isReadOnly="false">
          <customAttributes />
          <type>
            <externalTypeMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/Single" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSectionGroup name="SampleConfigurationGroup">
      <configurationSectionProperties>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/SampleConfigurationSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
      </configurationSectionProperties>
    </configurationSectionGroup>
  </configurationElements>
  <propertyValidators>
    <validators>
      <stringValidator name="StringValidator1" />
    </validators>
  </propertyValidators>
  <customTypeConverters>
    <converter name="CustomTypeTypeConverter">
      <type>
        <externalTypeMoniker name="/ab30db0b-dc8b-4ce7-8183-7764183593ef/CustomType" />
      </type>
    </converter>
  </customTypeConverters>
</configurationSectionModel>