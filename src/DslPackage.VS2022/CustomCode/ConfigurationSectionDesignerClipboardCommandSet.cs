using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace ConfigurationSectionDesigner
{
    internal partial class ConfigurationSectionDesignerClipboardCommandSet
    {
        protected override void ProcessOnStatusPasteCommand(System.ComponentModel.Design.MenuCommand cmd)
        {
            base.ProcessOnStatusPasteCommand(cmd);

            if (!cmd.Enabled && this.SingleSelection is ElementListCompartment)
            {
                var compartment = (ElementListCompartment)this.SingleSelection;
                ShapeElement pasteTarget = compartment.ParentShape;

                System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();

                if (pasteTarget != null && this.ElementOperations.CanMerge(pasteTarget, data))
                {
                    cmd.Enabled = true;
                }
            }
        }

        protected override void ProcessOnMenuPasteCommand()
        {
            if (this.SingleSelection is ElementListCompartment)
            {
                var compartment = (ElementListCompartment)this.SingleSelection;
                ShapeElement pasteTarget = compartment.ParentShape;

                System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();

                if (pasteTarget != null && this.ElementOperations.CanMerge(pasteTarget, data))
                {
                    using (Transaction t = pasteTarget.Store.TransactionManager.BeginTransaction("paste"))
                    {
                        this.ElementOperations.Merge(pasteTarget, data);
                        t.Commit();
                    }
                }
            }
            else
            {
                base.ProcessOnMenuPasteCommand();
            }
        }


    }
}