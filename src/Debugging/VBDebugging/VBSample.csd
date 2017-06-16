<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel dslVersion="1.0.0.0" Id="5bfcd33f-6f41-45f4-91b9-6744a999b715" namespace="VBSample" xmlSchemaNamespace="urn:vbsample" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="VBSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="vBSection">
      <baseClass>
        <configurationSectionMoniker name="/5bfcd33f-6f41-45f4-91b9-6744a999b715/VBSubSection" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="DotNet" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="dotNet" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/5bfcd33f-6f41-45f4-91b9-6744a999b715/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="VBElement" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="vBElement" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/5bfcd33f-6f41-45f4-91b9-6744a999b715/VBElement" />
          </type>
        </elementProperty>
        <elementProperty name="VBCollection" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="vBCollection" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/5bfcd33f-6f41-45f4-91b9-6744a999b715/VBCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="VBElement" inheritanceModifier="Sealed">
      <baseClass>
        <configurationElementMoniker name="/5bfcd33f-6f41-45f4-91b9-6744a999b715/Schnoll" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="Angular" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="angular" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/5bfcd33f-6f41-45f4-91b9-6744a999b715/Double" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="VBCollection" xmlItemName="vBElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/5bfcd33f-6f41-45f4-91b9-6744a999b715/VBElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Schnoll" inheritanceModifier="Abstract">
      <attributeProperties>
        <attributeProperty name="Scholl" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="scholl" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/5bfcd33f-6f41-45f4-91b9-6744a999b715/DateTime" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="VBSubSection" codeGenOptions="Singleton" xmlSectionName="vBSubSection" />
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>