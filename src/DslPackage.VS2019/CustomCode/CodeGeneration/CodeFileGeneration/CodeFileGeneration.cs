using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.VisualStudio.Modeling;
using System.Diagnostics;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.CodeDom;
using System.Reflection;

namespace ConfigurationSectionDesigner
{
    /// <summary>
    /// This class is composed of multiple partial class files, whose purpose is to 
    /// generate the csharp (cs) class files representing the config schema.
    /// </summary>
    internal partial class CodeFileGenerator
    {
        #region Method-shared variables

        private CodeDomProvider _codeDomProvider;
        private ICodeGenerator _generator;
        private CodeGeneratorOptions _options = new CodeGeneratorOptions
        {
            BlankLinesBetweenMembers = true,
            BracingStyle = "C",
            VerbatimOrder = true
        };
        private MemoryStream _codeStream;
        private StreamWriter _codeWriter;
        private CodeAttributeDeclaration _generatedCodeAttribute =
            new CodeAttributeDeclaration(
                GlobalReference(typeof(GeneratedCodeAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(typeof(CsdFileGenerator).FullName)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(CsdFileGenerator.ToolVersion))
            );
        private string _rootNamespace;

        #endregion

        #region Convenient code generation types

        #region Primitive types

        CodeTypeReference _void = GlobalReference(typeof(void));

        CodeTypeReference _string = GlobalReference(typeof(string));
        CodeTypeReference _int = GlobalReference(typeof(int));
        CodeTypeReference _bool = GlobalReference(typeof(bool));
        CodeTypeReference _object = GlobalReference(typeof(object));

        #endregion

        CodeExpression _this = new CodeThisReferenceExpression();
        CodeExpression _base = new CodeBaseReferenceExpression();

        CodeExpression _true = new CodePrimitiveExpression(true);
        CodeExpression _false = new CodePrimitiveExpression(false);

        #endregion

        public CodeFileGenerator(CodeDomProvider codeDomProvider, string rootNamespace)
        {
            _codeDomProvider = codeDomProvider;
            _rootNamespace = rootNamespace;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// This is ONLY called for "-gen" files.
        /// </remarks>
        /// <param name="inputFilePath"></param>
        /// <returns></returns>
        public byte[] GenerateCode( string inputFilePath )
        {
            using (Store store = new Store(typeof(CoreDesignSurfaceDomainModel), typeof(ConfigurationSectionDesignerDomainModel)))
            {
                // Prepare the model
                ConfigurationSectionModel model = PrepareModel(inputFilePath, store);

                // Prepare code generator
                CodeCompileUnit generationUnit = PrepareCodeGenerator();

                // Generate code for the configuration elements
                foreach (BaseConfigurationType type in model.ConfigurationElements)
                {
                    // Ignore any ConfigurationType that is not an instance of
                    // the ConfigurationElement.
                    ConfigurationElement element = type as ConfigurationElement;
                    if (element == null) continue;

                    // Create namespace declaration
                    CodeNamespace elementNamespace = new CodeNamespace( element.ActualNamespace );

                    // 20141124: Code block below might be useful for cleaning up the generated code a bit. Useful for when one 
                    // needs to examine/debug generated code. Might be enabled as option, or based on user feedback.
                    /*
                    elementNamespace.Imports.Add(new CodeNamespaceImport("System.Configuration"));
                    elementNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));
                    //elementNamespace.Imports.Add(new CodeNamespaceImport("System.CodeDom.Compiler"));
                    */

                    generationUnit.Namespaces.Add( elementNamespace );

                    // Create the element class and set common options
                    CodeTypeDeclaration elementClass = new CodeTypeDeclaration(element.Name);
                    elementNamespace.Types.Add(elementClass);
                    elementClass.Comments.Add(DocComment("<summary>"));
                    elementClass.Comments.Add(DocComment(EscapeStringChars(element.DocumentationText)));
                    elementClass.Comments.Add(DocComment("</summary>"));
                    elementClass.TypeAttributes = (element.AccessModifier == AccessModifiers.Public) ? TypeAttributes.Public : TypeAttributes.NotPublic;
                    if (element.InheritanceModifier == InheritanceModifiers.Abstract)
                        elementClass.TypeAttributes |= TypeAttributes.Abstract;
                    else if (element.InheritanceModifier == InheritanceModifiers.Sealed)
                        elementClass.TypeAttributes |= TypeAttributes.Sealed;
                    elementClass.IsPartial = true;
                    elementClass.IsClass = true;
                   
                    if (element.BaseClass != null)
                        elementClass.BaseTypes.Add(GlobalSelfReference(element.BaseClass.FullName));

                    GenerateDescriptionAndDisplayNameCode(element, elementClass);

                    // Set element-type specific options
                    if (element is ConfigurationSection)
                    {
                        // Set base type to be ConfigurationSection
                        if (element.BaseClass == null)
                            elementClass.BaseTypes.Add(GlobalReference(typeof(System.Configuration.ConfigurationSection)));

                        // Do custom ConfigurationElementCollection code generation
                        GenerateConfigurationSectionCode(element, elementClass);
                    }
                    else if (element is ConfigurationElementCollection)
                    {
                        // Set base type to be ConfigurationElementCollection
                        if (element.BaseClass == null)
                            elementClass.BaseTypes.Add(GlobalReference(typeof(System.Configuration.ConfigurationElementCollection)));

                        // Do custom ConfigurationElementCollection code generation
                        GenerateConfigurationElementCollectionCode(element, elementClass);
                    }
                    else if (element is ConfigurationElement)
                    {
                        // Set base type to be ConfigurationElement
                        if (element.BaseClass == null)
                            elementClass.BaseTypes.Add(GlobalReference(typeof(System.Configuration.ConfigurationElement)));
                    }

                    // Set IsReadOnly setting
                    CodeMemberMethod isReadOnlyMethod = new CodeMemberMethod();
                    elementClass.Members.Add(isReadOnlyMethod);
                    isReadOnlyMethod.StartDirectives.Add(Region("IsReadOnly override"));
                    isReadOnlyMethod.Comments.Add(DocComment("<summary>"));
                    isReadOnlyMethod.Comments.Add(DocComment("Gets a value indicating whether the element is read-only."));
                    isReadOnlyMethod.Comments.Add(DocComment("</summary>"));
                    isReadOnlyMethod.CustomAttributes.Add(_generatedCodeAttribute);
                    isReadOnlyMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                    isReadOnlyMethod.ReturnType = _bool;
                    isReadOnlyMethod.Name = "IsReadOnly";
                    isReadOnlyMethod.Statements.Add(
                        new CodeMethodReturnStatement(
                            element.IsReadOnly ? _true : _false
                        )
                    );
                    isReadOnlyMethod.EndDirectives.Add(EndRegion());

                    // Add all the element's properties
                    foreach (ConfigurationProperty property in element.Properties)
                    {
                        GenerateProperty(element, elementClass, property);
                    }

                    if (element.HasCustomChildElements)
                    {
                        GenerateCustomChildElementHandler(elementClass);
                    }
                }

                foreach (TypeDefinition type in model.TypeDefinitions)
                {
                    EnumeratedType enumType = type as EnumeratedType;
                    if (enumType != null && ((enumType.CodeGenOptions & TypeDefinitionCodeGenOptions.TypeDefinition) == TypeDefinitionCodeGenOptions.TypeDefinition))
                    {
                        // Create the namespace block
                        CodeNamespace typeNamespace = new CodeNamespace(enumType.Namespace);
                        generationUnit.Namespaces.Add(typeNamespace);

                        // Create enum
                        CodeTypeDeclaration enumTypeDeclaration = new CodeTypeDeclaration(enumType.Name);
                        typeNamespace.Types.Add(enumTypeDeclaration);
                        enumTypeDeclaration.Comments.Add(DocComment("<summary>"));
                        if (string.IsNullOrEmpty(enumType.Documentation))
                            // If the enum has no documentation, just make the documentation the name of the enum value
                            enumTypeDeclaration.Comments.Add(DocComment(string.Format("{0}.", enumType.Name)));
                        else
                            enumTypeDeclaration.Comments.Add(DocComment(EscapeStringChars(enumType.Documentation)));
                        enumTypeDeclaration.Comments.Add(DocComment("</summary>"));
                        enumTypeDeclaration.CustomAttributes.Add(_generatedCodeAttribute);
                        if (enumType.IsFlags)
                            enumTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(GlobalReference(typeof(FlagsAttribute))));
                        enumTypeDeclaration.Attributes = MemberAttributes.Public;
                        enumTypeDeclaration.IsEnum = true;

                        foreach (EnumerationLiteral literal in enumType.Literals)
                        {
                            CodeMemberField enumField = new CodeMemberField();
                            enumTypeDeclaration.Members.Add(enumField);
                            enumField.Comments.Add(DocComment("<summary>"));
                            if (string.IsNullOrEmpty(literal.Documentation))
                                enumField.Comments.Add(DocComment(string.Format("{0}.", literal.Name)));
                            else
                                enumField.Comments.Add(DocComment(EscapeStringChars(literal.Documentation)));
                            enumField.Comments.Add(DocComment("</summary>"));
                            enumField.Name = literal.Name;
                            if (!string.IsNullOrEmpty(literal.Value))
                                enumField.InitExpression = new CodeSnippetExpression(literal.Value);
                        }
                    }
                }

                if (model.PropertyValidators.Validators.Count > 0)
                {
                    GenerateValidators(model, generationUnit);
                }

                if (model.CustomTypeConverters.Count > 0)
                {
                    GenerateCustomConverters(model, generationUnit);
                }

                _generator.GenerateCodeFromCompileUnit(generationUnit, _codeWriter, _options);

                _codeWriter.Flush();
                byte[] output = new byte[_codeStream.Length];
                Array.Copy(_codeStream.GetBuffer(), 0, output, 0, _codeStream.Length);
                return output;
            }
        }

        #region Helper methods

        private static ConfigurationSectionModel PrepareModel(string inputFilePath, Store store)
        {
            ConfigurationSectionModel model;
            using (Transaction transaction = store.TransactionManager.BeginTransaction("Load", true))
            {
                SerializationResult serializationResult = new SerializationResult();
                model = ConfigurationSectionDesignerSerializationHelper.Instance.LoadModel(serializationResult, store, inputFilePath, null, null, null);

                //model = ConfigurationSectionDesignerSerializationHelper.Instance.LoadModel( serializationResult, store, inputFilePath, null, null );

                if (serializationResult.Failed)
                {
                    throw new SerializationException(serializationResult);
                }

                transaction.Commit();
            }
            return model;
        }

        private CodeCompileUnit PrepareCodeGenerator()
        {
            _codeStream = new MemoryStream();
            _codeWriter = new StreamWriter(_codeStream);
            _generator = _codeDomProvider.CreateGenerator(_codeWriter);
            CodeCompileUnit generationUnit = new CodeCompileUnit();
            return generationUnit;
        }

        #endregion

        private void GenerateDescriptionAndDisplayNameCode(ConfigurationElement element, CodeTypeDeclaration elementClass)
        {
            if (!string.IsNullOrEmpty(element.Documentation))
                elementClass.CustomAttributes.Add(new CodeAttributeDeclaration(GlobalReference(typeof(System.ComponentModel.DescriptionAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(EscapeStringChars(element.Documentation)))));

            if (!string.IsNullOrEmpty(element.DisplayName))
                elementClass.CustomAttributes.Add(new CodeAttributeDeclaration(GlobalReference(typeof(System.ComponentModel.DisplayNameAttribute)), new CodeAttributeArgument(new CodePrimitiveExpression(EscapeStringChars(element.DisplayName)))));
        }
    }
}
