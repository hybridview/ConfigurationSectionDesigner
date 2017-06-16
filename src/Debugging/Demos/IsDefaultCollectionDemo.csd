<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="b75921e4-8e0b-42f9-9a07-b7468061d4b4" namespace="Debugging.Demos" xmlSchemaNamespace="urn:Debugging.Demos" assemblyName="Debugging.Demos" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="ConfigurationSection1" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="configurationSection1">
      <elementProperties>
        <elementProperty name="Demo1Items" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/b75921e4-8e0b-42f9-9a07-b7468061d4b4/Demo1Collection" />
          </type>
        </elementProperty>
        <elementProperty name="Demo2Items" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="demo2Items" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/b75921e4-8e0b-42f9-9a07-b7468061d4b4/Demo2Collection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="Demo1Collection" xmlItemName="demoElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/b75921e4-8e0b-42f9-9a07-b7468061d4b4/Demo1Element" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Demo1Element">
      <attributeProperties>
        <attributeProperty name="attribute1" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="attribute1" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b75921e4-8e0b-42f9-9a07-b7468061d4b4/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="attribute2" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="attribute2" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b75921e4-8e0b-42f9-9a07-b7468061d4b4/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="Demo2Collection" xmlItemName="demo2Element" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/b75921e4-8e0b-42f9-9a07-b7468061d4b4/Demo2Element" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Demo2Element">
      <attributeProperties>
        <attributeProperty name="attribute21" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="attribute21" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/b75921e4-8e0b-42f9-9a07-b7468061d4b4/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
  <comments>
    <configurationSectionModelHasComments Id="3f7d422c-8cab-4914-9e77-9de7d82fe970">
      <comment Id="ba726dc2-ffbd-4b5d-9ee9-5530334afb06" text="IsDefaultCollection=TRUE; Demo1Items will have no parent xml element called demo1Items. Multiple demo1Element elements can be added directly under the configuration section." />
    </configurationSectionModelHasComments>
    <configurationSectionModelHasComments Id="1c60eb0b-f64c-4974-b4dd-ee6a0a8b942b">
      <comment Id="2d101db1-6cbf-4a62-9d4d-119af80d85cb" text="IsDefaultCollection=FALSE; Demo2Items will have a collection of demo2Elements within a demo2Items parent element." />
    </configurationSectionModelHasComments>
  </comments>
</configurationSectionModel>