using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace ConfigurationSectionDesigner
{
    internal partial class CodeFileGenerator
    {
        private void GenerateConfigurationElementCollectionCode(ConfigurationElement element, CodeTypeDeclaration elementClass)
        {
            ConfigurationElementCollection collectionElement = (ConfigurationElementCollection)element;
            ConfigurationProperty keyProperty = null;
 
			if (collectionElement.ItemType.KeyProperties.Count == 1)
			{
				keyProperty = collectionElement.ItemType.KeyProperties.Single();
			}

            string xmlPropertyName = string.Format( "{0}PropertyName", collectionElement.ItemType.Name );

            string configurationElementTypeReferenceForDoc = GetTypeReferenceString(typeof(System.Configuration.ConfigurationElement));
            string configurationElementCollectionTypeReferenceForDoc = GetTypeReferenceString(typeof(System.Configuration.ConfigurationElementCollection));

            // Any cref's with "global::" in them cause intellisense errors. This brings up another issue: Why are we using global:: for our custom classes too?
            // REMOVED until bigger picture is looked at: I changed the name to add "ForDoc" at the end to prevent any confusion.
            //string configurationElementTypeReferenceForDoc = FormatTypeReferenceStringForDoc(GetTypeReferenceString(typeof(System.Configuration.ConfigurationElement)));
            //string configurationElementCollectionTypeReferenceForDoc = FormatTypeReferenceStringForDoc(GetTypeReferenceString(typeof(System.Configuration.ConfigurationElementCollection)));
           
            CodeTypeReference collectionItemType = GlobalSelfReference( collectionElement.ItemType.FullName );
            string collectionItemTypeReference = GetTypeSelfReferenceString( collectionElement.ItemType.FullName );

            CodeTypeReference icollectionTypeReference = null;
            if ((collectionElement.CodeGenOptions & ConfigurationElementCollectionCodeGenOptions.ICollection) == ConfigurationElementCollectionCodeGenOptions.ICollection) 
            {
                icollectionTypeReference = new CodeTypeReference(string.Concat("global::System.Collections.Generic.ICollection<", collectionItemTypeReference, ">"));
                elementClass.BaseTypes.Add(icollectionTypeReference);
            }

            // Add ConfigurationCollectionAttribute
            CodeAttributeDeclaration collectionType =
                new CodeAttributeDeclaration(
                    GlobalReference( typeof( System.Configuration.ConfigurationCollectionAttribute ) ),
                    new CodeAttributeArgument( _selftypeof( collectionElement.ItemType.FullName ) ),
                    new CodeAttributeArgument( "CollectionType",
                        new CodeFieldReferenceExpression(
                            GlobalReferenceExpression( collectionElement.CollectionType.GetType() ),
                            Enum.GetName( collectionElement.CollectionType.GetType(), collectionElement.CollectionType )
                        )
                    )
                
            );

            
if( collectionElement.CollectionType == System.Configuration.ConfigurationElementCollectionType.BasicMap 
                || collectionElement.CollectionType == System.Configuration.ConfigurationElementCollectionType.BasicMapAlternate ) 
            { 
                collectionType.Arguments.Add( 
                       new CodeAttributeArgument( "AddItemName", 
                           new CodeFieldReferenceExpression( 
                               GlobalSelfReferenceExpression( element.FullName ), 
                               xmlPropertyName 
                           ) 
                       ) 
                   ); 
            } 
            else 
            { 

                collectionType.Arguments.Add( 
                       new CodeAttributeArgument( "AddItemName", 
                           new CodePrimitiveExpression( collectionElement.AddItemName ) 
                       ) 
                   ); 
                collectionType.Arguments.Add( 
                       new CodeAttributeArgument( "RemoveItemName", 
                           new CodePrimitiveExpression( collectionElement.RemoveItemName ) 
                       ) 
                   ); 
                collectionType.Arguments.Add( 
                       new CodeAttributeArgument( "ClearItemsName", 
                           new CodePrimitiveExpression( collectionElement.ClearItemsName ) 
                       ) 
                   ); 
            } 

            elementClass.CustomAttributes.Add( collectionType ); 



            // Add XmlName field
            CodeMemberField xmlNameField = new CodeMemberField( _string, xmlPropertyName );
            elementClass.Members.Add( xmlNameField );
            xmlNameField.StartDirectives.Add( Region( "Constants" ) );
            xmlNameField.Comments.Add( DocComment( "<summary>" ) );
            xmlNameField.Comments.Add( DocComment( string.Format( "The XML name of the individual <see cref=\"{0}\"/> instances in this collection.", collectionItemTypeReference ) ) );
            xmlNameField.Comments.Add( DocComment( "</summary>" ) );
            xmlNameField.CustomAttributes.Add( _generatedCodeAttribute );
            xmlNameField.Attributes = MemberAttributes.Const | MemberAttributes.Assembly;
            xmlNameField.InitExpression = new CodePrimitiveExpression( collectionElement.XmlItemName );
            xmlNameField.EndDirectives.Add( EndRegion() );

            // Add collection type property
            CodeMemberProperty collectionTypeProperty = new CodeMemberProperty();
            elementClass.Members.Add( collectionTypeProperty );
            collectionTypeProperty.StartDirectives.Add( Region( "Overrides" ) );
            collectionTypeProperty.Comments.Add( DocComment( "<summary>" ) );
            collectionTypeProperty.Comments.Add(DocComment(string.Format("Gets the type of the <see cref=\"{0}\"/>.", configurationElementCollectionTypeReferenceForDoc)));
            collectionTypeProperty.Comments.Add( DocComment( "</summary>" ) );
            collectionTypeProperty.Comments.Add( DocComment( string.Format( "<returns>The <see cref=\"{0}\"/> of this collection.</returns>", GetTypeReferenceString( typeof( System.Configuration.ConfigurationElementCollectionType ) ) ) ) );
            collectionTypeProperty.CustomAttributes.Add( _generatedCodeAttribute );
            collectionTypeProperty.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            collectionTypeProperty.Type = GlobalReference( typeof( System.Configuration.ConfigurationElementCollectionType ) );
            collectionTypeProperty.Name = "CollectionType";
            collectionTypeProperty.HasGet = true;
            collectionTypeProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        GlobalReferenceExpression( collectionElement.CollectionType.GetType() ),
                        Enum.GetName( collectionElement.CollectionType.GetType(), collectionElement.CollectionType )
                    )
                )
            );

            // Add ElementName property
            CodeMemberProperty elementNameProperty = new CodeMemberProperty();
            elementClass.Members.Add( elementNameProperty );
            elementNameProperty.Comments.Add( DocComment( "<summary>" ) );
            elementNameProperty.Comments.Add( DocComment( "Gets the name used to identify this collection of elements" ) );
            elementNameProperty.Comments.Add( DocComment( "</summary>" ) );
            elementNameProperty.CustomAttributes.Add( _generatedCodeAttribute );
            elementNameProperty.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            elementNameProperty.Type = _string;
            elementNameProperty.Name = "ElementName";
            elementNameProperty.HasGet = true;
            elementNameProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                        GlobalSelfReferenceExpression( element.FullName ),
                        xmlPropertyName
                    )
                )
            );

            // Add IsElementName method
            CodeMemberMethod isElementNameMethod = new CodeMemberMethod();
            elementClass.Members.Add( isElementNameMethod );
            isElementNameMethod.Comments.Add( DocComment( "<summary>" ) );
            isElementNameMethod.Comments.Add(DocComment(string.Format("Indicates whether the specified <see cref=\"{0}\"/> exists in the <see cref=\"{1}\"/>.", configurationElementTypeReferenceForDoc, configurationElementCollectionTypeReferenceForDoc)));
            isElementNameMethod.Comments.Add( DocComment( "</summary>" ) );
            isElementNameMethod.Comments.Add( DocComment( "<param name=\"elementName\">The name of the element to verify.</param>" ) );
            isElementNameMethod.Comments.Add( DocComment( "<returns>" ) );
            isElementNameMethod.Comments.Add( DocComment( "<see langword=\"true\"/> if the element exists in the collection; otherwise, <see langword=\"false\"/>." ) );
            isElementNameMethod.Comments.Add( DocComment( "</returns>" ) );
            isElementNameMethod.CustomAttributes.Add( _generatedCodeAttribute );
            isElementNameMethod.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            isElementNameMethod.ReturnType = GlobalReference( typeof( bool ) );
            isElementNameMethod.Name = "IsElementName";
            isElementNameMethod.Parameters.Add( new CodeParameterDeclarationExpression( _string, "elementName" ) );
            isElementNameMethod.Statements.Add(
                new CodeMethodReturnStatement(
                    new CodeBinaryOperatorExpression(
                        new CodeArgumentReferenceExpression( "elementName" ),
                        CodeBinaryOperatorType.ValueEquality,
                        new CodeFieldReferenceExpression(
                            GlobalSelfReferenceExpression( string.Format( "{0}.{1}", element.ActualNamespace, element.Name ) ),
                            xmlNameField.Name
                        )
                    )
                )
            );

            // Add GetElementKey method
            CodeMemberMethod getElementKeyMethod = new CodeMemberMethod();
            elementClass.Members.Add( getElementKeyMethod );
            getElementKeyMethod.Comments.Add( DocComment( "<summary>" ) );
            getElementKeyMethod.Comments.Add( DocComment( "Gets the element key for the specified configuration element." ) );
            getElementKeyMethod.Comments.Add( DocComment( "</summary>" ) );
            getElementKeyMethod.Comments.Add(DocComment(string.Format("<param name=\"element\">The <see cref=\"{0}\"/> to return the key for.</param>", configurationElementTypeReferenceForDoc)));
            getElementKeyMethod.Comments.Add( DocComment( "<returns>" ) );
            getElementKeyMethod.Comments.Add(DocComment(string.Format("An <see cref=\"{0}\"/> that acts as the key for the specified <see cref=\"{1}\"/>.", GetTypeReferenceString(typeof(object)), configurationElementTypeReferenceForDoc)));
            getElementKeyMethod.Comments.Add( DocComment( "</returns>" ) );
            getElementKeyMethod.CustomAttributes.Add( _generatedCodeAttribute );
            getElementKeyMethod.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            getElementKeyMethod.ReturnType = _object;
            getElementKeyMethod.Name = "GetElementKey";
            getElementKeyMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalReference( typeof( System.Configuration.ConfigurationElement ) ), "element" ) );
            if (keyProperty != null)
            {
                getElementKeyMethod.Statements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeCastExpression(
                                GlobalSelfReference(collectionElement.ItemType.FullName),
                                new CodeArgumentReferenceExpression("element")
                            ),
                            keyProperty.Name
                        )
                    )
                );
            }
            else
            {
                getElementKeyMethod.Statements.Add( 
                    new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(
                            new CodeVariableReferenceExpression("element"),
                            "GetHashCode"
                        )
                    )
                );
            }  

            // Add CreateNewElement method
            CodeMemberMethod createNewElementMethod = new CodeMemberMethod();
            elementClass.Members.Add( createNewElementMethod );
            createNewElementMethod.Comments.Add( DocComment( "<summary>" ) );
            createNewElementMethod.Comments.Add( DocComment( string.Format( "Creates a new <see cref=\"{0}\"/>.", collectionItemTypeReference ) ) );
            createNewElementMethod.Comments.Add( DocComment( "</summary>" ) );
            createNewElementMethod.Comments.Add( DocComment( "<returns>" ) );
            createNewElementMethod.Comments.Add( DocComment( string.Format( "A new <see cref=\"{0}\"/>.", collectionItemTypeReference ) ) );
            createNewElementMethod.Comments.Add( DocComment( "</returns>" ) );
            createNewElementMethod.CustomAttributes.Add( _generatedCodeAttribute );
            createNewElementMethod.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            createNewElementMethod.ReturnType = GlobalReference( typeof( System.Configuration.ConfigurationElement ) );
            createNewElementMethod.Name = "CreateNewElement";
            createNewElementMethod.Statements.Add(
                new CodeMethodReturnStatement( new CodeObjectCreateExpression( collectionItemType ) )
            );

            createNewElementMethod.EndDirectives.Add( EndRegion() );

            if( (collectionElement.CodeGenOptions & ConfigurationElementCollectionCodeGenOptions.Indexer) == ConfigurationElementCollectionCodeGenOptions.Indexer )
            {
                // Add indexer for location
                CodeMemberProperty locationIndexerProperty = new CodeMemberProperty();
                elementClass.Members.Add( locationIndexerProperty );
                locationIndexerProperty.StartDirectives.Add( Region( "Indexer" ) );
                locationIndexerProperty.Comments.Add( DocComment( "<summary>" ) );
                locationIndexerProperty.Comments.Add( DocComment( string.Format( "Gets the <see cref=\"{0}\"/> at the specified index.", collectionItemTypeReference ) ) );
                locationIndexerProperty.Comments.Add( DocComment( "</summary>" ) );
                locationIndexerProperty.Comments.Add( DocComment( string.Format( "<param name=\"index\">The index of the <see cref=\"{0}\"/> to retrieve.</param>", collectionItemTypeReference ) ) );
                locationIndexerProperty.CustomAttributes.Add( _generatedCodeAttribute );
                locationIndexerProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final | MemberAttributes.Overloaded;
                locationIndexerProperty.Type = GlobalSelfReference( collectionElement.ItemType.FullName );
                locationIndexerProperty.Name = "Item";
                locationIndexerProperty.Parameters.Add( new CodeParameterDeclarationExpression( _int, "index" ) );
                locationIndexerProperty.HasGet = true;
                locationIndexerProperty.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeCastExpression(
                            collectionItemType,
                            new CodeMethodInvokeExpression(
                                _base,
                                "BaseGet",
                                new CodeArgumentReferenceExpression( "index" )
                            )
                        )
                    )
                );

                // Add GetItemByKey method
                if (keyProperty != null)
                {
                    // Add GetItemByKey method for collections have an explicitly set key property
                    CodeMemberProperty keyIndexerProperty = new CodeMemberProperty();
                    elementClass.Members.Add(keyIndexerProperty);
                    keyIndexerProperty.Comments.Add(DocComment("<summary>"));
                    keyIndexerProperty.Comments.Add(DocComment(string.Format("Gets the <see cref=\"{0}\"/> with the specified key.", collectionItemTypeReference)));
                    keyIndexerProperty.Comments.Add(DocComment("</summary>"));
                    keyIndexerProperty.Comments.Add(DocComment(string.Format("<param name=\"{0}\">The key of the <see cref=\"{1}\"/> to retrieve.</param>", keyProperty.XmlName, collectionItemTypeReference)));
                    keyIndexerProperty.CustomAttributes.Add(_generatedCodeAttribute);
                    keyIndexerProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final | MemberAttributes.Overloaded;
                    keyIndexerProperty.Type = collectionItemType;
                    keyIndexerProperty.Name = "Item";
                    keyIndexerProperty.Parameters.Add(new CodeParameterDeclarationExpression(_object, keyProperty.XmlName));
                    keyIndexerProperty.GetStatements.Add(
                        new CodeMethodReturnStatement(
                            new CodeCastExpression(
                                collectionItemType,
                                new CodeMethodInvokeExpression(
                                    _base,
                                    "BaseGet",
                                    new CodeArgumentReferenceExpression(keyProperty.XmlName)
                                )
                            )
                        )
                    );
                    keyIndexerProperty.EndDirectives.Add(EndRegion());
                }
                else
                {
                    locationIndexerProperty.EndDirectives.Add(EndRegion());
                }        
            }

            if ( (collectionElement.CodeGenOptions & ConfigurationElementCollectionCodeGenOptions.AddMethod) == ConfigurationElementCollectionCodeGenOptions.AddMethod
               || (collectionElement.CodeGenOptions & ConfigurationElementCollectionCodeGenOptions.ICollection) == ConfigurationElementCollectionCodeGenOptions.ICollection )
            {
                // Add Add method
                CodeMemberMethod addMethod = new CodeMemberMethod();
                elementClass.Members.Add( addMethod );
                addMethod.StartDirectives.Add( Region( "Add" ) );
                addMethod.Comments.Add( DocComment( "<summary>" ) );
                addMethod.Comments.Add(DocComment(string.Format("Adds the specified <see cref=\"{0}\"/> to the <see cref=\"{1}\"/>.", collectionItemTypeReference, configurationElementCollectionTypeReferenceForDoc)));
                addMethod.Comments.Add( DocComment( "</summary>" ) );
                addMethod.Comments.Add( DocComment( string.Format( "<param name=\"{0}\">The <see cref=\"{1}\"/> to add.</param>", collectionElement.XmlItemName, collectionItemTypeReference ) ) );
                addMethod.CustomAttributes.Add( _generatedCodeAttribute );
                addMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                addMethod.ReturnType = _void;
                addMethod.Name = "Add";
                addMethod.Parameters.Add( new CodeParameterDeclarationExpression( GlobalSelfReference( collectionElement.ItemType.FullName ), collectionElement.XmlItemName ) );
                addMethod.Statements.Add(
                    new CodeMethodInvokeExpression(
                        _base,
                        "BaseAdd",
                        new CodeArgumentReferenceExpression( collectionElement.XmlItemName )
                    )
                );
                addMethod.EndDirectives.Add( EndRegion() );
            }

             if( (collectionElement.CodeGenOptions & ConfigurationElementCollectionCodeGenOptions.RemoveMethod) == ConfigurationElementCollectionCodeGenOptions.RemoveMethod
                || (collectionElement.CodeGenOptions & ConfigurationElementCollectionCodeGenOptions.ICollection) == ConfigurationElementCollectionCodeGenOptions.ICollection )
             {
                // Add Remove method
                CodeMemberMethod removeMethod = new CodeMemberMethod();
                elementClass.Members.Add( removeMethod );
                removeMethod.StartDirectives.Add( Region( "Remove" ) );
                removeMethod.Comments.Add( DocComment( "<summary>" ) );
                removeMethod.Comments.Add(DocComment(string.Format("Removes the specified <see cref=\"{0}\"/> from the <see cref=\"{1}\"/>.", collectionItemTypeReference, configurationElementCollectionTypeReferenceForDoc)));
                removeMethod.Comments.Add( DocComment( "</summary>" ) );
                removeMethod.Comments.Add( DocComment( string.Format( "<param name=\"{0}\">The <see cref=\"{1}\"/> to remove.</param>", collectionElement.XmlItemName, collectionItemTypeReference ) ) );
                removeMethod.CustomAttributes.Add( _generatedCodeAttribute );
                removeMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                removeMethod.ReturnType = _void;
                removeMethod.Name = "Remove";
                removeMethod.Parameters.Add( new CodeParameterDeclarationExpression( collectionItemType, collectionElement.XmlItemName ) );
                removeMethod.Statements.Add(
                    new CodeMethodInvokeExpression(
                        _base,
                        "BaseRemove",
                        new CodeMethodInvokeExpression(
                            _this,
                            "GetElementKey",
                            new CodeArgumentReferenceExpression( collectionElement.XmlItemName )
                        )
                    )
                );
                removeMethod.EndDirectives.Add( EndRegion() );
            }

            if( (collectionElement.CodeGenOptions & ConfigurationElementCollectionCodeGenOptions.GetItemMethods) == ConfigurationElementCollectionCodeGenOptions.GetItemMethods )
            {
                // Add GetItemAt method
                CodeMemberMethod getItemAtMethod = new CodeMemberMethod();
                elementClass.Members.Add( getItemAtMethod );
                getItemAtMethod.StartDirectives.Add( Region( "GetItem" ) );
                getItemAtMethod.Comments.Add( DocComment( "<summary>" ) );
                getItemAtMethod.Comments.Add( DocComment( string.Format( "Gets the <see cref=\"{0}\"/> at the specified index.", collectionItemTypeReference ) ) );
                getItemAtMethod.Comments.Add( DocComment( "</summary>" ) );
                getItemAtMethod.Comments.Add( DocComment( string.Format( "<param name=\"index\">The index of the <see cref=\"{0}\"/> to retrieve.</param>", collectionItemTypeReference ) ) );
                getItemAtMethod.CustomAttributes.Add( _generatedCodeAttribute );
                getItemAtMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                getItemAtMethod.ReturnType = collectionItemType;
                getItemAtMethod.Name = "GetItemAt";
                getItemAtMethod.Parameters.Add( new CodeParameterDeclarationExpression( _int, "index" ) );
                getItemAtMethod.Statements.Add(
                    new CodeMethodReturnStatement(
                        new CodeCastExpression(
                            collectionItemType,
                            new CodeMethodInvokeExpression(
                                _base,
                                "BaseGet",
                                new CodeArgumentReferenceExpression( "index" )
                            )
                        )
                    )
                );

                // Add GetItemByKey method
                if (keyProperty != null)
                {
                    // Add GetItemByKey method for collections have an explicitly set key property
                    CodeMemberMethod getItemByKeyMethod = new CodeMemberMethod();
                    elementClass.Members.Add(getItemByKeyMethod);
                    getItemByKeyMethod.Comments.Add(DocComment("<summary>"));
                    getItemByKeyMethod.Comments.Add(DocComment(string.Format("Gets the <see cref=\"{0}\"/> with the specified key.", collectionItemTypeReference)));
                    getItemByKeyMethod.Comments.Add(DocComment("</summary>"));
                    getItemByKeyMethod.Comments.Add(DocComment(string.Format("<param name=\"{0}\">The key of the <see cref=\"{1}\"/> to retrieve.</param>", keyProperty.XmlName, collectionItemTypeReference)));
                    getItemByKeyMethod.CustomAttributes.Add(_generatedCodeAttribute);
                    getItemByKeyMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                    getItemByKeyMethod.ReturnType = collectionItemType;
                    getItemByKeyMethod.Name = "GetItemByKey";
                    getItemByKeyMethod.Parameters.Add(new CodeParameterDeclarationExpression(GlobalReference(keyProperty.TypeName), keyProperty.XmlName));
                    getItemByKeyMethod.Statements.Add(
                        new CodeMethodReturnStatement(
                            new CodeCastExpression(
                                collectionItemType,
                                new CodeMethodInvokeExpression(
                                    _base,
                                    "BaseGet",
                                    new CodeCastExpression(
                                        _object,
                                        new CodeArgumentReferenceExpression(keyProperty.XmlName)
                                    )
                                )
                            )
                        )
                    );

                    getItemByKeyMethod.EndDirectives.Add(EndRegion());
                }
                else
                {
                    getItemAtMethod.EndDirectives.Add(EndRegion());
                } 
            }


            if ((collectionElement.CodeGenOptions & ConfigurationElementCollectionCodeGenOptions.ICollection) == ConfigurationElementCollectionCodeGenOptions.ICollection)
            {
                // Add Clear method
                CodeMemberMethod clearMethod = new CodeMemberMethod();
                elementClass.Members.Add(clearMethod);
                clearMethod.StartDirectives.Add(Region("ICollection"));
                clearMethod.Comments.Add(DocComment("<summary>"));
                clearMethod.Comments.Add(DocComment(string.Format("Removes all items from the <see cref=\"{0}\"/>.", configurationElementCollectionTypeReferenceForDoc)));
                clearMethod.Comments.Add(DocComment("</summary>"));
                clearMethod.CustomAttributes.Add(_generatedCodeAttribute);
                clearMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                clearMethod.ReturnType = _void;
                clearMethod.Name = "Clear";
                clearMethod.Statements.Add(
                    new CodeMethodInvokeExpression(_base, "BaseClear")
                );

                //Add Contains method
                CodeMemberMethod containsMethod = new CodeMemberMethod();
                elementClass.Members.Add(containsMethod);
                containsMethod.Comments.Add(DocComment("<summary>"));
                containsMethod.Comments.Add(DocComment(string.Format("Determines whether the <see cref=\"{0}\"/> contains a specific value.", configurationElementCollectionTypeReferenceForDoc)));
                containsMethod.Comments.Add(DocComment("</summary>"));
                containsMethod.Comments.Add(DocComment(string.Format("<param name=\"{0}\">The object to locate in the <see cref=\"{1}\"/>.</param>", collectionElement.XmlItemName, collectionItemTypeReference)));
                containsMethod.CustomAttributes.Add(_generatedCodeAttribute);
                containsMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                containsMethod.ReturnType = _bool;
                containsMethod.Name = "Contains";
                containsMethod.Parameters.Add(new CodeParameterDeclarationExpression(collectionItemType, collectionElement.XmlItemName));
                containsMethod.Statements.Add(
                    new CodeMethodReturnStatement(
                        new CodeBinaryOperatorExpression(
                            new CodeMethodInvokeExpression(_base, "BaseIndexOf", new CodeArgumentReferenceExpression(collectionElement.XmlItemName)),
                            CodeBinaryOperatorType.GreaterThanOrEqual,
                            new CodePrimitiveExpression(0)
                        )
                    )
                );

                //Add CopyTo method
                CodeMemberMethod copytoMethod = new CodeMemberMethod();
                elementClass.Members.Add(copytoMethod);
                copytoMethod.Comments.Add(DocComment("<summary>"));
                copytoMethod.Comments.Add(DocComment(string.Format("Copies the elements of the <see cref=\"{0}\"/> to an <see cref=\"System.Array\"/>, starting at a particular <see cref=\"System.Array\"/> index.", configurationElementCollectionTypeReferenceForDoc)));
                copytoMethod.Comments.Add(DocComment("</summary>"));
                copytoMethod.Comments.Add(DocComment(string.Format("<param name=\"array\">The one-dimensional <see cref=\"System.Array\"/> that is the destination of the elements copied from <see cref=\"{0}\"/>. The <see cref=\"System.Array\"/> must have zero-based indexing.</param>", configurationElementCollectionTypeReferenceForDoc)));
                copytoMethod.Comments.Add(DocComment("<param name=\"arrayIndex\">The zero-based index in <paramref name=\"array\"/> at which copying begins.</param>"));
                copytoMethod.CustomAttributes.Add(_generatedCodeAttribute);
                copytoMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                copytoMethod.ReturnType = _void;
                copytoMethod.Name = "CopyTo";
                copytoMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(collectionItemTypeReference, 1), "array"));
                copytoMethod.Parameters.Add(new CodeParameterDeclarationExpression(_int, "arrayIndex"));
                copytoMethod.Statements.Add(
                    new CodeMethodInvokeExpression(_base, "CopyTo", new CodeArgumentReferenceExpression("array"), new CodeArgumentReferenceExpression("arrayIndex"))
                );

                //Add ICollection.IsReadOnly property
                CodeMemberProperty isReadOnlyProperty = new CodeMemberProperty();
                elementClass.Members.Add(isReadOnlyProperty);
                isReadOnlyProperty.Comments.Add(DocComment("<summary>"));
                isReadOnlyProperty.Comments.Add(DocComment(string.Format("Gets a value indicating whether the <see cref=\"{0}\"/> is read-only.", configurationElementCollectionTypeReferenceForDoc)));
                isReadOnlyProperty.Comments.Add(DocComment("</summary>"));
                isReadOnlyProperty.CustomAttributes.Add(_generatedCodeAttribute);
                isReadOnlyProperty.Attributes = MemberAttributes.Private;
                isReadOnlyProperty.PrivateImplementationType = icollectionTypeReference;
                isReadOnlyProperty.Type = _bool;
                isReadOnlyProperty.HasGet = true;
                isReadOnlyProperty.HasSet = false;
                isReadOnlyProperty.Name = "IsReadOnly";
                isReadOnlyProperty.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeMethodInvokeExpression(_this, "IsReadOnly")
                    )
                );

                //Add ICollection.Remove method
                CodeMemberMethod icRemoveMethod = new CodeMemberMethod();
                elementClass.Members.Add(icRemoveMethod);
                icRemoveMethod.Comments.Add(DocComment("<summary>"));
                icRemoveMethod.Comments.Add(DocComment(string.Format("Removes the first occurrence of a specific object from the <see cref=\"{0}\"/>.", configurationElementCollectionTypeReferenceForDoc)));
                icRemoveMethod.Comments.Add(DocComment("</summary>"));
                icRemoveMethod.Comments.Add(DocComment(string.Format("<param name=\"item\">The <see cref=\"{0}\"/> to remove.</param>", collectionItemTypeReference)));
                icRemoveMethod.CustomAttributes.Add(_generatedCodeAttribute);
                icRemoveMethod.Attributes = MemberAttributes.Private;
                icRemoveMethod.PrivateImplementationType = icollectionTypeReference;
                icRemoveMethod.ReturnType = _bool;
                icRemoveMethod.Name = "Remove";
                icRemoveMethod.Parameters.Add(new CodeParameterDeclarationExpression(collectionItemType, "item"));
                icRemoveMethod.Statements.Add(
                    new CodeVariableDeclarationStatement(_int, "idx",
                        new CodeMethodInvokeExpression(_base, "BaseIndexOf", new CodeArgumentReferenceExpression("item"))
                    )
                );
                icRemoveMethod.Statements.Add(
                    new CodeConditionStatement(
                        new CodeBinaryOperatorExpression(
                            new CodeVariableReferenceExpression("idx"),
                            CodeBinaryOperatorType.IdentityEquality,
                            new CodePrimitiveExpression(-1)
                        ),
                        new CodeMethodReturnStatement(new CodePrimitiveExpression(false))
                    )
                );
                icRemoveMethod.Statements.Add(
                    new CodeMethodInvokeExpression(_base, "BaseRemoveAt", new CodeVariableReferenceExpression("idx"))
                );
                icRemoveMethod.Statements.Add(
                    new CodeMethodReturnStatement(new CodePrimitiveExpression(true))
                );

                //Add IEnumerator<> method
                CodeTypeReference ienumTypeReference = new CodeTypeReference(typeof(System.Collections.Generic.IEnumerator<>), CodeTypeReferenceOptions.GlobalReference);
                ienumTypeReference.Options = CodeTypeReferenceOptions.GlobalReference | CodeTypeReferenceOptions.GenericTypeParameter;
                ienumTypeReference.TypeArguments.Add(collectionItemType);
                CodeTypeReference listTypeReference = new CodeTypeReference(typeof(System.Collections.Generic.List<>), CodeTypeReferenceOptions.GlobalReference);
                listTypeReference.Options = CodeTypeReferenceOptions.GlobalReference | CodeTypeReferenceOptions.GenericTypeParameter;
                listTypeReference.TypeArguments.Add(collectionItemType);

                CodeMemberMethod getEnumeratorMethod = new CodeMemberMethod();
                elementClass.Members.Add(getEnumeratorMethod);
                getEnumeratorMethod.Comments.Add(DocComment("<summary>"));
                getEnumeratorMethod.Comments.Add(DocComment("Returns an enumerator that iterates through the collection."));
                getEnumeratorMethod.Comments.Add(DocComment("</summary>"));
                getEnumeratorMethod.CustomAttributes.Add(_generatedCodeAttribute);
                getEnumeratorMethod.Attributes = MemberAttributes.New | MemberAttributes.Public | MemberAttributes.Final;
                getEnumeratorMethod.ReturnType = ienumTypeReference;
                getEnumeratorMethod.Name = "GetEnumerator";
                getEnumeratorMethod.Statements.Add(
                    new CodeVariableDeclarationStatement(listTypeReference, "list",
                        new CodeObjectCreateExpression(listTypeReference, new CodePropertyReferenceExpression(_base, "Count"))
                    )
                );
                CodeVariableReferenceExpression varList = new CodeVariableReferenceExpression("list");
                getEnumeratorMethod.Statements.Add(
                    new CodeVariableDeclarationStatement(GlobalReference(typeof(System.Collections.IEnumerator)), "iter",
                        new CodeMethodInvokeExpression(_base, "GetEnumerator")
                    )
                );
                CodeVariableReferenceExpression varIter = new CodeVariableReferenceExpression("iter");
                CodeExpressionStatement emptyExpr = new CodeExpressionStatement(new CodeSnippetExpression());
                getEnumeratorMethod.Statements.Add(
                    new CodeIterationStatement(
                        emptyExpr,
                        new CodeMethodInvokeExpression(varIter, "MoveNext"),
                        emptyExpr,
                        new CodeExpressionStatement(
                            new CodeMethodInvokeExpression(
                                varList, "Add",
                                new CodeCastExpression(collectionItemType, new CodePropertyReferenceExpression(varIter, "Current"))
                            )
                        )
                    )
                );
                getEnumeratorMethod.Statements.Add(
                    new CodeMethodReturnStatement(new CodeMethodInvokeExpression(varList, "GetEnumerator")
                    )
                );
                getEnumeratorMethod.EndDirectives.Add(EndRegion());
            }


        }
    }
}
