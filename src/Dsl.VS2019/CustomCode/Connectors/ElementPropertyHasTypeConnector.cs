using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.PlatformUI;

namespace ConfigurationSectionDesigner
{
    partial class ElementPropertyHasTypeConnector
    {
        /// <summary>
        ///     Are themable colors already applied?
        /// </summary>
        internal static bool IsColorThemeSet { get; set; }

        public override void OnPaintShape(DiagramPaintEventArgs e)
        {
            // Check if we have to override colors.
            if (!IsColorThemeSet)
            {
                SetColorTheme();
            }

            base.OnPaintShape(e);

            if (e.View != null)
            {
                // If the shape is in the EmphasizedShapes list, draw the emphasis shape around the shape.
                var diagram = Diagram as ConfigurationSectionDesignerDiagram;
                if (diagram.EmphasizedShapes.Contains(new DiagramItem(this)))
                {
                    ShapeGeometry.DoPaintEmphasis(e, this);
                }
            }
        }

        /// <summary>
        ///     Override themable colors.
        /// </summary>
        private void SetColorTheme()
        {
            // Set the emphasis outline color for selected shapes.
            ClassStyleSet.OverridePenColor(DiagramPens.EmphasisOutline, ConfigurationElementShape.EmphasisShapeOutlineColor);
            // SourceEndDisplayText and TargetEndDisplayText use this brush, and we need them to be distinguisable in the background. 
            ClassStyleSet.OverrideBrushColor(
                DiagramBrushes.ShapeText, VSColorTheme.GetThemedColor(EnvironmentColors.ToolWindowTextColorKey));
            // Shouldn't need to do this unless user changes theme.
            IsColorThemeSet = true;
        }
    }
}
