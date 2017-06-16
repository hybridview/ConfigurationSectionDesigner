<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="5a49b528-9c4b-4130-9093-0fb5bdef29ee" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionDesigner" Name="ConfigurationSectionDesigner" DisplayName="ConfigurationSectionDesigner" Namespace="ConfigurationSectionDesigner" ProductName="Configuration Section Designer" CompanyName="Microsoft PLK" PackageGuid="9f16956e-2232-4f80-9ebb-4165929ef3ef" PackageNamespace="ConfigurationSectionDesigner" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="a923fcd2-2d2e-4bd5-ac9f-296eb48b3a12" Description="The configuration section being designed, which contains all configuration elements." Name="ConfigurationSectionModel" DisplayName="Configuration Section Model" Namespace="ConfigurationSectionDesigner" HasCustomConstructor="true">
      <Properties>
        <DomainProperty Id="472be10d-b8f9-46c0-8c17-49ca59fa266a" Description="The root namespace used for code generation." Name="Namespace" DisplayName="Namespace" Category="Code">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a9b8787d-43f1-4989-b1da-649140895582" Description="The root XML namespace for XML Schema (XSD) generation." Name="XmlSchemaNamespace" DisplayName="Xml Schema Namespace" Category="XML">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="TypeDefinition" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationSectionModelHasTypeDefinitions.TypeDefinitions</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="BaseConfigurationType" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationSectionModelHasConfigurationElements.ConfigurationElements</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="PropertyValidators" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationSectionModelHasPropertyValidators.PropertyValidators</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="CustomTypeConverter" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationSectionModelHasCustomTypeConverters.CustomTypeConverters</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Comment" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationSectionModelHasComments.Comments</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="82c927d7-0b4a-43e8-8157-463859ba944b" Description="Description for ConfigurationSectionDesigner.ConfigurationElement" Name="ConfigurationElement" DisplayName="Configuration Element" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="BaseConfigurationType" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="17f06e52-3480-4df2-89d9-f4f2bbac7817" Description="Provides documentation notes on this Configuration Element." Name="Documentation" DisplayName="Documentation" Category="Documentation">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="aa25d7d3-4d0e-46d5-a040-cb9698ce242e" Description="Determines if this configuration element has custom child elements that are not known at design-time. WARNING: Setting this to true will disable intellisense to the ENTIRE configuration file because the resulting schema will become ambiguous." Name="HasCustomChildElements" DisplayName="Has Custom Child Elements" DefaultValue="false" Category="Code">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a66fa5f7-3682-4f69-beae-389c137760a1" Description="Determines whether the configuration element can be modified or not." Name="IsReadOnly" DisplayName="Is Read Only" DefaultValue="false" Category="Code">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9c1f3dc0-662f-4ba3-90d3-5746d7cbb983" Description="Specifies the display name for this element" Name="DisplayName" DisplayName="Display Name" Category="Metadata">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="AttributeProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationElementHasAttributeProperties.AttributeProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ElementProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationElementHasElementProperties.ElementProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="efff5100-9242-483b-8788-acb3c0690c30" Description="Description for ConfigurationSectionDesigner.AttributeProperty" Name="AttributeProperty" DisplayName="Attribute" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ConfigurationProperty" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="4a2024cd-f112-4678-9047-5ecbe3753f56" Description="The default value of this property." Name="DefaultValue" DisplayName="Default Value" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="39303ae2-7f95-42dd-9137-c0bc09c32901" Description="Use a custom editor to edit this attribute" Name="CustomEditor" DisplayName="Custom Editor" DefaultValue="None" Category="Metadata">
          <Type>
            <DomainEnumerationMoniker Name="CustomEditors" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="d298038b-5200-4aeb-a6a2-d91e6aa2ba90" Description="Description for ConfigurationSectionDesigner.ConfigurationSection" Name="ConfigurationSection" DisplayName="Configuration Section" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ConfigurationElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="d73f86af-29ec-42bd-9a44-40a148c6ec3a" Description="The possible blocks of code that are generated for a Configuration Section." Name="CodeGenOptions" DisplayName="Code Generation Options" DefaultValue="Singleton|XmlnsProperty" Category="Code">
          <Type>
            <DomainEnumerationMoniker Name="ConfigurationSectionCodeGenOptions" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7430c978-4ebf-41ee-b1d5-65fe8e5b8030" Description="The name of this Configuration Section as it appears in XML." Name="XmlSectionName" DisplayName="Xml Section Name" Category="XML">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f69dcbb6-d525-445f-a057-dc0c092884aa" Description="Decides which section protection provider to protect this configuration section with" Name="ProtectionProvider" DisplayName="Protection Provider" DefaultValue="RSAProtectedConfigurationProvider" Category="Protection">
          <Type>
            <DomainEnumerationMoniker Name="ProtectionProviders" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="81ec3bc7-323b-4fb2-af8c-8d16e9e9de6d" Description="If &quot;Protection Provider&quot; is set to Custom, this property decides which custom provider to use" Name="CustomProtectionProvider" DisplayName="Custom Protection Provider" Category="Protection">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="a7457ba6-e6fd-440f-b940-c6ff94b181df" Description="Description for ConfigurationSectionDesigner.ConfigurationElementCollection" Name="ConfigurationElementCollection" DisplayName="Configuration Element Collection" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ConfigurationElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="43dc46ec-a850-4e6c-a131-112ecaaa5683" Description="The type of this collection." Name="CollectionType" DisplayName="Collection Type" DefaultValue="BasicMapAlternate" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System.Configuration/ConfigurationElementCollectionType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="189ca66a-11e8-4aa9-8270-f5d82664c0b7" Description="The name of each item in the collection as it appears in XML." Name="XmlItemName" DisplayName="Xml Item Name" Category="XML">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="814d95d9-a63e-4f30-8125-fa9f783a1898" Description="The possible blocks of code that are generated for a Configuration Element Collection." Name="CodeGenOptions" DisplayName="Code Generation Options" DefaultValue="Indexer|AddMethod|RemoveMethod|GetItemMethods" Category="Code">
          <Type>
            <DomainEnumerationMoniker Name="ConfigurationElementCollectionCodeGenOptions" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="69f09ff9-d8ff-4ade-aaf6-ccfa0c438cd9" Description="The name of the 'add' configuration element. This value is only used if CollectionType is AddRemoveClearMap or AddRemoveClearMapAlternate." Name="AddItemName" DisplayName="Add Item Name" DefaultValue="add" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b47481a3-4ac1-4502-becc-f52ad66cefef" Description="The name of the 'remove' configuration element. This value is only used if CollectionType is AddRemoveClearMap or AddRemoveClearMapAlternate." Name="RemoveItemName" DisplayName="Remove Item Name" DefaultValue="remove" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7c2d861e-cc41-4970-9c48-42cbdeae2225" Description="The name for the 'clear' configuration element. This value is only used if CollectionType is AddRemoveClearMap or AddRemoveClearMapAlternate." Name="ClearItemsName" DisplayName="Clear Items Name" DefaultValue="clear" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="543af28a-a969-476d-a934-b8c596427cd4" Description="If set to true, allow the ItemType element to be keyless, uses GetHashCode on the elemt itself." Name="UseHashForElementKey" DisplayName="Use Hash For Element Key" DefaultValue="false" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="8faa6746-c23e-4bcd-8c92-14ea26fa03da" Description="Description for ConfigurationSectionDesigner.ElementProperty" Name="ElementProperty" DisplayName="Element" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="ConfigurationProperty" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="37d3b10e-35a5-4608-8bf5-5556ad9d3fe9" Description="Description for ConfigurationSectionDesigner.ConfigurationProperty" Name="ConfigurationProperty" DisplayName="Configuration Property" InheritanceModifier="Abstract" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="8bbb2285-fa6e-44d4-9ce7-e491a0286101" Description="The name of this property." Name="Name" DisplayName="Name" Category="Definition" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1c7c520e-fb0b-4718-b5d8-3186baca908e" Description="Determines if this is a required property." Name="IsRequired" DisplayName="Is Required" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="21f0455b-950a-47eb-9d2f-7cc8b545bd71" Description="Determines if this property is the key for the element in which it appears." Name="IsKey" DisplayName="Is Key" DefaultValue="False" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5c09c892-7be1-466a-b782-3f7a388bf029" Description="Determines if this is the default property collection. NOTE: If false, the .NET framework will build a nested section like '&lt;items&gt; ...&lt;/items&gt;' to hold the collection. This property will be ignored if element is not a collections." Name="IsDefaultCollection" DisplayName="Is Default Collection" Category="Definition">
          <Notes>If false, the .NET framework will build a nested section like '&lt;items&gt; ...&lt;/items&gt;'. This property will be ignored for non-collections.</Notes>
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="41c30859-fc52-430a-bd7b-ea0056db7b9e" Description="The name of this property as it appears in XML." Name="XmlName" DisplayName="Xml Name" DefaultValue=" " Category="XML">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3ccc2359-840a-4a5c-8528-2ca1c08bd7d3" Description="Determines if this property has only a getter or also a setter." Name="IsReadOnly" DisplayName="Is Read Only" DefaultValue="False" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7fd60b77-80a0-401d-b314-e57b319b9e58" Description="Provides documentation notes on this property." Name="Documentation" DisplayName="Documentation" Category="Documentation">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="09023d9a-8496-4fa2-8c9e-92e496753669" Description="Adds custom Attributes to the property. These will be applied to the property in the code behind file as VS attributes. Make sure the custom attributes declared here are defined." Name="CustomAttributes" DisplayName="Custom Attributes" Kind="Calculated" Category="Metadata" IsUIReadOnly="true">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.DesignerSerializationVisibility">
              <Parameters>
                <AttributeParameter Value="System.ComponentModel.DesignerSerializationVisibility.Content" />
              </Parameters>
            </ClrAttribute>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof( ConfigurationSectionDesigner.AttributeEditor )" />
                <AttributeParameter Value="typeof( System.Drawing.Design.UITypeEditor )" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="423997b8-e502-4df9-a128-6dbc26a26c48" Description="Whether a property should be displayed" Name="Browsable" DisplayName="Browsable" DefaultValue="true" Category="Metadata">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ccc0b3a9-e394-409d-9bf9-bcc90b4e9d97" Description="Specifies the display name for a property" Name="DisplayName" DisplayName="Display Name" Category="Metadata">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c262d286-0a65-4903-b276-89b2ede6d14c" Description="The name of the category in which to group the property" Name="Category" DisplayName="Category" Category="Metadata">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="147ba091-bc2a-4bce-a6db-aa7f00205cf6" Description="The typeconverter to use for this property" Name="TypeConverter" DisplayName="Type Converter" DefaultValue="None" Category="Code">
          <Type>
            <DomainEnumerationMoniker Name="TypeConverters" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Attribute" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationPropertyHasAttributes.Attributes</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="c03f81ec-babe-4771-8886-ce2a697f1831" Description="Description for ConfigurationSectionDesigner.TypeDefinition" Name="TypeDefinition" DisplayName="Type Definition" InheritanceModifier="Abstract" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="TypeBase" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="1e95de2f-c311-4845-b897-eccbd1d0489c" Description="Description for ConfigurationSectionDesigner.ExternalType" Name="ExternalType" DisplayName="External Type" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="TypeDefinition" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="0dc675b4-e762-431b-a967-858fc923164b" Description="Description for ConfigurationSectionDesigner.EnumeratedType" Name="EnumeratedType" DisplayName="Enumerated Type" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="TypeDefinition" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="63e72b7e-fd20-4bd3-a480-a85325de3b1e" Description="Determines if this type is a flags enumeration." Name="IsFlags" DisplayName="Is Flags" DefaultValue="false" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e42e806a-8d97-44f1-8dfe-f154b00f816c" Description="Provides documentation notes on this type definition." Name="Documentation" DisplayName="Documentation" Category="Documentation">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9d1f6ae2-9eb7-4cfa-b869-cdb84c861e3e" Description="Defines if code for the enumeration is generated." Name="CodeGenOptions" DisplayName="Code Generation Options" DefaultValue="TypeDefinition" Category="Code">
          <Type>
            <DomainEnumerationMoniker Name="TypeDefinitionCodeGenOptions" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="EnumerationLiteral" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>EnumeratedTypeHasLiterals.Literals</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="43dcc559-12e4-42be-9fc6-770a07480377" Description="Description for ConfigurationSectionDesigner.EnumerationLiteral" Name="EnumerationLiteral" DisplayName="Enumeration Literal" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="03447a00-87e1-4d93-91c8-c4026386a757" Description="The name of the enumeration literal." Name="Name" DisplayName="Name" Category="Definition" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a47f00b1-ebcd-482b-b8b1-fcf6d6d66126" Description="The numeric value of the enumeration literal (may be null)." Name="Value" DisplayName="Value" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="aa6e7b02-754b-428b-9c16-7652c9a6c110" Description="Provides documentation notes on this enumeration literal." Name="Documentation" DisplayName="Documentation" Category="Documentation">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="63af814b-6a3c-4043-a197-7859295f34d0" Description="Description for ConfigurationSectionDesigner.BaseConfigurationType" Name="BaseConfigurationType" DisplayName="Base Configuration Type" InheritanceModifier="Abstract" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="TypeBase" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="c9f30476-3f09-45f5-bd35-1ba844194cfe" Description="The actual type name of this Configuration Element." Name="TypeName" DisplayName="Type Name" Kind="Calculated" Category="Definition" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2a1bb1bb-73fe-4a36-be74-3e0d7d51d9da" Description="Determines if the inheritance modifier of the generated class is none, abstract or sealed" Name="InheritanceModifier" DisplayName="Inheritance Modifier" DefaultValue="None" Category="Code">
          <Type>
            <DomainEnumerationMoniker Name="InheritanceModifiers" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="00041019-a117-4bf5-b5dc-190a1ee7a4d1" Description="Determines what the access modifier for the generated class is." Name="AccessModifier" DisplayName="Access Modifier" DefaultValue="Public" Category="Code">
          <Type>
            <DomainEnumerationMoniker Name="AccessModifiers" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="d475254d-0763-4840-aee9-f64e7388dec6" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroup" Name="ConfigurationSectionGroup" DisplayName="Configuration Section Group" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="BaseConfigurationType" />
      </BaseClass>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ConfigurationSectionProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationSectionGroupHasConfigurationSectionProperties.ConfigurationSectionProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ConfigurationSectionGroupProperty" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConfigurationSectionGroupHasConfigurationSectionGroupProperties.ConfigurationSectionGroupProperties</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="29e0dc85-a1b6-40ee-8d5e-297e31619245" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionProperty" Name="ConfigurationSectionProperty" DisplayName="Configuration Section Property" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="f755f792-28ba-40f4-8501-f8c54767faad" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionProperty.Configuration Section Name" Name="ConfigurationSectionName" DisplayName="Configuration Section Name" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="eb5ac774-3003-4615-8158-f0f6e6b5133d" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupProperty" Name="ConfigurationSectionGroupProperty" DisplayName="Configuration Section Group Property" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="815800d0-413c-4861-9add-7b3508450de2" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupProperty.Configuration Section Group Name" Name="ConfigurationSectionGroupName" DisplayName="Configuration Section Group Name" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="14a6d386-5e9e-42d3-a6cc-335ab70d14cf" Description="Description for ConfigurationSectionDesigner.PropertyValidators" Name="PropertyValidators" DisplayName="Validators" Namespace="ConfigurationSectionDesigner">
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="PropertyValidator" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>PropertyValidatorsHasValidators.Validators</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="27282ca0-27e0-4bb3-86d8-f534f0f35173" Description="Description for ConfigurationSectionDesigner.PropertyValidator" Name="PropertyValidator" DisplayName="Property Validator" InheritanceModifier="Abstract" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="986fecd5-8de3-4085-85bc-7faf5eb46941" Description="The validator's name" Name="Name" DisplayName="Name" Category="Definition" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="78185a4a-fbfc-453e-a0cb-b88efc0dfd8f" Description="Provides dynamic validation of an object by calling a method to validate it." Name="CallbackValidator" DisplayName="Callback Validator" InheritanceModifier="Sealed" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="PropertyValidator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="9c9c90ef-595f-44bd-a311-b6789c607c89" Description="The method to call for validation." Name="Callback" DisplayName="Callback" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="a4446ac7-f21f-45e4-ba96-5d32a7882695" Description="Description for ConfigurationSectionDesigner.NumberValidator" Name="NumberValidator" DisplayName="Number Validator" InheritanceModifier="Abstract" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="PropertyValidator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="bf8817ed-a49c-4b34-b69e-f6857f7c1ac2" Description="Indicates whether to include or exclude the range defined by the MinValue and MaxValue property values." Name="ExcludeRange" DisplayName="Exclude Range" DefaultValue="false" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="83c56d8d-2749-4edc-bb83-c447b784134c" Description="Validates properties of Int32 type." Name="IntegerValidator" DisplayName="Integer Validator" InheritanceModifier="Sealed" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="NumberValidator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="f43c8b67-86c7-4d75-bf62-f306b40b7498" Description="The maximum value allowed for the property." Name="MaxValue" DisplayName="Max Value" DefaultValue="2147483647" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7dd1e02b-8f45-41e9-9c50-9250cd04e284" Description="The minimum value allowed for the property." Name="MinValue" DisplayName="Min Value" DefaultValue="-2147483648" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="615db1fd-b687-4686-a6d3-a5b16b94bb60" Description="Validates properties of Int64 type." Name="LongValidator" DisplayName="Long Validator" InheritanceModifier="Sealed" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="NumberValidator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="6c5d698f-87f1-4149-a5c8-c6d5f54c4ded" Description="The maximum value allowed for the property." Name="MaxValue" DisplayName="Max Value" DefaultValue="" Kind="CustomStorage" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/Int64" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="628ab3c6-3c34-4fa8-ac5f-fe01713f5f67" Description="The minimum value allowed for the property." Name="MinValue" DisplayName="Min Value" DefaultValue="" Kind="CustomStorage" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/Int64" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="1f887b1f-0e17-4ed0-b52d-27fb598c506e" Description="Validates properties of TimeSpan type." Name="PositiveTimeSpanValidator" DisplayName="Positive Time Span Validator" InheritanceModifier="Sealed" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="PropertyValidator" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="2941db5b-589b-48a4-9499-f9aabc27655e" Description="Validates properties of TimeSpan type." Name="TimeSpanValidator" DisplayName="Time Span Validator" InheritanceModifier="Sealed" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="NumberValidator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="41156c42-d51b-48b5-ad93-bd58a417b215" Description="The maximum value allowed for the property." Name="MaxValue" DisplayName="Max Value" Kind="CustomStorage" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/TimeSpan" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5d5b0174-1e11-42e8-a0a0-3aa0b52cbc5d" Description="The minimum value allowed for the property." Name="MinValue" DisplayName="Min Value" Kind="CustomStorage" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/TimeSpan" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="bfa722b6-1963-4535-a9d1-02c33c4fd511" Description="Uses a regular expression to validate properties of the String type." Name="RegexStringValidator" DisplayName="RegexString Validator" InheritanceModifier="Sealed" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="PropertyValidator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="6d94df04-4a7d-4292-acd4-23f5369cbc65" Description="The regular expression used to filter the string assigned to the decorated configuration-element property." Name="RegularExpression" DisplayName="Regular Expression" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="891b7058-07cd-44fb-827f-259fd8877933" Description="Validates properties of String type." Name="StringValidator" DisplayName="String Validator" InheritanceModifier="Sealed" Namespace="ConfigurationSectionDesigner">
      <BaseClass>
        <DomainClassMoniker Name="PropertyValidator" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="af99c1e9-1235-4ff5-a056-3169b5bbbd49" Description="The set of characters that are not allowed for the property." Name="InvalidCharacters" DisplayName="Invalid Characters" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="181ee7ea-03c5-40c2-9144-1df657fcf8b0" Description="The maximum length allowed for the string to assign to the property." Name="MaxLength" DisplayName="Max Length" DefaultValue="2147483647" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="be010dfa-e04c-41d5-957e-6035d8496931" Description="The minimum allowed value for the string to assign to the property." Name="MinLength" DisplayName="Min Length" DefaultValue="0" Category="Validation">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="cdff1d9b-f69c-4481-861a-5e8be81add1e" Description="Description for ConfigurationSectionDesigner.CustomTypeConverter" Name="CustomTypeConverter" DisplayName="Custom Type Converter" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="f7f5f949-fbca-4404-ac9f-d13a253f7060" Description="The name of the custom converter" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1d21954a-f4b2-41d0-a8aa-2538159deb56" Description="Provides documentation notes on this custom converter." Name="Documentation" DisplayName="Documentation" Category="Documentation">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="d0cc5f43-1fd5-4f30-a187-3579f1d5e1f2" Description="Description for ConfigurationSectionDesigner.TypeBase" Name="TypeBase" DisplayName="Type Base" InheritanceModifier="Abstract" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="46f85fa9-c1e4-4c99-a5d2-a163c90d92f5" Description="The name of the type" Name="Name" DisplayName="Name" Category="Definition" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="31f9cdbb-80b7-45f4-bbfc-5a658cd81374" Description="The namespace of this type." Name="Namespace" DisplayName="Namespace" Category="Definition">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="aec58765-d7d4-469e-a827-af42c6bedc58" Description="Represents an attribute." Name="Attribute" DisplayName="Attribute" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="128e700e-48fe-459a-95e2-7035899998e7" Description="The name of the attribute. This should be a Fully Qualified Name." Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="AttributeParameter" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>AttributeHasParameters.Parameters</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="53b3f479-ace4-439a-bb57-bb23906813e6" Description="Description for ConfigurationSectionDesigner.AttributeParameter" Name="AttributeParameter" DisplayName="Attribute Parameter" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="539879e8-d7b5-4367-a382-dca53e9df073" Description="The name of a name attribute parameter. Set to empty string if the parameter is not named." Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e9e3420c-a13a-4104-855b-8ad57a131c44" Description="The value of the parameter." Name="Value" DisplayName="Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="3bf59c64-04a1-41f3-b6f8-741efaae6649" Description="Diagram comment box. Will not affect resulting configuration code." Name="Comment" DisplayName="Comment" Namespace="ConfigurationSectionDesigner">
      <Properties>
        <DomainProperty Id="701e762d-188d-4acd-a264-a4d34f59eb17" Description="Comment text." Name="Text" DisplayName="Text">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof( System.ComponentModel.Design.MultilineStringEditor )" />
                <AttributeParameter Value="typeof( System.Drawing.Design.UITypeEditor )" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="5770271a-b8f0-4768-9598-e0025796fa76" Description="Description for ConfigurationSectionDesigner.ConfigurationElementHasAttributeProperties" Name="ConfigurationElementHasAttributeProperties" DisplayName="Configuration Element Has Attribute Properties" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="d531d4e7-5eef-4bd6-a989-37da3aa0ef57" Description="Description for ConfigurationSectionDesigner.ConfigurationElementHasAttributeProperties.ConfigurationElement" Name="ConfigurationElement" DisplayName="Configuration Element" PropertyName="AttributeProperties" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Attribute Properties">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="e0a7c246-6b21-47e4-b537-a23bfef86401" Description="Description for ConfigurationSectionDesigner.ConfigurationElementHasAttributeProperties.AttributeProperty" Name="AttributeProperty" DisplayName="Attribute Property" PropertyName="ConfigurationElement" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Configuration Element">
          <RolePlayer>
            <DomainClassMoniker Name="AttributeProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="c2b30fd0-a8b8-4b9f-abe5-fd8447c1f53e" Description="Description for ConfigurationSectionDesigner.ConfigurationElementCollectionHasItemType" Name="ConfigurationElementCollectionHasItemType" DisplayName="Configuration Element Collection Has Item Type" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="fb43fdc5-b8e3-4638-95c8-94f4932bef14" Description="The type of the items in this collection." Name="ConfigurationElementCollection" DisplayName="Configuration Element Collection" PropertyName="ItemType" Multiplicity="One" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" Category="Definition" PropertyDisplayName="Item Type">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationElementCollection" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c3883765-4155-41e5-8118-7b5a79089c3e" Description="Description for ConfigurationSectionDesigner.ConfigurationElementCollectionHasItemType.ConfigurationElement" Name="ConfigurationElement" DisplayName="Configuration Element" PropertyName="ReferringConfigurationElementCollections" PropertyDisplayName="Referring Configuration Element Collections">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="12ffa767-778b-486d-a9a6-10d49f2fa642" Description="Description for ConfigurationSectionDesigner.ConfigurationElementHasElementProperties" Name="ConfigurationElementHasElementProperties" DisplayName="Configuration Element Has Element Properties" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="dd6504ef-77ca-4e58-80cd-35ce1245bcfd" Description="Description for ConfigurationSectionDesigner.ConfigurationElementHasElementProperties.ConfigurationElement" Name="ConfigurationElement" DisplayName="Configuration Element" PropertyName="ElementProperties" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Element Properties">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="b31a7e8b-3522-45d9-9f7c-346dbaa52f7f" Description="Description for ConfigurationSectionDesigner.ConfigurationElementHasElementProperties.ElementProperty" Name="ElementProperty" DisplayName="Element Property" PropertyName="ConfigurationElement" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Configuration Element">
          <RolePlayer>
            <DomainClassMoniker Name="ElementProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="817738b5-f315-4a74-9d4f-6ec5e726ddef" Description="The type of this property." Name="ElementPropertyHasType" DisplayName="Element Property Has Type" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="3627555c-ddc1-4630-9fe2-6f8cbebaa348" Description="The type of this property." Name="ElementProperty" DisplayName="Element Property" PropertyName="Type" Multiplicity="One" PropagatesCopy="PropagateCopyToLinkOnly" Category="Definition" PropertyDisplayName="Type">
          <RolePlayer>
            <DomainClassMoniker Name="ElementProperty" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="ddf49bab-a5fb-499f-9799-8651da650592" Description="Description for ConfigurationSectionDesigner.ElementPropertyHasType.ConfigurationElement" Name="ConfigurationElement" DisplayName="Configuration Element" PropertyName="ReferringElements" PropertyDisplayName="Referring Elements">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="44edd3cb-a478-45de-84e8-57433006cb3f" Description="The type of this property." Name="AttributePropertyHasPropertyType" DisplayName="Attribute Property Has Property Type" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="7d16d8ac-4ce5-4865-888a-ee530b21684a" Description="The type of this property." Name="AttributeProperty" DisplayName="Attribute Property" PropertyName="Type" Multiplicity="One" PropagatesCopy="PropagateCopyToLinkOnly" Category="Definition" PropertyDisplayName="Type">
          <RolePlayer>
            <DomainClassMoniker Name="AttributeProperty" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="bb3050bf-0bc1-4767-a3a1-8e55678ccd59" Description="Description for ConfigurationSectionDesigner.AttributePropertyHasPropertyType.TypeDefinition" Name="TypeDefinition" DisplayName="Type Definition" PropertyName="ReferringAttributes" PropertyDisplayName="Referring Attributes">
          <RolePlayer>
            <DomainClassMoniker Name="TypeDefinition" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="67b651a6-8f44-4a6b-b767-eed9de158dc4" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasTypeDefinitions" Name="ConfigurationSectionModelHasTypeDefinitions" DisplayName="Configuration Section Model Has Type Definitions" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="b9d0f943-254e-41b5-9254-d57dc05a33a9" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasTypeDefinitions.ConfigurationSectionModel" Name="ConfigurationSectionModel" DisplayName="Configuration Section Model" PropertyName="TypeDefinitions" PropertyDisplayName="Type Definitions">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="bfa60ac5-6010-461b-8cdc-8afa44b64f39" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasTypeDefinitions.TypeDefinition" Name="TypeDefinition" DisplayName="Type Definition" PropertyName="ConfigurationSectionModel" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Configuration Section Model">
          <RolePlayer>
            <DomainClassMoniker Name="TypeDefinition" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="e2efdbc8-1f56-4d05-b808-22ecad823313" Description="Description for ConfigurationSectionDesigner.EnumeratedTypeHasLiterals" Name="EnumeratedTypeHasLiterals" DisplayName="Enumerated Type Has Literals" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="dd223a91-254e-4f9d-a3c7-564cf15a5061" Description="Description for ConfigurationSectionDesigner.EnumeratedTypeHasLiterals.EnumeratedType" Name="EnumeratedType" DisplayName="Enumerated Type" PropertyName="Literals" PropertyDisplayName="Literals">
          <RolePlayer>
            <DomainClassMoniker Name="EnumeratedType" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="6f8dffe1-be1e-4ef0-a7d2-e32f53033331" Description="Description for ConfigurationSectionDesigner.EnumeratedTypeHasLiterals.EnumerationLiteral" Name="EnumerationLiteral" DisplayName="Enumeration Literal" PropertyName="EnumeratedType" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Enumerated Type">
          <RolePlayer>
            <DomainClassMoniker Name="EnumerationLiteral" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="1adef01e-16c1-442b-9b1b-17234b7229aa" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasConfigurationElements" Name="ConfigurationSectionModelHasConfigurationElements" DisplayName="Configuration Section Model Has Configuration Elements" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="ea7d0257-fdcf-4176-a0f6-a842fd419ed0" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasConfigurationElements.ConfigurationSectionModel" Name="ConfigurationSectionModel" DisplayName="Configuration Section Model" PropertyName="ConfigurationElements" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Configuration Elements">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="f2e44f3b-3cf4-4a4f-8702-5c6bd51c213a" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasConfigurationElements.BaseConfigurationType" Name="BaseConfigurationType" DisplayName="Base Configuration Type" PropertyName="ConfigurationSectionModel" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Configuration Section Model">
          <RolePlayer>
            <DomainClassMoniker Name="BaseConfigurationType" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="923b418a-dd91-4bcc-8926-bf432fb1c412" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupHasConfigurationSectionProperties" Name="ConfigurationSectionGroupHasConfigurationSectionProperties" DisplayName="Configuration Section Group Has Configuration Section Properties" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="2838e823-5ea2-4dc2-996b-f75fb7c9c2cd" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupHasConfigurationSectionProperties.ConfigurationSectionGroup" Name="ConfigurationSectionGroup" DisplayName="Configuration Section Group" PropertyName="ConfigurationSectionProperties" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Configuration Section Properties">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionGroup" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3a71dd6c-cc33-4efc-87fe-047db4a1cf64" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupHasConfigurationSectionProperties.ConfigurationSectionProperty" Name="ConfigurationSectionProperty" DisplayName="Configuration Section Property" PropertyName="ConfigurationSectionGroup" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Configuration Section Group">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="d919466a-4800-461d-81c7-e11140541141" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionPropertyHasConfigurationSection" Name="ConfigurationSectionPropertyHasConfigurationSection" DisplayName="Configuration Section Property Has Configuration Section" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="2bbaeed7-0255-4803-b438-fbc186d84038" Description="The configuration section the section group contains" Name="ConfigurationSectionProperty" DisplayName="Configuration Section Property" PropertyName="ContainedConfigurationSection" Multiplicity="One" PropagatesCopy="PropagateCopyToLinkOnly" Category="Definition" PropertyDisplayName="Contained Configuration Section">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionProperty" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9d0a72ee-0ae9-4fd4-babe-695ff6f9bd11" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionPropertyHasConfigurationSection.ConfigurationSection" Name="ConfigurationSection" DisplayName="Configuration Section" PropertyName="ReferringConfigurationSectionGroup" Multiplicity="ZeroOne" PropertyDisplayName="Referring Configuration Section Group">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSection" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="61e3af3d-d9c2-4bcb-a602-0a1c8b938c39" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupHasConfigurationSectionGroupProperties" Name="ConfigurationSectionGroupHasConfigurationSectionGroupProperties" DisplayName="Configuration Section Group Has Configuration Section Group Properties" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="bde62e92-e0ea-483b-be95-4fd02c910e5b" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupHasConfigurationSectionGroupProperties.ConfigurationSectionGroup" Name="ConfigurationSectionGroup" DisplayName="Configuration Section Group" PropertyName="ConfigurationSectionGroupProperties" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Configuration Section Group Properties">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionGroup" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a75ab573-5a61-45bc-a094-96e1a4171c6f" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupHasConfigurationSectionGroupProperties.ConfigurationSectionGroupProperty" Name="ConfigurationSectionGroupProperty" DisplayName="Configuration Section Group Property" PropertyName="ConfigurationSectionGroup" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Configuration Section Group">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionGroupProperty" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="b168b5ae-d730-494c-90a0-ef7e06bcb125" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupPropertyHasConfigurationSectionGroup" Name="ConfigurationSectionGroupPropertyHasConfigurationSectionGroup" DisplayName="Configuration Section Group Property Has Configuration Section Group" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="39cde385-a63e-424e-af55-2e70df24d7a2" Description="The configuration section the section group contains" Name="ConfigurationSectionGroupProperty" DisplayName="Configuration Section Group Property" PropertyName="ContainedConfigurationSectionGroup" Multiplicity="One" PropagatesCopy="PropagateCopyToLinkOnly" Category="Definition" PropertyDisplayName="Configuration Section Group">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionGroupProperty" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="4667c6a9-f024-4a0f-b114-8d4e296c05c6" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupPropertyHasConfigurationSectionGroup.ConfigurationSectionGroup" Name="ConfigurationSectionGroup" DisplayName="Configuration Section Group" PropertyName="ReferringConfigurationSectionGroup" Multiplicity="ZeroOne" PropertyDisplayName="Referring Configuration Section Group">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionGroup" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="e56558c8-7988-43a2-bb79-80a0349a4f0c" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasPropertyValidators" Name="ConfigurationSectionModelHasPropertyValidators" DisplayName="Configuration Section Model Has Property Validators" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="9cd358d7-41ee-46dd-99c1-09fd790a7852" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasPropertyValidators.ConfigurationSectionModel" Name="ConfigurationSectionModel" DisplayName="Configuration Section Model" PropertyName="PropertyValidators" Multiplicity="ZeroOne" PropertyDisplayName="Property Validators">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="2e404e32-005e-4975-96b0-2689b178ffab" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasPropertyValidators.PropertyValidators" Name="PropertyValidators" DisplayName="Property Validators" PropertyName="ConfigurationSectionModel" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Configuration Section Model">
          <RolePlayer>
            <DomainClassMoniker Name="PropertyValidators" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="8d6c7262-eb89-42ca-b6f1-cd012016b71d" Description="Description for ConfigurationSectionDesigner.PropertyValidatorsHasValidators" Name="PropertyValidatorsHasValidators" DisplayName="Property Validators Has Validators" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="e210e39e-9c8d-4926-b523-78e9f7c3f9c3" Description="Description for ConfigurationSectionDesigner.PropertyValidatorsHasValidators.PropertyValidators" Name="PropertyValidators" DisplayName="Property Validators" PropertyName="Validators" PropertyDisplayName="Validators">
          <RolePlayer>
            <DomainClassMoniker Name="PropertyValidators" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="b66cd1b8-ee71-40b1-828a-dbd4106fb49d" Description="Description for ConfigurationSectionDesigner.PropertyValidatorsHasValidators.PropertyValidator" Name="PropertyValidator" DisplayName="Property Validator" PropertyName="PropertyValidators" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Property Validators">
          <RolePlayer>
            <DomainClassMoniker Name="PropertyValidator" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="4425730d-db79-493a-bd8e-d366b21149e0" Description="Description for ConfigurationSectionDesigner.ConfigurationPropertyHasPropertyValidator" Name="ConfigurationPropertyHasPropertyValidator" DisplayName="Configuration Property Has Property Validator" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="13383714-1aec-4a69-acc3-52fce0a3b2d3" Description="The validator used to validate this property. Add validators by viewing the 'Configuration Section Explorer' explorer tab and locating the Property Validators item. Right-click that item to add." Name="ConfigurationProperty" DisplayName="Configuration Property" PropertyName="Validator" Multiplicity="ZeroOne" Category="Code" PropertyDisplayName="Validator">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationProperty" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="6c7be187-0f7b-442c-86aa-7fb319ebe5c3" Description="Description for ConfigurationSectionDesigner.ConfigurationPropertyHasPropertyValidator.PropertyValidator" Name="PropertyValidator" DisplayName="Property Validator" PropertyName="ReferringConfigurationProperties" PropertyDisplayName="Referring Configuration Properties">
          <RolePlayer>
            <DomainClassMoniker Name="PropertyValidator" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f6d4e7ff-ce6c-4bef-8942-a870622bb8ae" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasCustomTypeConverters" Name="ConfigurationSectionModelHasCustomTypeConverters" DisplayName="Configuration Section Model Has Custom Type Converters" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="362e12a7-349d-4ac8-a7a9-ca616a4e7b59" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasCustomTypeConverters.ConfigurationSectionModel" Name="ConfigurationSectionModel" DisplayName="Configuration Section Model" PropertyName="CustomTypeConverters" PropertyDisplayName="Custom Type Converters">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="342d46c1-792f-4603-8284-29690567a636" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasCustomTypeConverters.CustomTypeConverter" Name="CustomTypeConverter" DisplayName="Custom Type Converter" PropertyName="ConfigurationSectionModel" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Configuration Section Model">
          <RolePlayer>
            <DomainClassMoniker Name="CustomTypeConverter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="d77d39bf-afcb-4612-923c-f7513617f76e" Description="Description for ConfigurationSectionDesigner.ConfigurationPropertyReferencesCustomTypeConverter" Name="ConfigurationPropertyReferencesCustomTypeConverter" DisplayName="Configuration Property References Custom Type Converter" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="aebf2438-fdc3-4318-8944-88e50d51f37e" Description="The custom type converter to use if the TypeConverter property is set to Custom" Name="ConfigurationProperty" DisplayName="Configuration Property" PropertyName="CustomTypeConverter" Multiplicity="ZeroOne" Category="Code" PropertyDisplayName="Custom Type Converter">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationProperty" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a07d8f5c-248e-434a-bab4-20e51627c17d" Description="Description for ConfigurationSectionDesigner.ConfigurationPropertyReferencesCustomTypeConverter.CustomTypeConverter" Name="CustomTypeConverter" DisplayName="Custom Type Converter" PropertyName="ConfigurationProperties" PropertyDisplayName="Configuration Properties">
          <RolePlayer>
            <DomainClassMoniker Name="CustomTypeConverter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="8a5af61d-5d76-47b2-9d2c-c625702492c2" Description="Description for ConfigurationSectionDesigner.CustomTypeConverterHasType" Name="CustomTypeConverterHasType" DisplayName="Custom Type Converter Has Type" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="7021ddca-b96a-4c47-b24a-6201ff101272" Description="The type this converter converts to and from" Name="CustomTypeConverter" DisplayName="Custom Type Converter" PropertyName="Type" Multiplicity="One" PropertyDisplayName="Type">
          <RolePlayer>
            <DomainClassMoniker Name="CustomTypeConverter" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="97181424-d129-4f7d-beea-7dfa67d6df35" Description="Description for ConfigurationSectionDesigner.CustomTypeConverterHasType.TypeBase" Name="TypeBase" DisplayName="Type Base" PropertyName="ReferringCustomTypeConverters" PropertyDisplayName="Referring Custom Type Converters">
          <RolePlayer>
            <DomainClassMoniker Name="TypeBase" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="1baa42c8-dd15-4029-9ba7-79ec811a0f1a" Description="Description for ConfigurationSectionDesigner.BaseConfigurationTypeHasBaseClass" Name="BaseConfigurationTypeHasBaseClass" DisplayName="Base Configuration Type Has Base Class" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="4373f92f-6e61-4830-ac95-99330cbb2225" Description="Base class of this type" Name="SourceBaseConfigurationType" DisplayName="Source Base Configuration Type" PropertyName="BaseClass" Multiplicity="ZeroOne" Category="Code" PropertyDisplayName="Base Class">
          <RolePlayer>
            <DomainClassMoniker Name="BaseConfigurationType" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="f706d85d-7190-41aa-820c-6b8d40724833" Description="Description for ConfigurationSectionDesigner.BaseConfigurationTypeHasBaseClass.TargetBaseConfigurationType" Name="TargetBaseConfigurationType" DisplayName="Target Base Configuration Type" PropertyName="SuperClassOf" PropertyDisplayName="Super Class Of">
          <RolePlayer>
            <DomainClassMoniker Name="BaseConfigurationType" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="44e84722-2ff6-48a1-90dc-803dfa50cca5" Description="Description for ConfigurationSectionDesigner.AttributeHasParameters" Name="AttributeHasParameters" DisplayName="Attribute Has Parameters" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="713c1da5-5c5a-4352-a10e-3e72d4f0a4b3" Description="The list of parameters for this attribute." Name="Attribute" DisplayName="Attribute" PropertyName="Parameters" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertySetterAccessModifier="Private" PropertyDisplayName="Parameters">
          <RolePlayer>
            <DomainClassMoniker Name="Attribute" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="bf506f51-c08d-43fb-b300-3182b4184e74" Description="Description for ConfigurationSectionDesigner.AttributeHasParameters.AttributeParameter" Name="AttributeParameter" DisplayName="Attribute Parameter" PropertyName="Attribute" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Attribute">
          <RolePlayer>
            <DomainClassMoniker Name="AttributeParameter" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="e49ba3bf-c523-4e21-85d1-4880fcb17f1f" Description="Description for ConfigurationSectionDesigner.ConfigurationPropertyHasAttributes" Name="ConfigurationPropertyHasAttributes" DisplayName="Configuration Property Has Attributes" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="6b1b4fb9-e6d5-4c65-af2e-c9b5e74233e2" Description="Adds custom Attributes to the property" Name="ConfigurationProperty" DisplayName="Configuration Property" PropertyName="Attributes" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" Category="Metadata" PropertyDisplayName="Attributes">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationProperty" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="890cf041-32e8-4db9-8602-bcfc98beab11" Description="Description for ConfigurationSectionDesigner.ConfigurationPropertyHasAttributes.Attribute" Name="Attribute" DisplayName="Attribute" PropertyName="ConfigurationProperty" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Configuration Property">
          <RolePlayer>
            <DomainClassMoniker Name="Attribute" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="04d566c5-f5da-4d61-be32-d91f4ad5b1e7" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasComments" Name="ConfigurationSectionModelHasComments" DisplayName="Configuration Section Model Has Comments" Namespace="ConfigurationSectionDesigner" IsEmbedding="true">
      <Source>
        <DomainRole Id="82dd23e8-ec76-4558-a51e-f1b31c02de6d" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasComments.ConfigurationSectionModel" Name="ConfigurationSectionModel" DisplayName="Configuration Section Model" PropertyName="Comments" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="ConfigurationSectionModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="4bfffecc-bd39-4d9c-b22d-ffd0522d9c15" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionModelHasComments.Comment" Name="Comment" DisplayName="Comment" PropertyName="ConfigurationSectionModel" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Configuration Section Model">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="070e998b-947e-4aec-be77-f88beb8d727b" Description="Description for ConfigurationSectionDesigner.CommentsReferenceConfigurationItems" Name="CommentsReferenceConfigurationItems" DisplayName="Comments Reference Configuration Items" Namespace="ConfigurationSectionDesigner">
      <Source>
        <DomainRole Id="47585056-4f3c-4d8d-a73c-3ffff6a09383" Description="" Name="Comment" DisplayName="Comment" PropertyName="Subjects" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Subjects">
          <RolePlayer>
            <DomainClassMoniker Name="Comment" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c8b442e7-3005-486f-ab51-67542a90fe9b" Description="" Name="Subject" DisplayName="Subject" PropertyName="Comments" IsPropertyGenerator="false" PropertyDisplayName="Comments">
          <RolePlayer>
            <DomainClassMoniker Name="BaseConfigurationType" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <ExternalType Name="ConfigurationElementCollectionType" Namespace="System.Configuration" />
    <DomainEnumeration Name="ConfigurationSectionCodeGenOptions" Namespace="ConfigurationSectionDesigner" IsFlags="true" Description="The possible blocks of code that are generated for a Configuration Section.">
      <Literals>
        <EnumerationLiteral Description="Generates a static singleton instance for easy access to the Configuration Section." Name="Singleton" Value="1" />
        <EnumerationLiteral Description="No additional code is generated." Name="None" Value="0" />
        <EnumerationLiteral Description="Generates a property that represents the XML namespace (xmlns)." Name="XmlnsProperty" Value="2" />
        <EnumerationLiteral Description="Generates code for easy access to the configuration section protection mechanisms" Name="Protection" Value="4" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="ConfigurationElementCollectionCodeGenOptions" Namespace="ConfigurationSectionDesigner" IsFlags="true" Description="The possible blocks of code that are generated for a Configuration Element Collection.">
      <Literals>
        <EnumerationLiteral Description="Generates an indexer for the elements in the collection." Name="Indexer" Value="1" />
        <EnumerationLiteral Description="Generates an &quot;Add&quot; method to add items to the collection." Name="AddMethod" Value="2" />
        <EnumerationLiteral Description="No additional code is generated." Name="None" Value="0" />
        <EnumerationLiteral Description="Generates a &quot;Remove&quot; method to add items to the collection." Name="RemoveMethod" Value="4" />
        <EnumerationLiteral Description="Generates &quot;GetItem&quot; methods to retrieve items from the collection by key and id." Name="GetItemMethods" Value="8" />
        <EnumerationLiteral Description="Generates ICollection&lt;T&gt; implementation for the collection" Name="ICollection" Value="16" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="TypeDefinitionCodeGenOptions" Namespace="ConfigurationSectionDesigner" IsFlags="true" Description="The options for code that is generated for a Type Definition.">
      <Literals>
        <EnumerationLiteral Description="No code is generated." Name="None" Value="0" />
        <EnumerationLiteral Description="Generates the type definition in code." Name="TypeDefinition" Value="1" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="CustomEditors" Namespace="ConfigurationSectionDesigner" Description="Description for ConfigurationSectionDesigner.CustomEditors">
      <Literals>
        <EnumerationLiteral Description="Do not use a custom editor" Name="None" Value="0" />
        <EnumerationLiteral Description="Provides a user interface for choosing a file from the file system" Name="FileNameEditor" Value="1" />
        <EnumerationLiteral Description="Provides a user interface for choosing a folder from the file system" Name="FolderNameEditor" Value="2" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="ProtectionProviders" Namespace="ConfigurationSectionDesigner" Description="Description for ConfigurationSectionDesigner.ProtectionProviders">
      <Literals>
        <EnumerationLiteral Description="This provider uses DPAPI to encrypt and decrypt data" Name="DataProtectionConfigurationProvider" Value="" />
        <EnumerationLiteral Description="This provider uses the RSA public key encryption to encrypt and decrypt data" Name="RSAProtectedConfigurationProvider" Value="" />
        <EnumerationLiteral Description="Use a custom ProtectedConfigurationProvider" Name="Custom" Value="" />
      </Literals>
    </DomainEnumeration>
    <ExternalType Name="TimeSpan" Namespace="System" />
    <DomainEnumeration Name="TypeConverters" Namespace="ConfigurationSectionDesigner" Description="Description for ConfigurationSectionDesigner.TypeConverters">
      <Literals>
        <EnumerationLiteral Description="Converts a comma-delimited value to/from a CommaDelimitedStringCollection" Name="CommaDelimitedStringCollectionConverter" Value="" />
        <EnumerationLiteral Description="Converts between a string and an enumerated type" Name="GenericEnumConverter" Value="" />
        <EnumerationLiteral Description="Converts between a string and the standard infinite or integer value" Name="InfiniteIntConverter" Value="" />
        <EnumerationLiteral Description="Converts between a string and the standard infinite TimeSpan value" Name="InfiniteTimeSpanConverter" Value="" />
        <EnumerationLiteral Description="No type conversion will be done" Name="None" Value="" />
        <EnumerationLiteral Description="Converts to and from a time span expressed in minutes" Name="TimeSpanMinutesConverter" Value="" />
        <EnumerationLiteral Description="Converts to and from a time span expressed in minutes, or infinite" Name="TimeSpanMinutesOrInfiniteConverter" Value="" />
        <EnumerationLiteral Description="Converts to and from a time span expressed in seconds" Name="TimeSpanSecondsConverter" Value="" />
        <EnumerationLiteral Description="Converts to and from a time span expressed in seconds, or infinite" Name="TimeSpanSecondsOrInfiniteConverter" Value="" />
        <EnumerationLiteral Description="Converts between a Type object and the string representation of that type" Name="TypeNameConverter" Value="" />
        <EnumerationLiteral Description="Converts a string to its canonical format (white space trimmed from front and back)" Name="WhiteSpaceTrimStringConverter" Value="" />
        <EnumerationLiteral Description="Use a custom type converter" Name="Custom" Value="" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="InheritanceModifiers" Namespace="ConfigurationSectionDesigner" Description="Description for ConfigurationSectionDesigner.InheritanceModifiers">
      <Literals>
        <EnumerationLiteral Description="Description for ConfigurationSectionDesigner.InheritanceModifiers.None" Name="None" Value="0" />
        <EnumerationLiteral Description="Description for ConfigurationSectionDesigner.InheritanceModifiers.Abstract" Name="Abstract" Value="1" />
        <EnumerationLiteral Description="Description for ConfigurationSectionDesigner.InheritanceModifiers.Sealed" Name="Sealed" Value="2" />
      </Literals>
      <Attributes>
        <ClrAttribute Name="System.ComponentModel.TypeConverter">
          <Parameters>
            <AttributeParameter Value="typeof(InheritanceModifierConverter)" />
          </Parameters>
        </ClrAttribute>
      </Attributes>
    </DomainEnumeration>
    <ExternalType Name="DashStyle" Namespace="System.Drawing.Drawing2D" />
    <DomainEnumeration Name="AccessModifiers" Namespace="ConfigurationSectionDesigner" Description="Description for ConfigurationSectionDesigner.AccessModifiers">
      <Literals>
        <EnumerationLiteral Description="Description for ConfigurationSectionDesigner.AccessModifiers.Internal" Name="Internal" Value="0" />
        <EnumerationLiteral Description="Description for ConfigurationSectionDesigner.AccessModifiers.Public" Name="Public" Value="1" />
      </Literals>
      <Attributes>
        <ClrAttribute Name="System.ComponentModel.TypeConverter">
          <Parameters>
            <AttributeParameter Value="typeof(AccessModifierConverter)" />
          </Parameters>
        </ClrAttribute>
      </Attributes>
    </DomainEnumeration>
  </Types>
  <Shapes>
    <CompartmentShape Id="73125fd0-c3b7-43b4-ab7d-d92536e7c949" Description="Description for ConfigurationSectionDesigner.ConfigurationShape" Name="ConfigurationShape" DisplayName="Configuration Shape" Namespace="ConfigurationSectionDesigner" GeneratesDoubleDerived="true" FixedTooltipText="Configuration Shape" FillColor="LightSteelBlue" InitialHeight="0.4" OutlineThickness="0.01" FillGradientMode="None" ExposesOutlineDashStyleAsProperty="true" ExposesOutlineThicknessAsProperty="true" Geometry="Rectangle">
      <Properties>
        <DomainProperty Id="9ec5f7f0-461e-4601-91ac-2ae4083edbee" Description="Description for ConfigurationSectionDesigner.ConfigurationShape.Outline Dash Style" Name="OutlineDashStyle" DisplayName="Outline Dash Style" Kind="CustomStorage" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System.Drawing.Drawing2D/DashStyle" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3e499060-7cff-4e37-8b75-01d5b923058f" Description="Description for ConfigurationSectionDesigner.ConfigurationShape.Outline Thickness" Name="OutlineThickness" DisplayName="Outline Thickness" Kind="CustomStorage" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Single" />
          </Type>
        </DomainProperty>
      </Properties>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.2" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapseDecorator" DisplayName="Expand Collapse Decorator" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.2" VerticalOffset="0.15">
        <TextDecorator Name="DescriptionDecorator" DisplayName="Description Decorator" DefaultText="DescriptionDecorator" FontSize="7" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IconDecoratorConfigurationElement" DisplayName="Icon Decorator Configuration Element" DefaultIcon="Resources\ConfigurationElement.bmp" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.0025" VerticalOffset="0.0025">
        <IconDecorator Name="IconDecoratorConfigurationSection" DisplayName="Icon Decorator Configuration Section" DefaultIcon="Resources\ConfigurationSection.bmp" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IconDecoratorConfigurationElementCollection" DisplayName="Icon Decorator Configuration Element Collection" DefaultIcon="Resources\ConfigurationElementCollection.bmp" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="IconDecoratorConfigurationSectionGroup" DisplayName="Icon Decorator Configuration Section Group" DefaultIcon="Resources\ConfigurationSectionGroup.bmp" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0.2" VerticalOffset="0">
        <TextDecorator Name="AbstractNameDecorator" DisplayName="Abstract Name Decorator" DefaultText="AbstractNameDecorator" FontStyle="Bold, Italic" />
      </ShapeHasDecorators>
    </CompartmentShape>
    <CompartmentShape Id="c0033fe2-ee4c-4eff-98e4-e2b2a912c4f9" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionShape" Name="ConfigurationSectionShape" DisplayName="Configuration Section Shape" Namespace="ConfigurationSectionDesigner" FixedTooltipText="Configuration Section Shape" FillColor="255, 192, 128" OutlineColor="24, 143, 222" InitialHeight="1" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseCompartmentShape>
        <CompartmentShapeMoniker Name="ConfigurationElementShape" />
      </BaseCompartmentShape>
    </CompartmentShape>
    <CompartmentShape Id="3a3bc190-b6b6-4bd9-8abc-b59563efda79" Description="Description for ConfigurationSectionDesigner.ConfigurationElementCollectionShape" Name="ConfigurationElementCollectionShape" DisplayName="Configuration Element Collection Shape" Namespace="ConfigurationSectionDesigner" FixedTooltipText="Configuration Element Collection Shape" FillColor="192, 192, 255" OutlineColor="24, 143, 222" InitialHeight="1" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseCompartmentShape>
        <CompartmentShapeMoniker Name="ConfigurationElementShape" />
      </BaseCompartmentShape>
    </CompartmentShape>
    <CompartmentShape Id="021c3880-818c-4424-b63b-85db44d70131" Description="Description for ConfigurationSectionDesigner.ConfigurationElementShape" Name="ConfigurationElementShape" DisplayName="Configuration Element Shape" Namespace="ConfigurationSectionDesigner" GeneratesDoubleDerived="true" FixedTooltipText="Configuration Element Shape" FillColor="0, 122, 204" OutlineColor="24, 143, 222" InitialHeight="1" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseCompartmentShape>
        <CompartmentShapeMoniker Name="ConfigurationShape" />
      </BaseCompartmentShape>
      <Compartment TitleFillColor="WhiteSmoke" Name="AttributeProperties" Title="Attributes" />
      <Compartment TitleFillColor="WhiteSmoke" Name="ElementProperties" Title="Elements" />
    </CompartmentShape>
    <CompartmentShape Id="9c2e341e-512c-4b42-ac42-805e268df18a" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupShape" Name="ConfigurationSectionGroupShape" DisplayName="Configuration Section Group Shape" Namespace="ConfigurationSectionDesigner" GeneratesDoubleDerived="true" FixedTooltipText="Configuration Section Group Shape" FillColor="224, 224, 224" OutlineColor="24, 143, 222" InitialHeight="1" OutlineThickness="0.01" FillGradientMode="None" Geometry="Rectangle">
      <BaseCompartmentShape>
        <CompartmentShapeMoniker Name="ConfigurationShape" />
      </BaseCompartmentShape>
      <Compartment TitleFillColor="WhiteSmoke" Name="ConfigurationSectionProperties" Title="Configuration Sections" />
      <Compartment TitleFillColor="WhiteSmoke" Name="ConfigurationSectionGroupProperties" Title="Configuration Section Groups" />
    </CompartmentShape>
    <GeometryShape Id="05924a37-802a-4788-89fb-213ebfb3fb23" Description="" Name="CommentBoxShape" DisplayName="Comment Box Shape" Namespace="ConfigurationSectionDesigner" GeneratesDoubleDerived="true" FixedTooltipText="Comment Box Shape" FillColor="255, 255, 192" OutlineColor="192, 192, 0" InitialHeight="0.3" OutlineThickness="0.015625" FillGradientMode="None" Geometry="Rectangle">
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="Comment" DisplayName="Comment" DefaultText="" />
      </ShapeHasDecorators>
    </GeometryShape>
  </Shapes>
  <Connectors>
    <Connector Id="8161bf76-7e15-418c-87cb-0326f9bb78f7" Description="Description for ConfigurationSectionDesigner.ConfigurationElementCollectionHasItemTypeConnector" Name="ConfigurationElementCollectionHasItemTypeConnector" DisplayName="Configuration Element Collection Has Item Type Connector" Namespace="ConfigurationSectionDesigner" TooltipType="Variable" FixedTooltipText="Configuration Element Collection Has Item Type Connector" TextColor="LightSlateGray" Color="LightSlateGray" DashStyle="Dot" TargetEndStyle="EmptyArrow" Thickness="0.01">
      <ConnectorHasDecorators Position="SourceTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="Item Type" FontStyle="Italic" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="b761e9ae-2bdf-4a53-a48d-0a6e90d39d7c" Description="Description for ConfigurationSectionDesigner.ElementPropertyHasTypeConnector" Name="ElementPropertyHasTypeConnector" DisplayName="Element Property Has Type Connector" Namespace="ConfigurationSectionDesigner" TooltipType="Variable" FixedTooltipText="Element Property Has Type Connector" TextColor="LightSlateGray" Color="LightSlateGray" TargetEndStyle="EmptyArrow" Thickness="0.01">
      <ConnectorHasDecorators Position="SourceTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" />
      </ConnectorHasDecorators>
    </Connector>
    <Connector Id="d595d6a8-e812-4df7-a977-ee2480affeab" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionPropertyHasConfigurationSectionConnector" Name="ConfigurationSectionPropertyHasConfigurationSectionConnector" DisplayName="Configuration Section Property Has Configuration Section Connector" Namespace="ConfigurationSectionDesigner" TooltipType="Variable" FixedTooltipText="Configuration Section Property Has Configuration Section Connector" TextColor="LightSlateGray" Color="LightSlateGray" TargetEndStyle="EmptyArrow" Thickness="0.01" />
    <Connector Id="8c93c91b-460d-41f1-874e-843a1c2474ff" Description="Description for ConfigurationSectionDesigner.ConfigurationSectionGroupHasConfigurationSectionGroupPropertiesConnector" Name="ConfigurationSectionGroupHasConfigurationSectionGroupPropertiesConnector" DisplayName="Configuration Section Group Has Configuration Section Group Properties Connector" Namespace="ConfigurationSectionDesigner" TooltipType="Variable" FixedTooltipText="Configuration Section Group Has Configuration Section Group Properties Connector" TextColor="LightSlateGray" TargetEndStyle="EmptyArrow" Thickness="0.01" />
    <Connector Id="7d43d6da-858e-47af-af79-9cc09778fc0a" Description="Description for ConfigurationSectionDesigner.InheritanceConnector" Name="InheritanceConnector" DisplayName="Inheritance Connector" Namespace="ConfigurationSectionDesigner" TooltipType="Variable" FixedTooltipText="Inheritance Connector" TextColor="LightSlateGray" Color="LightSlateGray" TargetEndStyle="HollowArrow" Thickness="0.01" />
    <Connector Id="64e9f29b-f57b-469d-aabe-447faf73c297" Description="" Name="CommentLink" DisplayName="Comment Link" Namespace="ConfigurationSectionDesigner" FixedTooltipText="Comment Link" Color="Gray" DashStyle="Dot" RoutingStyle="Straight" />
  </Connectors>
  <XmlSerializationBehavior Name="ConfigurationSectionDesignerSerializationBehavior" Namespace="ConfigurationSectionDesigner">
    <ClassData>
      <XmlClassData TypeName="ConfigurationSectionModel" MonikerAttributeName="" IsCustom="true" SerializeId="true" MonikerElementName="configurationSectionModelMoniker" ElementName="configurationSectionModel" MonikerTypeName="ConfigurationSectionModelMoniker">
        <DomainClassMoniker Name="ConfigurationSectionModel" />
        <ElementData>
          <XmlPropertyData XmlName="namespace">
            <DomainPropertyMoniker Name="ConfigurationSectionModel/Namespace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="xmlSchemaNamespace">
            <DomainPropertyMoniker Name="ConfigurationSectionModel/XmlSchemaNamespace" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="typeDefinitions">
            <DomainRelationshipMoniker Name="ConfigurationSectionModelHasTypeDefinitions" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="configurationElements">
            <DomainRelationshipMoniker Name="ConfigurationSectionModelHasConfigurationElements" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="propertyValidators">
            <DomainRelationshipMoniker Name="ConfigurationSectionModelHasPropertyValidators" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="customTypeConverters">
            <DomainRelationshipMoniker Name="ConfigurationSectionModelHasCustomTypeConverters" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="comments">
            <DomainRelationshipMoniker Name="ConfigurationSectionModelHasComments" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionDesignerDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="minimalLanguageDiagramMoniker" ElementName="minimalLanguageDiagram" MonikerTypeName="ConfigurationSectionDesignerDiagramMoniker">
        <DiagramMoniker Name="ConfigurationSectionDesignerDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationElement" MonikerAttributeName="" MonikerElementName="configurationElementMoniker" ElementName="configurationElement" MonikerTypeName="ConfigurationElementMoniker">
        <DomainClassMoniker Name="ConfigurationElement" />
        <ElementData>
          <XmlRelationshipData RoleElementName="attributeProperties">
            <DomainRelationshipMoniker Name="ConfigurationElementHasAttributeProperties" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="elementProperties">
            <DomainRelationshipMoniker Name="ConfigurationElementHasElementProperties" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="documentation">
            <DomainPropertyMoniker Name="ConfigurationElement/Documentation" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="hasCustomChildElements">
            <DomainPropertyMoniker Name="ConfigurationElement/HasCustomChildElements" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isReadOnly">
            <DomainPropertyMoniker Name="ConfigurationElement/IsReadOnly" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="ConfigurationElement/DisplayName" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationElementCollectionHasItemTypeConnector" MonikerAttributeName="" MonikerElementName="configurationElementCollectionHasItemTypeConnectorMoniker" ElementName="configurationElementCollectionHasItemTypeConnector" MonikerTypeName="ConfigurationElementCollectionHasItemTypeConnectorMoniker">
        <ConnectorMoniker Name="ConfigurationElementCollectionHasItemTypeConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationShape" MonikerAttributeName="" MonikerElementName="configurationShapeMoniker" ElementName="configurationShape" MonikerTypeName="ConfigurationShapeMoniker">
        <CompartmentShapeMoniker Name="ConfigurationShape" />
        <ElementData>
          <XmlPropertyData XmlName="outlineDashStyle">
            <DomainPropertyMoniker Name="ConfigurationShape/OutlineDashStyle" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="outlineThickness">
            <DomainPropertyMoniker Name="ConfigurationShape/OutlineThickness" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AttributeProperty" MonikerAttributeName="" MonikerElementName="attributePropertyMoniker" ElementName="attributeProperty" MonikerTypeName="AttributePropertyMoniker">
        <DomainClassMoniker Name="AttributeProperty" />
        <ElementData>
          <XmlPropertyData XmlName="defaultValue">
            <DomainPropertyMoniker Name="AttributeProperty/DefaultValue" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="type">
            <DomainRelationshipMoniker Name="AttributePropertyHasPropertyType" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="customEditor">
            <DomainPropertyMoniker Name="AttributeProperty/CustomEditor" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationElementHasAttributeProperties" MonikerAttributeName="" MonikerElementName="configurationElementHasAttributePropertiesMoniker" ElementName="configurationElementHasAttributeProperties" MonikerTypeName="ConfigurationElementHasAttributePropertiesMoniker">
        <DomainRelationshipMoniker Name="ConfigurationElementHasAttributeProperties" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSection" MonikerAttributeName="" MonikerElementName="configurationSectionMoniker" ElementName="configurationSection" MonikerTypeName="ConfigurationSectionMoniker">
        <DomainClassMoniker Name="ConfigurationSection" />
        <ElementData>
          <XmlPropertyData XmlName="codeGenOptions">
            <DomainPropertyMoniker Name="ConfigurationSection/CodeGenOptions" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="xmlSectionName">
            <DomainPropertyMoniker Name="ConfigurationSection/XmlSectionName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="protectionProvider">
            <DomainPropertyMoniker Name="ConfigurationSection/ProtectionProvider" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="customProtectionProvider">
            <DomainPropertyMoniker Name="ConfigurationSection/CustomProtectionProvider" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionShape" MonikerAttributeName="" MonikerElementName="configurationSectionShapeMoniker" ElementName="configurationSectionShape" MonikerTypeName="ConfigurationSectionShapeMoniker">
        <CompartmentShapeMoniker Name="ConfigurationSectionShape" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationElementCollection" MonikerAttributeName="" MonikerElementName="configurationElementCollectionMoniker" ElementName="configurationElementCollection" MonikerTypeName="ConfigurationElementCollectionMoniker">
        <DomainClassMoniker Name="ConfigurationElementCollection" />
        <ElementData>
          <XmlRelationshipData RoleElementName="itemType">
            <DomainRelationshipMoniker Name="ConfigurationElementCollectionHasItemType" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="collectionType">
            <DomainPropertyMoniker Name="ConfigurationElementCollection/CollectionType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="xmlItemName">
            <DomainPropertyMoniker Name="ConfigurationElementCollection/XmlItemName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="codeGenOptions">
            <DomainPropertyMoniker Name="ConfigurationElementCollection/CodeGenOptions" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="addItemName">
            <DomainPropertyMoniker Name="ConfigurationElementCollection/AddItemName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="removeItemName">
            <DomainPropertyMoniker Name="ConfigurationElementCollection/RemoveItemName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="clearItemsName">
            <DomainPropertyMoniker Name="ConfigurationElementCollection/ClearItemsName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="useHashForElementKey">
            <DomainPropertyMoniker Name="ConfigurationElementCollection/UseHashForElementKey" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationElementCollectionShape" MonikerAttributeName="" MonikerElementName="configurationElementCollectionShapeMoniker" ElementName="configurationElementCollectionShape" MonikerTypeName="ConfigurationElementCollectionShapeMoniker">
        <CompartmentShapeMoniker Name="ConfigurationElementCollectionShape" />
      </XmlClassData>
      <XmlClassData TypeName="ElementPropertyHasTypeConnector" MonikerAttributeName="" MonikerElementName="elementPropertyHasTypeConnectorMoniker" ElementName="elementPropertyHasTypeConnector" MonikerTypeName="ElementPropertyHasTypeConnectorMoniker">
        <ConnectorMoniker Name="ElementPropertyHasTypeConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationElementCollectionHasItemType" MonikerAttributeName="" MonikerElementName="configurationElementCollectionHasItemTypeMoniker" ElementName="configurationElementCollectionHasItemType" MonikerTypeName="ConfigurationElementCollectionHasItemTypeMoniker">
        <DomainRelationshipMoniker Name="ConfigurationElementCollectionHasItemType" />
      </XmlClassData>
      <XmlClassData TypeName="ElementProperty" MonikerAttributeName="" MonikerElementName="elementPropertyMoniker" ElementName="elementProperty" MonikerTypeName="ElementPropertyMoniker">
        <DomainClassMoniker Name="ElementProperty" />
        <ElementData>
          <XmlRelationshipData RoleElementName="type">
            <DomainRelationshipMoniker Name="ElementPropertyHasType" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationProperty" MonikerAttributeName="name" MonikerElementName="configurationPropertyMoniker" ElementName="configurationProperty" MonikerTypeName="ConfigurationPropertyMoniker">
        <DomainClassMoniker Name="ConfigurationProperty" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="ConfigurationProperty/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isRequired">
            <DomainPropertyMoniker Name="ConfigurationProperty/IsRequired" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isKey">
            <DomainPropertyMoniker Name="ConfigurationProperty/IsKey" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isDefaultCollection">
            <DomainPropertyMoniker Name="ConfigurationProperty/IsDefaultCollection" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="xmlName">
            <DomainPropertyMoniker Name="ConfigurationProperty/XmlName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="isReadOnly">
            <DomainPropertyMoniker Name="ConfigurationProperty/IsReadOnly" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="documentation">
            <DomainPropertyMoniker Name="ConfigurationProperty/Documentation" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="ignoredCustomAttributes" Representation="Ignore">
            <DomainPropertyMoniker Name="ConfigurationProperty/CustomAttributes" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="browsable">
            <DomainPropertyMoniker Name="ConfigurationProperty/Browsable" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="displayName">
            <DomainPropertyMoniker Name="ConfigurationProperty/DisplayName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="category">
            <DomainPropertyMoniker Name="ConfigurationProperty/Category" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="validator">
            <DomainRelationshipMoniker Name="ConfigurationPropertyHasPropertyValidator" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="typeConverter">
            <DomainPropertyMoniker Name="ConfigurationProperty/TypeConverter" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="customTypeConverter">
            <DomainRelationshipMoniker Name="ConfigurationPropertyReferencesCustomTypeConverter" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="customAttributes">
            <DomainRelationshipMoniker Name="ConfigurationPropertyHasAttributes" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationElementHasElementProperties" MonikerAttributeName="" MonikerElementName="configurationElementHasElementPropertiesMoniker" ElementName="configurationElementHasElementProperties" MonikerTypeName="ConfigurationElementHasElementPropertiesMoniker">
        <DomainRelationshipMoniker Name="ConfigurationElementHasElementProperties" />
      </XmlClassData>
      <XmlClassData TypeName="ElementPropertyHasType" MonikerAttributeName="" MonikerElementName="elementPropertyHasTypeMoniker" ElementName="elementPropertyHasType" MonikerTypeName="ElementPropertyHasTypeMoniker">
        <DomainRelationshipMoniker Name="ElementPropertyHasType" />
      </XmlClassData>
      <XmlClassData TypeName="TypeDefinition" MonikerAttributeName="" MonikerElementName="typeDefinitionMoniker" ElementName="typeDefinition" MonikerTypeName="TypeDefinitionMoniker">
        <DomainClassMoniker Name="TypeDefinition" />
      </XmlClassData>
      <XmlClassData TypeName="ExternalType" MonikerAttributeName="" MonikerElementName="externalTypeMoniker" ElementName="externalType" MonikerTypeName="ExternalTypeMoniker">
        <DomainClassMoniker Name="ExternalType" />
      </XmlClassData>
      <XmlClassData TypeName="EnumeratedType" MonikerAttributeName="" MonikerElementName="enumeratedTypeMoniker" ElementName="enumeratedType" MonikerTypeName="EnumeratedTypeMoniker">
        <DomainClassMoniker Name="EnumeratedType" />
        <ElementData>
          <XmlPropertyData XmlName="isFlags">
            <DomainPropertyMoniker Name="EnumeratedType/IsFlags" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="literals">
            <DomainRelationshipMoniker Name="EnumeratedTypeHasLiterals" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="documentation">
            <DomainPropertyMoniker Name="EnumeratedType/Documentation" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="codeGenOptions">
            <DomainPropertyMoniker Name="EnumeratedType/CodeGenOptions" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AttributePropertyHasPropertyType" MonikerAttributeName="" MonikerElementName="attributePropertyHasPropertyTypeMoniker" ElementName="attributePropertyHasPropertyType" MonikerTypeName="AttributePropertyHasPropertyTypeMoniker">
        <DomainRelationshipMoniker Name="AttributePropertyHasPropertyType" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionModelHasTypeDefinitions" MonikerAttributeName="" MonikerElementName="configurationSectionModelHasTypeDefinitionsMoniker" ElementName="configurationSectionModelHasTypeDefinitions" MonikerTypeName="ConfigurationSectionModelHasTypeDefinitionsMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionModelHasTypeDefinitions" />
      </XmlClassData>
      <XmlClassData TypeName="EnumerationLiteral" MonikerAttributeName="name" MonikerElementName="enumerationLiteralMoniker" ElementName="enumerationLiteral" MonikerTypeName="EnumerationLiteralMoniker">
        <DomainClassMoniker Name="EnumerationLiteral" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="EnumerationLiteral/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="value">
            <DomainPropertyMoniker Name="EnumerationLiteral/Value" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="documentation">
            <DomainPropertyMoniker Name="EnumerationLiteral/Documentation" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="EnumeratedTypeHasLiterals" MonikerAttributeName="" MonikerElementName="enumeratedTypeHasLiteralsMoniker" ElementName="enumeratedTypeHasLiterals" MonikerTypeName="EnumeratedTypeHasLiteralsMoniker">
        <DomainRelationshipMoniker Name="EnumeratedTypeHasLiterals" />
      </XmlClassData>
      <XmlClassData TypeName="BaseConfigurationType" MonikerAttributeName="" MonikerElementName="baseConfigurationTypeMoniker" ElementName="baseConfigurationType" MonikerTypeName="BaseConfigurationTypeMoniker">
        <DomainClassMoniker Name="BaseConfigurationType" />
        <ElementData>
          <XmlPropertyData XmlName="typeName" Representation="Ignore">
            <DomainPropertyMoniker Name="BaseConfigurationType/TypeName" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="baseClass">
            <DomainRelationshipMoniker Name="BaseConfigurationTypeHasBaseClass" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="inheritanceModifier">
            <DomainPropertyMoniker Name="BaseConfigurationType/InheritanceModifier" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="accessModifier">
            <DomainPropertyMoniker Name="BaseConfigurationType/AccessModifier" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionModelHasConfigurationElements" MonikerAttributeName="" MonikerElementName="configurationSectionModelHasConfigurationElementsMoniker" ElementName="configurationSectionModelHasConfigurationElements" MonikerTypeName="ConfigurationSectionModelHasConfigurationElementsMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionModelHasConfigurationElements" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationElementShape" MonikerAttributeName="" MonikerElementName="configurationElementShapeMoniker" ElementName="configurationElementShape" MonikerTypeName="ConfigurationElementShapeMoniker">
        <CompartmentShapeMoniker Name="ConfigurationElementShape" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionGroup" MonikerAttributeName="" MonikerElementName="configurationSectionGroupMoniker" ElementName="configurationSectionGroup" MonikerTypeName="ConfigurationSectionGroupMoniker">
        <DomainClassMoniker Name="ConfigurationSectionGroup" />
        <ElementData>
          <XmlRelationshipData RoleElementName="configurationSectionProperties">
            <DomainRelationshipMoniker Name="ConfigurationSectionGroupHasConfigurationSectionProperties" />
          </XmlRelationshipData>
          <XmlRelationshipData RoleElementName="configurationSectionGroupProperties">
            <DomainRelationshipMoniker Name="ConfigurationSectionGroupHasConfigurationSectionGroupProperties" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionGroupShape" MonikerAttributeName="" MonikerElementName="configurationSectionGroupShapeMoniker" ElementName="configurationSectionGroupShape" MonikerTypeName="ConfigurationSectionGroupShapeMoniker">
        <CompartmentShapeMoniker Name="ConfigurationSectionGroupShape" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionProperty" MonikerAttributeName="" MonikerElementName="configurationSectionPropertyMoniker" ElementName="configurationSectionProperty" MonikerTypeName="ConfigurationSectionPropertyMoniker">
        <DomainClassMoniker Name="ConfigurationSectionProperty" />
        <ElementData>
          <XmlRelationshipData RoleElementName="containedConfigurationSection">
            <DomainRelationshipMoniker Name="ConfigurationSectionPropertyHasConfigurationSection" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="configurationSectionName" Representation="Ignore">
            <DomainPropertyMoniker Name="ConfigurationSectionProperty/ConfigurationSectionName" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionGroupHasConfigurationSectionProperties" MonikerAttributeName="" MonikerElementName="configurationSectionGroupHasConfigurationSectionPropertiesMoniker" ElementName="configurationSectionGroupHasConfigurationSectionProperties" MonikerTypeName="ConfigurationSectionGroupHasConfigurationSectionPropertiesMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionGroupHasConfigurationSectionProperties" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionPropertyHasConfigurationSection" MonikerAttributeName="" MonikerElementName="configurationSectionPropertyHasConfigurationSectionMoniker" ElementName="configurationSectionPropertyHasConfigurationSection" MonikerTypeName="ConfigurationSectionPropertyHasConfigurationSectionMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionPropertyHasConfigurationSection" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionPropertyHasConfigurationSectionConnector" MonikerAttributeName="" MonikerElementName="configurationSectionPropertyHasConfigurationSectionConnectorMoniker" ElementName="configurationSectionPropertyHasConfigurationSectionConnector" MonikerTypeName="ConfigurationSectionPropertyHasConfigurationSectionConnectorMoniker">
        <ConnectorMoniker Name="ConfigurationSectionPropertyHasConfigurationSectionConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionGroupProperty" MonikerAttributeName="" MonikerElementName="configurationSectionGroupPropertyMoniker" ElementName="configurationSectionGroupProperty" MonikerTypeName="ConfigurationSectionGroupPropertyMoniker">
        <DomainClassMoniker Name="ConfigurationSectionGroupProperty" />
        <ElementData>
          <XmlPropertyData XmlName="configurationSectionGroupName" Representation="Ignore">
            <DomainPropertyMoniker Name="ConfigurationSectionGroupProperty/ConfigurationSectionGroupName" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="containedConfigurationSectionGroup">
            <DomainRelationshipMoniker Name="ConfigurationSectionGroupPropertyHasConfigurationSectionGroup" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionGroupHasConfigurationSectionGroupProperties" MonikerAttributeName="" MonikerElementName="configurationSectionGroupHasConfigurationSectionGroupPropertiesMoniker" ElementName="configurationSectionGroupHasConfigurationSectionGroupProperties" MonikerTypeName="ConfigurationSectionGroupHasConfigurationSectionGroupPropertiesMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionGroupHasConfigurationSectionGroupProperties" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionGroupPropertyHasConfigurationSectionGroup" MonikerAttributeName="" MonikerElementName="configurationSectionGroupPropertyHasConfigurationSectionGroupMoniker" ElementName="configurationSectionGroupPropertyHasConfigurationSectionGroup" MonikerTypeName="ConfigurationSectionGroupPropertyHasConfigurationSectionGroupMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionGroupPropertyHasConfigurationSectionGroup" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionGroupHasConfigurationSectionGroupPropertiesConnector" MonikerAttributeName="" MonikerElementName="configurationSectionGroupHasConfigurationSectionGroupPropertiesConnectorMoniker" ElementName="configurationSectionGroupHasConfigurationSectionGroupPropertiesConnector" MonikerTypeName="ConfigurationSectionGroupHasConfigurationSectionGroupPropertiesConnectorMoniker">
        <ConnectorMoniker Name="ConfigurationSectionGroupHasConfigurationSectionGroupPropertiesConnector" />
      </XmlClassData>
      <XmlClassData TypeName="PropertyValidators" MonikerAttributeName="" MonikerElementName="validatorsMoniker" ElementName="validators" MonikerTypeName="PropertyValidatorsMoniker">
        <DomainClassMoniker Name="PropertyValidators" />
        <ElementData>
          <XmlRelationshipData OmitElement="true" RoleElementName="validators">
            <DomainRelationshipMoniker Name="PropertyValidatorsHasValidators" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionModelHasPropertyValidators" MonikerAttributeName="" MonikerElementName="configurationSectionModelHasPropertyValidatorsMoniker" ElementName="configurationSectionModelHasPropertyValidators" MonikerTypeName="ConfigurationSectionModelHasPropertyValidatorsMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionModelHasPropertyValidators" />
      </XmlClassData>
      <XmlClassData TypeName="PropertyValidator" MonikerAttributeName="name" MonikerElementName="validatorMoniker" ElementName="validator" MonikerTypeName="PropertyValidatorMoniker">
        <DomainClassMoniker Name="PropertyValidator" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="PropertyValidator/Name" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="PropertyValidatorsHasValidators" MonikerAttributeName="" MonikerElementName="propertyValidatorsHasValidatorsMoniker" ElementName="propertyValidatorsHasValidators" MonikerTypeName="PropertyValidatorsHasValidatorsMoniker">
        <DomainRelationshipMoniker Name="PropertyValidatorsHasValidators" />
      </XmlClassData>
      <XmlClassData TypeName="CallbackValidator" MonikerAttributeName="" MonikerElementName="callbackValidatorMoniker" ElementName="callbackValidator" MonikerTypeName="CallbackValidatorMoniker">
        <DomainClassMoniker Name="CallbackValidator" />
        <ElementData>
          <XmlPropertyData XmlName="callback">
            <DomainPropertyMoniker Name="CallbackValidator/Callback" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="NumberValidator" MonikerAttributeName="" MonikerElementName="numberValidatorMoniker" ElementName="numberValidator" MonikerTypeName="NumberValidatorMoniker">
        <DomainClassMoniker Name="NumberValidator" />
        <ElementData>
          <XmlPropertyData XmlName="excludeRange">
            <DomainPropertyMoniker Name="NumberValidator/ExcludeRange" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="IntegerValidator" MonikerAttributeName="" MonikerElementName="integerValidatorMoniker" ElementName="integerValidator" MonikerTypeName="IntegerValidatorMoniker">
        <DomainClassMoniker Name="IntegerValidator" />
        <ElementData>
          <XmlPropertyData XmlName="maxValue">
            <DomainPropertyMoniker Name="IntegerValidator/MaxValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="minValue">
            <DomainPropertyMoniker Name="IntegerValidator/MinValue" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="LongValidator" MonikerAttributeName="" MonikerElementName="longValidatorMoniker" ElementName="longValidator" MonikerTypeName="LongValidatorMoniker">
        <DomainClassMoniker Name="LongValidator" />
        <ElementData>
          <XmlPropertyData XmlName="maxValue">
            <DomainPropertyMoniker Name="LongValidator/MaxValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="minValue">
            <DomainPropertyMoniker Name="LongValidator/MinValue" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="PositiveTimeSpanValidator" MonikerAttributeName="" MonikerElementName="positiveTimeSpanValidatorMoniker" ElementName="positiveTimeSpanValidator" MonikerTypeName="PositiveTimeSpanValidatorMoniker">
        <DomainClassMoniker Name="PositiveTimeSpanValidator" />
      </XmlClassData>
      <XmlClassData TypeName="TimeSpanValidator" MonikerAttributeName="" MonikerElementName="timeSpanValidatorMoniker" ElementName="timeSpanValidator" MonikerTypeName="TimeSpanValidatorMoniker">
        <DomainClassMoniker Name="TimeSpanValidator" />
        <ElementData>
          <XmlPropertyData XmlName="maxValue">
            <DomainPropertyMoniker Name="TimeSpanValidator/MaxValue" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="minValue">
            <DomainPropertyMoniker Name="TimeSpanValidator/MinValue" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="RegexStringValidator" MonikerAttributeName="" MonikerElementName="regexStringValidatorMoniker" ElementName="regexStringValidator" MonikerTypeName="RegexStringValidatorMoniker">
        <DomainClassMoniker Name="RegexStringValidator" />
        <ElementData>
          <XmlPropertyData XmlName="regularExpression">
            <DomainPropertyMoniker Name="RegexStringValidator/RegularExpression" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationPropertyHasPropertyValidator" MonikerAttributeName="" MonikerElementName="validatorReferenceMoniker" ElementName="validatorReference" MonikerTypeName="ConfigurationPropertyHasPropertyValidatorMoniker">
        <DomainRelationshipMoniker Name="ConfigurationPropertyHasPropertyValidator" />
      </XmlClassData>
      <XmlClassData TypeName="StringValidator" MonikerAttributeName="" MonikerElementName="stringValidatorMoniker" ElementName="stringValidator" MonikerTypeName="StringValidatorMoniker">
        <DomainClassMoniker Name="StringValidator" />
        <ElementData>
          <XmlPropertyData XmlName="invalidCharacters">
            <DomainPropertyMoniker Name="StringValidator/InvalidCharacters" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="maxLength">
            <DomainPropertyMoniker Name="StringValidator/MaxLength" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="minLength">
            <DomainPropertyMoniker Name="StringValidator/MinLength" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="CustomTypeConverter" MonikerAttributeName="name" MonikerElementName="converterMoniker" ElementName="converter" MonikerTypeName="CustomTypeConverterMoniker">
        <DomainClassMoniker Name="CustomTypeConverter" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="CustomTypeConverter/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="documentation">
            <DomainPropertyMoniker Name="CustomTypeConverter/Documentation" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="type">
            <DomainRelationshipMoniker Name="CustomTypeConverterHasType" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionModelHasCustomTypeConverters" MonikerAttributeName="" MonikerElementName="configurationSectionModelHasCustomTypeConvertersMoniker" ElementName="configurationSectionModelHasCustomTypeConverters" MonikerTypeName="ConfigurationSectionModelHasCustomTypeConvertersMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionModelHasCustomTypeConverters" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationPropertyReferencesCustomTypeConverter" MonikerAttributeName="" MonikerElementName="configurationPropertyReferencesCustomTypeConverterMoniker" ElementName="configurationPropertyReferencesCustomTypeConverter" MonikerTypeName="ConfigurationPropertyReferencesCustomTypeConverterMoniker">
        <DomainRelationshipMoniker Name="ConfigurationPropertyReferencesCustomTypeConverter" />
      </XmlClassData>
      <XmlClassData TypeName="TypeBase" MonikerAttributeName="name" MonikerElementName="typeBaseMoniker" ElementName="typeBase" MonikerTypeName="TypeBaseMoniker">
        <DomainClassMoniker Name="TypeBase" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TypeBase/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="namespace">
            <DomainPropertyMoniker Name="TypeBase/Namespace" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="CustomTypeConverterHasType" MonikerAttributeName="" MonikerElementName="customTypeConverterHasTypeMoniker" ElementName="customTypeConverterHasType" MonikerTypeName="CustomTypeConverterHasTypeMoniker">
        <DomainRelationshipMoniker Name="CustomTypeConverterHasType" />
      </XmlClassData>
      <XmlClassData TypeName="BaseConfigurationTypeHasBaseClass" MonikerAttributeName="" MonikerElementName="baseConfigurationTypeHasBaseClassMoniker" ElementName="baseConfigurationTypeHasBaseClass" MonikerTypeName="BaseConfigurationTypeHasBaseClassMoniker">
        <DomainRelationshipMoniker Name="BaseConfigurationTypeHasBaseClass" />
      </XmlClassData>
      <XmlClassData TypeName="InheritanceConnector" MonikerAttributeName="" MonikerElementName="inheritanceConnectorMoniker" ElementName="inheritanceConnector" MonikerTypeName="InheritanceConnectorMoniker">
        <ConnectorMoniker Name="InheritanceConnector" />
      </XmlClassData>
      <XmlClassData TypeName="Attribute" MonikerAttributeName="name" MonikerElementName="attributeMoniker" ElementName="attribute" MonikerTypeName="AttributeMoniker">
        <DomainClassMoniker Name="Attribute" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="Attribute/Name" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="parameters">
            <DomainRelationshipMoniker Name="AttributeHasParameters" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AttributeParameter" MonikerAttributeName="name" MonikerElementName="parameterMoniker" ElementName="parameter" MonikerTypeName="parameterMoniker">
        <DomainClassMoniker Name="AttributeParameter" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="AttributeParameter/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="value">
            <DomainPropertyMoniker Name="AttributeParameter/Value" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AttributeHasParameters" MonikerAttributeName="" MonikerElementName="attributeHasParametersMoniker" ElementName="attributeHasParameters" MonikerTypeName="AttributeHasParametersMoniker">
        <DomainRelationshipMoniker Name="AttributeHasParameters" />
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationPropertyHasAttributes" MonikerAttributeName="" MonikerElementName="configurationPropertyHasAttributesMoniker" ElementName="configurationPropertyHasAttributes" MonikerTypeName="ConfigurationPropertyHasAttributesMoniker">
        <DomainRelationshipMoniker Name="ConfigurationPropertyHasAttributes" />
      </XmlClassData>
      <XmlClassData TypeName="CommentBoxShape" MonikerAttributeName="" MonikerElementName="commentBoxShapeMoniker" ElementName="commentBoxShape" MonikerTypeName="CommentBoxShapeMoniker">
        <GeometryShapeMoniker Name="CommentBoxShape" />
      </XmlClassData>
      <XmlClassData TypeName="Comment" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentMoniker" ElementName="comment" MonikerTypeName="CommentMoniker">
        <DomainClassMoniker Name="Comment" />
        <ElementData>
          <XmlPropertyData XmlName="text">
            <DomainPropertyMoniker Name="Comment/Text" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="subjects">
            <DomainRelationshipMoniker Name="CommentsReferenceConfigurationItems" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConfigurationSectionModelHasComments" MonikerAttributeName="" SerializeId="true" MonikerElementName="configurationSectionModelHasCommentsMoniker" ElementName="configurationSectionModelHasComments" MonikerTypeName="ConfigurationSectionModelHasCommentsMoniker">
        <DomainRelationshipMoniker Name="ConfigurationSectionModelHasComments" />
      </XmlClassData>
      <XmlClassData TypeName="CommentLink" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentLinkMoniker" ElementName="commentLink" MonikerTypeName="CommentLinkMoniker">
        <ConnectorMoniker Name="CommentLink" />
      </XmlClassData>
      <XmlClassData TypeName="CommentsReferenceConfigurationItems" MonikerAttributeName="" SerializeId="true" MonikerElementName="commentsReferenceConfigurationItemsMoniker" ElementName="commentsReferenceConfigurationItems" MonikerTypeName="CommentsReferenceConfigurationItemsMoniker">
        <DomainRelationshipMoniker Name="CommentsReferenceConfigurationItems" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="ConfigurationSectionDesignerExplorer">
    <CustomNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\ConfigurationElement.bmp" ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="ConfigurationElement" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\ConfigurationElementCollection.bmp" ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="ConfigurationElementCollection" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\ConfigurationSection.bmp" ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="ConfigurationSection" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\EnumerationLiteral.bmp">
        <Class>
          <DomainClassMoniker Name="EnumerationLiteral" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings>
        <Class>
          <DomainClassMoniker Name="AttributeProperty" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings>
        <Class>
          <DomainClassMoniker Name="ElementProperty" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\EnumeratedType.bmp">
        <Class>
          <DomainClassMoniker Name="EnumeratedType" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\ExternalType.bmp">
        <Class>
          <DomainClassMoniker Name="ExternalType" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\ConfigurationSectionGroup.bmp" ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="ConfigurationSectionGroup" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\ConfigurationSection.bmp">
        <Class>
          <DomainClassMoniker Name="ConfigurationSectionModel" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\Validators.bmp">
        <Class>
          <DomainClassMoniker Name="PropertyValidators" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\Validator.bmp" ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="PropertyValidator" />
        </Class>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\Converter.bmp">
        <Class>
          <DomainClassMoniker Name="CustomTypeConverter" />
        </Class>
      </ExplorerNodeSettings>
    </CustomNodeSettings>
  </ExplorerBehavior>
  <ConnectionBuilders>
    <ConnectionBuilder Name="ConfigurationElementCollectionHasItemTypeBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ConfigurationElementCollectionHasItemType" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationElementCollection" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ElementPropertyHasTypeBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ElementPropertyHasType" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ElementProperty" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="AttributePropertyHasPropertyTypeBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="AttributePropertyHasPropertyType" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="AttributeProperty" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TypeDefinition" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ConfigurationSectionPropertyHasConfigurationSectionBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ConfigurationSectionPropertyHasConfigurationSection" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationSectionProperty" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationSection" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ConfigurationSectionGroupPropertyHasConfigurationSectionGroupBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ConfigurationSectionGroupPropertyHasConfigurationSectionGroup" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationSectionGroupProperty" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationSectionGroup" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ConfigurationPropertyHasPropertyValidatorBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ConfigurationPropertyHasPropertyValidator" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationProperty" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="PropertyValidator" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ConfigurationPropertyReferencesCustomTypeConverterBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ConfigurationPropertyReferencesCustomTypeConverter" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConfigurationProperty" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="CustomTypeConverter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="CustomTypeConverterHasTypeBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="CustomTypeConverterHasType" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="CustomTypeConverter" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TypeBase" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="BaseConfigurationTypeHasBaseClassBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="BaseConfigurationTypeHasBaseClass" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="BaseConfigurationType" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="BaseConfigurationType" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="CommentsReferenceConfigurationItemsBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="CommentsReferenceConfigurationItems" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Comment" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="BaseConfigurationType" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="cf255b73-e2b7-4007-bc5d-80d166ffda9c" Description="The diagram representing a Configuration Section" Name="ConfigurationSectionDesignerDiagram" DisplayName="Configuration Section Diagram" Namespace="ConfigurationSectionDesigner" GeneratesDoubleDerived="true">
    <Class>
      <DomainClassMoniker Name="ConfigurationSectionModel" />
    </Class>
    <ShapeMaps>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ConfigurationElement" />
        <ParentElementPath>
          <DomainPath>ConfigurationSectionModelHasConfigurationElements.ConfigurationSectionModel/!ConfigurationSectionModel</DomainPath>
        </ParentElementPath>
        <CompartmentShapeMoniker Name="ConfigurationElementShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="ConfigurationElementShape/AttributeProperties" />
          <ElementsDisplayed>
            <DomainPath>ConfigurationElementHasAttributeProperties.AttributeProperties/!AttributeProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConfigurationProperty/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="ConfigurationElementShape/ElementProperties" />
          <ElementsDisplayed>
            <DomainPath>ConfigurationElementHasElementProperties.ElementProperties/!ElementProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConfigurationProperty/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ConfigurationSection" />
        <ParentElementPath>
          <DomainPath>ConfigurationSectionModelHasConfigurationElements.ConfigurationSectionModel/!ConfigurationSectionModel</DomainPath>
        </ParentElementPath>
        <CompartmentShapeMoniker Name="ConfigurationSectionShape" />
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ConfigurationElementCollection" />
        <ParentElementPath>
          <DomainPath>ConfigurationSectionModelHasConfigurationElements.ConfigurationSectionModel/!ConfigurationSectionModel</DomainPath>
        </ParentElementPath>
        <CompartmentShapeMoniker Name="ConfigurationElementCollectionShape" />
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="BaseConfigurationType" />
        <ParentElementPath>
          <DomainPath>ConfigurationSectionModelHasConfigurationElements.ConfigurationSectionModel/!ConfigurationSectionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConfigurationShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TypeBase/Name" />
            </PropertyPath>
          </PropertyDisplayed>
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="BaseConfigurationType/InheritanceModifier" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="None" />
              <PropertyFilter FilteringValue="Sealed" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConfigurationShape/DescriptionDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="BaseConfigurationType/TypeName" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ConfigurationShape/IconDecoratorConfigurationElement" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="BaseConfigurationType/TypeName" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="ConfigurationElement" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ConfigurationShape/IconDecoratorConfigurationElementCollection" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="BaseConfigurationType/TypeName" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="ConfigurationElementCollection" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ConfigurationShape/IconDecoratorConfigurationSection" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="BaseConfigurationType/TypeName" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="ConfigurationSection" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="ConfigurationShape/IconDecoratorConfigurationSectionGroup" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="BaseConfigurationType/TypeName" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="ConfigurationSectionGroup" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConfigurationShape/AbstractNameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TypeBase/Name" />
            </PropertyPath>
          </PropertyDisplayed>
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="BaseConfigurationType/InheritanceModifier" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="Abstract" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="ConfigurationShape" />
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="ConfigurationSectionGroup" />
        <ParentElementPath>
          <DomainPath>ConfigurationSectionModelHasConfigurationElements.ConfigurationSectionModel/!ConfigurationSectionModel</DomainPath>
        </ParentElementPath>
        <CompartmentShapeMoniker Name="ConfigurationSectionGroupShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="ConfigurationSectionGroupShape/ConfigurationSectionProperties" />
          <ElementsDisplayed>
            <DomainPath>ConfigurationSectionGroupHasConfigurationSectionProperties.ConfigurationSectionProperties/!ConfigurationSectionProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConfigurationSectionProperty/ConfigurationSectionName" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
        <CompartmentMap>
          <CompartmentMoniker Name="ConfigurationSectionGroupShape/ConfigurationSectionGroupProperties" />
          <ElementsDisplayed>
            <DomainPath>ConfigurationSectionGroupHasConfigurationSectionGroupProperties.ConfigurationSectionGroupProperties/!ConfigurationSectionGroupProperty</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConfigurationSectionGroupProperty/ConfigurationSectionGroupName" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Comment" />
        <ParentElementPath>
          <DomainPath>ConfigurationSectionModelHasComments.ConfigurationSectionModel/!ConfigurationSectionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CommentBoxShape/Comment" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Comment/Text" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="CommentBoxShape" />
      </ShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap ConnectsCustomSource="true">
        <ConnectorMoniker Name="ElementPropertyHasTypeConnector" />
        <DomainRelationshipMoniker Name="ElementPropertyHasType" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="ElementPropertyHasTypeConnector/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConfigurationProperty/Name" />
              <DomainPath>ElementPropertyHasType!ElementProperty</DomainPath>
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="ConfigurationElementCollectionHasItemTypeConnector" />
        <DomainRelationshipMoniker Name="ConfigurationElementCollectionHasItemType" />
      </ConnectorMap>
      <ConnectorMap ConnectsCustomSource="true">
        <ConnectorMoniker Name="ConfigurationSectionPropertyHasConfigurationSectionConnector" />
        <DomainRelationshipMoniker Name="ConfigurationSectionPropertyHasConfigurationSection" />
      </ConnectorMap>
      <ConnectorMap ConnectsCustomSource="true">
        <ConnectorMoniker Name="ConfigurationSectionGroupHasConfigurationSectionGroupPropertiesConnector" />
        <DomainRelationshipMoniker Name="ConfigurationSectionGroupPropertyHasConfigurationSectionGroup" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="InheritanceConnector" />
        <DomainRelationshipMoniker Name="BaseConfigurationTypeHasBaseClass" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="CommentLink" />
        <DomainRelationshipMoniker Name="CommentsReferenceConfigurationItems" />
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="csd" Icon="Resources\ConfigurationSectionDesigner.ico" EditorGuid="548a5f67-a0b1-4c78-84f0-508456f45157">
    <RootClass>
      <DomainClassMoniker Name="ConfigurationSectionModel" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="ConfigurationSectionDesignerSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="Configuration Section Designer">
      <ElementTool Name="ConfigurationSection" ToolboxIcon="Resources\ConfigurationSection.bmp" Caption="Configuration Section" Tooltip="Defines a Configuration Section" HelpKeyword="ConfigurationSection">
        <DomainClassMoniker Name="ConfigurationSection" />
      </ElementTool>
      <ElementTool Name="ConfigurationElement" ToolboxIcon="Resources\ConfigurationElement.bmp" Caption="Configuration Element" Tooltip="Defines a Configuration Element" HelpKeyword="ConfigurationElement">
        <DomainClassMoniker Name="ConfigurationElement" />
      </ElementTool>
      <ElementTool Name="ConfigurationElementCollection" ToolboxIcon="Resources\ConfigurationElementCollection.bmp" Caption="Configuration Element Collection" Tooltip="Defines a collection of Configuration Elements" HelpKeyword="ConfigurationElementCollection">
        <DomainClassMoniker Name="ConfigurationElementCollection" />
      </ElementTool>
      <ConnectionTool Name="CollectionItemType" ToolboxIcon="Resources\CollectionItemType.bmp" Caption="Collection Item Type" Tooltip="Defines the item type of a collection by linking a Configuration Element Collection to its Configuration Element item type" HelpKeyword="CollectionItemType">
        <ConnectionBuilderMoniker Name="ConfigurationSectionDesigner/ConfigurationElementCollectionHasItemTypeBuilder" />
      </ConnectionTool>
      <ElementTool Name="ConfigurationSectionGroup" ToolboxIcon="Resources\ConfigurationSectionGroup.bmp" Caption="Configuration Section Group" Tooltip="Defines a Configuration Section Group. Groups allow you to organize configuration sections into logical groups in the XML. When accessing sections that are part of groups via code, the section will be accessible in the root namespace (nesting of groups will not be represented in code behind). When a section in the group is accessed, the code behind will properly send the full XML path (with groups included) to the configuration manager during lookup." HelpKeyword="ConfigurationSectionGroup">
        <DomainClassMoniker Name="ConfigurationSectionGroup" />
      </ElementTool>
      <ElementTool Name="Comment" ToolboxIcon="Resources\EnumerationLiteral.bmp" Caption="Comment" Tooltip="Add a Comment box to the diagram. Comments will only be shown in the diagram and will not be part of any generated code." HelpKeyword="ConnectCommentF1Keyword">
        <DomainClassMoniker Name="Comment" />
      </ElementTool>
      <ConnectionTool Name="CommentConnector" ToolboxIcon="Resources\CollectionItemType.bmp" Caption="Comment Connector" Tooltip="Connect a Comment to its subject(s)." HelpKeyword="ConnectCommentsReferenceTypesF1Keyword">
        <ConnectionBuilderMoniker Name="ConfigurationSectionDesigner/CommentsReferenceConfigurationItemsBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <Validation UsesMenu="true" UsesOpen="true" UsesSave="true" UsesLoad="false" />
    <DiagramMoniker Name="ConfigurationSectionDesignerDiagram" />
  </Designer>
  <Explorer ExplorerGuid="b5896815-b473-4bf5-b478-fe2b4252eb59" Title="Configuration Section Explorer">
    <ExplorerBehaviorMoniker Name="ConfigurationSectionDesigner/ConfigurationSectionDesignerExplorer" />
  </Explorer>
</Dsl>