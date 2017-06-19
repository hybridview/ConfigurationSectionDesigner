<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="9cbc31e5-7cb8-45a5-8086-1f0e2ee8c189" namespace="Samples.Configuration.SharedElementsSample" xmlSchemaNamespace="urn:Samples.Configuration.SharedElementsSample" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="SharedConsumerSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="sharedConsumerSection">
      <elementProperties>
        <elementProperty name="MyElement" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="myElement" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/9cbc31e5-7cb8-45a5-8086-1f0e2ee8c189/ExternalSharedElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="ExternalSharedElement" documentation="This would be the shared element located in another CSD file.">
      <attributeProperties>
        <attributeProperty name="Foo" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="foo" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/9cbc31e5-7cb8-45a5-8086-1f0e2ee8c189/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="Bar" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="bar" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/9cbc31e5-7cb8-45a5-8086-1f0e2ee8c189/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
  <comments>
    <configurationSectionModelHasComments Id="8518579b-3208-4258-86ff-a3e6355e1218">
      <comment Id="99285d52-3331-4a7a-b74a-874b4f65b151" text="This is a concept only, and has not yet been implemented." />
    </configurationSectionModelHasComments>
    <configurationSectionModelHasComments Id="7c4512ad-502e-40a3-8551-03f0c642cf8f">
      <comment Id="d0d51a26-d773-443c-91b1-2d52cea8b91e" text="Once this feature has been implemented, this element here will be located in another CSD file, but will be accessible to this CSD diagram.">
        <subjects>
          <commentsReferenceConfigurationItems Id="77b2d32f-b9f4-4e92-8e3b-418a820f4ab2">
            <configurationElementMoniker name="/9cbc31e5-7cb8-45a5-8086-1f0e2ee8c189/ExternalSharedElement" />
          </commentsReferenceConfigurationItems>
        </subjects>
      </comment>
    </configurationSectionModelHasComments>
    <configurationSectionModelHasComments Id="764af685-d825-4325-ba1a-210d7eb4b7f4">
      <comment Id="e64bfb11-0050-45c9-939c-0720a22419f6" text="To see an example of generated code that this feature might produce, see file SharedElementsSample.CONCEPT.cs" />
    </configurationSectionModelHasComments>
  </comments>
</configurationSectionModel>