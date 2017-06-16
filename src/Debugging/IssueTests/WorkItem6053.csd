<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2" namespace="Debugging.IssueTests" xmlSchemaNamespace="urn:Debugging.IssueTests" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSectionGroup name="OuterSectionGroup">
      <configurationSectionProperties>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/InnerConfigurationSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
      </configurationSectionProperties>
      <configurationSectionGroupProperties>
        <configurationSectionGroupProperty>
          <containedConfigurationSectionGroup>
            <configurationSectionGroupMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/ConfigurationSectionGroup1" />
          </containedConfigurationSectionGroup>
        </configurationSectionGroupProperty>
      </configurationSectionGroupProperties>
    </configurationSectionGroup>
    <configurationSection name="InnerConfigurationSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="innerConfigurationSection">
      <attributeProperties>
        <attributeProperty name="TestAttribute1" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="testAttribute1" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationSection>
    <configurationSectionGroup name="ConfigurationSectionGroup1">
      <configurationSectionProperties>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/ConfigurationSection1" />
          </containedConfigurationSection>
        </configurationSectionProperty>
      </configurationSectionProperties>
    </configurationSectionGroup>
    <configurationSection name="ConfigurationSection1" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="configurationSection1">
      <attributeProperties>
        <attributeProperty name="TestAttribute2" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="testAttribute2" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationSection>
    <configurationSection name="EmailConfigSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="emailConfigSection">
      <elementProperties>
        <elementProperty name="Emails" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="emails" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/EmailCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="EmailCollection" xmlItemName="email" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods, ICollection">
      <itemType>
        <configurationElementMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/Email" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Email">
      <attributeProperties>
        <attributeProperty name="enabled" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="enabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/7b8daa9d-fd7e-4040-b9dc-4cebbaf67ce2/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
  <comments>
    <configurationSectionModelHasComments Id="8e923b06-9070-4015-a9d1-c7015b6cf789">
      <comment Id="5478dbfe-b3b0-48f8-b686-61895b6acead" text="Attempting to reproduce null instance bug. No success yet..." />
    </configurationSectionModelHasComments>
  </comments>
</configurationSectionModel>