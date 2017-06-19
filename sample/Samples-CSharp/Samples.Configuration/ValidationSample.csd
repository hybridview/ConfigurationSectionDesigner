<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="407741f5-2106-41c5-8e25-354d4c4a51dd" namespace="Samples.Configuration.ValidationSample" xmlSchemaNamespace="urn:Samples.Configuration.ValidationSample" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="ValidationSampleSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="validationSampleSection">
      <attributeProperties>
        <attributeProperty name="PositiveNumber" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="positiveNumber" isReadOnly="false">
          <validator>
            <integerValidatorMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/PosIntegerValidator" />
          </validator>
          <type>
            <externalTypeMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="ZipCode" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="zipCode" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="AnythingButDots" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="anythingButDots" isReadOnly="false">
          <validator>
            <stringValidatorMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/NoDotsStringValidator" />
          </validator>
          <type>
            <externalTypeMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Foo" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="foo" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/Foo" />
          </type>
        </elementProperty>
        <elementProperty name="Bars" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="bars" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/Bars" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="Bars" xmlItemName="bar" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/Bar" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Bar">
      <attributeProperties>
        <attributeProperty name="Snap" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="snap" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Crackle" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="crackle" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/Single" />
          </type>
        </attributeProperty>
        <attributeProperty name="NoDots" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="noDots" isReadOnly="false">
          <validator>
            <stringValidatorMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/NoDotsStringValidator" />
          </validator>
          <type>
            <externalTypeMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="Foo">
      <attributeProperties>
        <attributeProperty name="Baz" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="baz" isReadOnly="false">
          <validator>
            <stringValidatorMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/NoDotsStringValidator" />
          </validator>
          <type>
            <externalTypeMoniker name="/407741f5-2106-41c5-8e25-354d4c4a51dd/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators>
      <integerValidator name="PosIntegerValidator" minValue="0" />
      <stringValidator name="NoDotsStringValidator" invalidCharacters="." />
      <regexStringValidator name="ZipCodeValidator" regularExpression="\d{5}" />
      <callbackValidator name="CustomValidators" callback="IsPrime" />
    </validators>
  </propertyValidators>
  <comments>
    <configurationSectionModelHasComments Id="bb32b196-1e98-4702-98e4-59593bb41efd">
      <comment Id="0b49c07e-662f-498d-93db-92740bc565b8" text="Validators only called on access of property. I thought spec of ConfigurationManager was to validate properties when section loaded?" />
    </configurationSectionModelHasComments>
  </comments>
</configurationSectionModel>