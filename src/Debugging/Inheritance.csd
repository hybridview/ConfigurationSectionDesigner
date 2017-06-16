<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="e0c780c9-e551-4178-aaae-5526426e7d8c" namespace="Inherit" xmlSchemaNamespace="inherit" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="SchoolRegistrySection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="schoolRegistrySection">
      <baseClass>
        <configurationSectionMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/AbstractSection" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="SchoolName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="schoolName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Professors" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="professors" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Professors" />
          </type>
        </elementProperty>
        <elementProperty name="Students" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Students" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="NamedElement" inheritanceModifier="Abstract">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="Person" inheritanceModifier="Abstract">
      <baseClass>
        <configurationElementMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/NamedElement" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="YearOfBirth" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="yearOfBirth" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="YearOfDeath" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="yearOfDeath" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="Student">
      <baseClass>
        <configurationElementMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Person" />
      </baseClass>
      <elementProperties>
        <elementProperty name="Grades" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="grades" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Grades" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="Grades" xmlItemName="grade" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Grade" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Grade">
      <attributeProperties>
        <attributeProperty name="GradeValue" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="grade" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Class" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="class" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="Professor">
      <baseClass>
        <configurationElementMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Person" />
      </baseClass>
      <elementProperties>
        <elementProperty name="Students" isRequired="false" isKey="false" isDefaultCollection="true" xmlName="" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Students" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="Students" xmlItemName="student" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Student" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="Professors" xmlItemName="professor" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/Professor" />
      </itemType>
    </configurationElementCollection>
    <configurationSection name="AbstractSection" codeGenOptions="Singleton" xmlSectionName="abstractSection">
      <attributeProperties>
        <attributeProperty name="AbstractProperty" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="abstractProperty" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e0c780c9-e551-4178-aaae-5526426e7d8c/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationSection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>