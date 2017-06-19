<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="209884a0-1637-48d5-90e1-3d8d8f72b0e9" namespace="Samples.Configuration.SimpleSample" xmlSchemaNamespace="urn:Samples.Configuration.SimpleSample" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="SimpleSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="simpleSection">
      <elementProperties>
        <elementProperty name="Simple" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="simple" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/209884a0-1637-48d5-90e1-3d8d8f72b0e9/SimpleElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="SimpleElement">
      <attributeProperties>
        <attributeProperty name="Foo" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="foo" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/209884a0-1637-48d5-90e1-3d8d8f72b0e9/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Bar" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="bar" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/209884a0-1637-48d5-90e1-3d8d8f72b0e9/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>