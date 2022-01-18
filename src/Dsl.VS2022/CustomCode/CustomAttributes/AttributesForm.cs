using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigurationSectionDesigner.Properties;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Modeling;

namespace ConfigurationSectionDesigner
{
    public partial class AttributesForm : Form
    {
        private TreeNode addAttributeNode = new TreeNode( "<add attribute>" )
        {
            ForeColor = Color.DarkGray,
            ImageIndex = 0
        };

        private LinkedElementCollection<Attribute> _attributes;

        private Store _store;

        private static TreeNode CreateAddParameterNode( TreeNode attributeNode )
        {
            TreeNode addParameter = attributeNode.Nodes.Add( "<add parameter>" );
            addParameter.ImageKey = addParameter.SelectedImageKey = "DoubleBrace";
            addParameter.ForeColor = Color.DarkGray;
            addParameter.Tag = "addparameter";
            return addParameter;
        }

        private static void SetParameterIcons( TreeNodeCollection nodes )
        {
            if( nodes.Count == 1 )
                nodes[0].ImageKey = nodes[0].SelectedImageKey = "DoubleBrace";
            else
                foreach( TreeNode node in nodes )
                    if( node.PrevNode == null )
                        node.ImageKey = node.SelectedImageKey = "LeftBrace";
                    else if( node.PrevNode != null && node.NextNode != null )
                        node.ImageKey = node.SelectedImageKey = "Comma";
                    else
                        node.ImageKey = node.SelectedImageKey = "RightBrace";
        }

        private static void NamedPropertyEvaluation( string propertyText, Action<string> ifValue, Action<string, string> ifNameValue )
        {
            Match match = Regex.Match( propertyText, "^(?<name>[^=\"]+?)\\s*=\\s*(?<value>.+)$" );
            if( match.Success && match.Groups["name"].Value.IsValidIdentifier() )
                ifNameValue( match.Groups["name"].Value, match.Groups["value"].Value );
            else
                ifValue( propertyText );
        }

        public AttributesForm( Store store, LinkedElementCollection<Attribute> attributes )
        {
            _attributes = attributes;
            _store = store;

            InitializeComponent();

            foreach( Attribute attribute in attributes )
            {
                TreeNode attributeNode = AttributeTree.Nodes.Add( attribute.Name );
                attributeNode.Tag = attribute;
                foreach( AttributeParameter parameter in attribute.Parameters )
                {
                    TreeNode parameterNode;
                    if( string.IsNullOrEmpty( parameter.Name ) )
                        parameterNode = attributeNode.Nodes.Add( parameter.Value );
                    else
                        parameterNode = attributeNode.Nodes.Add( string.Format( "{0} = {1}", parameter.Name, parameter.Value ) );
                    parameterNode.Tag = parameter;
                }
                TreeNode addParameter = CreateAddParameterNode( attributeNode );
                SetParameterIcons( attributeNode.Nodes );
            }
            AttributeTree.Nodes.Add( addAttributeNode );

            IsDirty = false;
        }

        public bool IsDirty { get; private set; }

