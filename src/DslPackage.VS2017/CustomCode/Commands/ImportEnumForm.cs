using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConfigurationSectionDesigner.Properties;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Modeling;
using System.Reflection;

namespace ConfigurationSectionDesigner
{
    public partial class ImportEnumForm : Form
    {
        public ImportEnumForm( List<Type> enumTypes )
        {
            InitializeComponent();

            foreach( Type enumType in enumTypes )
            {
                TreeNode enumNode = EnumTree.Nodes.Add( enumType.FullName );
                enumNode.Tag = enumType;
                enumNode.ImageKey = "Enum";
                enumNode.SelectedImageKey = "Enum";

                FieldInfo[] fields = enumType.GetFields( BindingFlags.Public | BindingFlags.Static );
                foreach( FieldInfo field in fields )
                {
                    TreeNode fieldNode = enumNode.Nodes.Add( field.Name );
                    fieldNode.ImageKey = "Value";
                    fieldNode.SelectedImageKey = "Value";
                }
            }
        }

        private void EnumTree_AfterSelect( object sender, TreeViewEventArgs e )
        {
            OKButton.Enabled = EnumTree.SelectedNode != null && EnumTree.SelectedNode.Parent == null;
        }

        public Type SelectedEnum
        {
            get
            {
                return (Type)EnumTree.SelectedNode.Tag;
            }
        }
    }
}
