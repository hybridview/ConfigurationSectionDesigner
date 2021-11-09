using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;

namespace ConfigurationSectionDesigner
{
    // Implements the IVsEditorFactoryNotify on our designer editor factory in order to
    // be notified when a Configuration Section Designer is added to a project, so that
    // we can set its Custom Tool to "CsdFileGenerator" when that happens.

    internal partial class ConfigurationSectionDesignerEditorFactory : IVsEditorFactoryNotify
    {
        #region IVsEditorFactoryNotify Members

        public int NotifyDependentItemSaved( IVsHierarchy pHier, uint itemidParent, string pszMkDocumentParent, uint itemidDpendent, string pszMkDocumentDependent )
        {
            // Ignored
            return 0;
        }

        public int NotifyItemAdded( uint grfEFN, IVsHierarchy pHier, uint itemid, string pszMkDocument )
        {
            try
            {
                int hr = SafeNotifyItemAdded( grfEFN, pHier, itemid, pszMkDocument );
                return 0;
            }
            catch( Exception )
            {
                return 0;
            }
        }

        private int SafeNotifyItemAdded( uint grfEFN, IVsHierarchy pHier, uint itemid, string pszMkDocument )
        {
            object itemObject;
            int hr = pHier.GetProperty( itemid, (int)__VSHPROPID.VSHPROPID_ExtObject, out itemObject );

            ProjectItem item = itemObject as ProjectItem;
            if( item == null )
            {
                return -1;
            }

            // Set the name of the custom tool on .csd files
            item.Properties.Item( "CustomTool" ).Value = "CsdFileGenerator";

            // Ensure a reference to System.Configuration is added!
            //VsHelper.AddProjectReference(VsHelper.ToDteProject(pHier), "", "System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A");
            VsHelper.AddProjectReference(VsHelper.ToDteProject(pHier), "", "System.Configuration");

            return 0;
        }

        public int NotifyItemRenamed( IVsHierarchy pHier, uint itemid, string pszMkDocumentOld, string pszMkDocumentNew )
        {
            // Ignored
            return 0;
        }

        #endregion
    }
}
