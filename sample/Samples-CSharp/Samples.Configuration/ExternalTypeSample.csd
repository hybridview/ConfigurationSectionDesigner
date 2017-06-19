<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="dd7352e1-27e2-4af2-88dd-b961e6edc89b" namespace="Samples.Configuration.ExternalTypeSample" xmlSchemaNamespace="urn:Samples.Configuration.ExternalTypeSample" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
    <externalType name="Rectangle" namespace="Samples.Configuration.ExternalTypes" />
    <externalType name="CustomBlock" namespace="Samples.Configuration.ExternalTypes" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="ExternalTypesComputerDemoSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="externalTypesComputerDemoSection">
      <elementProperties>
        <elementProperty name="computerPackage" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="computerPackage" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/ComputerPackageElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="ComputerPackageElement">
      <attributeProperties>
        <attributeProperty name="DesktopBox" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="desktopBox" isReadOnly="false" typeConverter="Custom">
          <customTypeConverter>
            <converterMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/CustomBlockTypeConverter" />
          </customTypeConverter>
          <type>
            <externalTypeMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/CustomBlock" />
          </type>
        </attributeProperty>
        <attributeProperty name="AccessoriesBox" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="accessoriesBox" isReadOnly="false" typeConverter="Custom">
          <customTypeConverter>
            <converterMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/CustomBlockTypeConverter" />
          </customTypeConverter>
          <type>
            <externalTypeMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/CustomBlock" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="ExternalTypesFooDemoSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="externalTypesFooDemoSection">
      <attributeProperties>
        <attributeProperty name="Samples" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="samples" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Foo" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="foo" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/Foo" />
          </type>
        </elementProperty>
        <elementProperty name="Bars" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="bars" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/Bars" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="Bars" xmlItemName="bar" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/Bar" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Bar">
      <attributeProperties>
        <attributeProperty name="Snap" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="snap" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Crackle" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="crackle" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/Single" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="Foo">
      <attributeProperties>
        <attributeProperty name="Baz" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="baz" isReadOnly="false" typeConverter="Custom">
          <customTypeConverter>
            <converterMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/CustomBlockTypeConverter" />
          </customTypeConverter>
          <type>
            <externalTypeMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/CustomBlock" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSectionGroup name="ExternalTypesFooDemoGroup">
      <configurationSectionProperties>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/ExternalTypesFooDemoSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
      </configurationSectionProperties>
    </configurationSectionGroup>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
  <customTypeConverters>
    <converter name="CustomBlockTypeConverter">
      <type>
        <externalTypeMoniker name="/dd7352e1-27e2-4af2-88dd-b961e6edc89b/CustomBlock" />
      </type>
    </converter>
  </customTypeConverters>
</configurationSectionModel>