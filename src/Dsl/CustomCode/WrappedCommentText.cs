using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace ConfigurationSectionDesigner
{
    partial class CommentBoxShape
    {
        public override bool HasShadow
        {
            get
            {
                return false;
            }
        }

        // The class is double-derived so that we can override this method.
        // Called once for each class.
        protected override void InitializeDecorators(IList<ShapeField> shapeFields, IList<Decorator> decorators)
        {
            // TODO: Allow user to change text align.

            // Set up the decorators defined in the DSL Definition:
            base.InitializeDecorators(shapeFields, decorators);

            //this.HasShadow = false;
            

            // Look up the text decorator, which is called "Comment".
            TextField commentField = (TextField)ShapeElement.FindShapeField(shapeFields, "Comment");

            // This sets the wrapping behavior, but we need a couple of other things too:
            commentField.DefaultMultipleLine = true;

            // Autosize is incompatible with multiple line:
            commentField.DefaultAutoSize = false;

            //commentField.AllowInPlaceEditorAutoSize(this);

            // Need to anchor the field sides to the parent box to get sensible size:
            commentField.AnchoringBehavior.Clear();
            commentField.AnchoringBehavior.SetLeftAnchor(AnchoringBehavior.Edge.Left, 0.01);
            commentField.AnchoringBehavior.SetRightAnchor(AnchoringBehavior.Edge.Right, 0.01);
            commentField.AnchoringBehavior.SetTopAnchor(AnchoringBehavior.Edge.Top, 0.01);
            commentField.AnchoringBehavior.SetBottomAnchor(AnchoringBehavior.Edge.Bottom, 0.01);

            // Note that this method is called just once per class.
            // commentField is a field definition, attached to the class, not instances.
        }

    }
}
