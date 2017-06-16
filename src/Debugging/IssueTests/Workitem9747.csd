<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="601d4b5c-5560-462d-884f-88037d62ff1b" namespace="Debugging.IssueTests" xmlSchemaNamespace="urn:Debugging.IssueTests" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationElement name="MailMessage">
      <attributeProperties>
        <attributeProperty name="Subject" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="subject" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="AddRemoveClearMapRecipients" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="addRemoveClearMapRecipients" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/AddRemoveClearMapRecipients" />
          </type>
        </elementProperty>
        <elementProperty name="AddRemoveClearMapAlternateRecipients" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="addRemoveClearMapAlternateRecipients" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/AddRemoveClearMapAlternateRecipients" />
          </type>
        </elementProperty>
        <elementProperty name="BasicMapRecipients" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="basicMapRecipients" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/BasicMapRecipients" />
          </type>
        </elementProperty>
        <elementProperty name="BasicMapAlternateRecipients" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="basicMapAlternateRecipients" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/BasicMapAlternateRecipients" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="Reciepient">
      <attributeProperties>
        <attributeProperty name="Address" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="address" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="AddRemoveClearMapRecipients" collectionType="AddRemoveClearMap" xmlItemName="reciepient" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/Reciepient" />
      </itemType>
    </configurationElementCollection>
    <configurationSection name="MailSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="mailSection">
      <elementProperties>
        <elementProperty name="Mail" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="mail" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/MailMessage" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="AddRemoveClearMapAlternateRecipients" collectionType="AddRemoveClearMapAlternate" xmlItemName="reciepient" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/Reciepient" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="BasicMapAlternateRecipients" xmlItemName="reciepient" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/Reciepient" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="BasicMapRecipients" collectionType="BasicMap" xmlItemName="reciepient" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/601d4b5c-5560-462d-884f-88037d62ff1b/Reciepient" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>