        private void AttributeTree_AfterLabelEdit( object sender, NodeLabelEditEventArgs e )
        {
            if( e.Node == addAttributeNode )
            {
                string newAttributeLabel = e.Label;
                e.CancelEdit = true;

                Attribute attribute = new Attribute( _store ) { Name = newAttributeLabel };
                _attributes.Add( attribute );
                IsDirty = true;

                TreeNode newAttribute = AttributeTree.Nodes.Insert( AttributeTree.Nodes.Count - 1, newAttributeLabel );
                newAttribute.Tag = attribute;
                TreeNode addParameter = CreateAddParameterNode( newAttribute );
                newAttribute.Expand();

                AttributeTree.BeginInvoke( (Action)(() =>
                {
                    AttributeTree.SelectedNode = addParameter;
                    AttributeTree.StartLabelEdit();
                }) );
            }
            else if( e.Node.IsAddParameterNode() )
            {
                TreeNode addParameter = e.Node;
                TreeNode attributeNode = e.Node.Parent;
                Attribute attribute = attributeNode.Tag as Attribute;
                string newParameterLabel = e.Label;
                e.CancelEdit = true;

                AttributeParameter parameter = null;
                NamedPropertyEvaluation( newParameterLabel,
                    ( value ) =>
                    {
                        newParameterLabel = value;
                        parameter = new AttributeParameter( _store ) { Value = value };
                    },
                    ( name, value ) =>
                    {
                        newParameterLabel = string.Format( "{0} = {1}", name, value );
                        parameter = new AttributeParameter( _store ) { Name = name, Value = value };
                    }
                );
                attribute.Parameters.Add( parameter );
                IsDirty = true;

                TreeNode newParameter = addParameter.Parent.Nodes.Insert( addParameter.Parent.Nodes.Count - 1, newParameterLabel );
                newParameter.Tag = parameter;
                SetParameterIcons( attributeNode.Nodes );

                AttributeTree.BeginInvoke( (Action)(() =>
                {
                    AttributeTree.SelectedNode = addParameter;
                    AttributeTree.StartLabelEdit();
                }) );
            }
            else
            {
                if( e.Node.Tag is Attribute )
                {
                    ((Attribute)e.Node.Tag).Name = e.Label;
                    e.Node.Text = e.Label;
                }
                else if( e.Node.Tag is AttributeParameter )
                {
                    AttributeParameter property = (AttributeParameter)e.Node.Tag;
                    NamedPropertyEvaluation( e.Label,
                        ( value ) =>
                        {
                            e.Node.Text = value;
                            property.Name = null;
                            property.Value = value;
                        },
                        ( name, value ) =>
                        {
                            e.Node.Text = string.Format( "{0} = {1}", name, value );
                            property.Name = name;
                            property.Value = value;
                        }
                    );
                }
                IsDirty = true;
            }
        }

        private void AttributeTree_BeforeLabelEdit( object sender, NodeLabelEditEventArgs e )
        {
            if( e.Node == addAttributeNode || e.Node.IsAddParameterNode() )
            {
                e.Node.Text = "";
            }
        }

        private void AttributeTree_ValidateLabelEdit( object sender, ValidateLabelEditEventArgs e )
        {
            if( string.IsNullOrEmpty( e.Label.Trim() ) )
                e.Cancel = true;
        }

        private void AttributeTree_KeyUp( object sender, KeyEventArgs e )
        {
            if( e.KeyCode == Keys.F2 )
            {
                if( AttributeTree.SelectedNode != null )
                    AttributeTree.StartLabelEdit();
            }
            else if( e.KeyCode == Keys.Delete )
            {
                TreeNode selectedNode = AttributeTree.SelectedNode;
                if( selectedNode == null || selectedNode == addAttributeNode || selectedNode.IsAddParameterNode() )
                    return;

                if( selectedNode.Parent == null )
                {
                    _attributes.Remove( (Attribute)selectedNode.Tag );
                    IsDirty = true;

                    AttributeTree.Nodes.Remove( selectedNode );
                }
                else
                {
                    TreeNode attributeNode = selectedNode.Parent;
                    Attribute attribute = (Attribute)attributeNode.Tag;
                    attribute.Parameters.Remove( (AttributeParameter)selectedNode.Tag );
                    IsDirty = true;

                    attributeNode.Nodes.Remove( selectedNode );
                    SetParameterIcons( attributeNode.Nodes );
                }
            }
        }

        private void ExpandAllButton_Click( object sender, EventArgs e )
        {
            AttributeTree.ExpandAll();
        }
    }

    internal static class TypeExtensions
    {
        public static bool IsAddParameterNode( this TreeNode @this )
        {
            return @this.Tag != null && @this.Tag.Equals( "addparameter" );
        }

        public static bool IsValidIdentifier( this string @this )
        {
            const string start = @"(\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl})";
            const string extend = @"(\p{Mn}|\p{Mc}|\p{Nd}|\p{Pc}|\p{Cf})";
            var ident = new Regex( string.Format( "{0}({0}|{1})*", start, extend ) );
            @this = @this.Normalize();
            return ident.Match( @this ).Success;
        }
    }
}
