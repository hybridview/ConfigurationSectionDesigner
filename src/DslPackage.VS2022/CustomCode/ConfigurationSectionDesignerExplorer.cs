using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;

namespace ConfigurationSectionDesigner
{
    internal partial class ConfigurationSectionDesignerExplorer
    {
        #region Code to suppress "Delete" for Validators

        /// <summary>
        /// Override to stop the "Delete" command appearing for
        /// Validators.
        /// </summary>
        protected override void ProcessOnStatusDeleteCommand( MenuCommand command )
        {
            // Check the selected items to see if they contain
            // Validators.
            if( this.SelectedElement.GetType()== typeof( PropertyValidators ) ) 
            {
                // Disable the menu command
                command.Enabled = false;
                command.Visible = false;
            }
            else
            {
                // Otherwise, delegate to the base method.
                base.ProcessOnStatusDeleteCommand( command );
            }
        }

        #endregion

    }
}