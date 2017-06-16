using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Diagrams.GraphObject;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ConfigurationSectionDesigner
{
    partial class ConfigurationSectionDesignerDiagram
    {
        private readonly EmphasizedShapes _emphasizedShapes = new EmphasizedShapes();

        protected override void InitializeResources(StyleSet classStyleSet)
        {
            base.InitializeResources(classStyleSet);
            // Set themable colors for all instances.
            SetColorTheme(classStyleSet);
            // Subscribe to theme changed event to update themable colors for all instances if necessary.
            VSColorTheme.ThemeChanged += e => SetColorTheme(classStyleSet);
        }

        private static void SetColorTheme(StyleSet styleSet)
        {
            // TODO: Without this some test in ViewModel.Tests.csproj are failing because apparently they are non runing in VS. We should fix the tests and shouldn't need this anymore. 
            if (!IsThemeServiceAvailable())
            {
                return;
            }
            // Override brush settings for this diagram background.
            styleSet.OverrideBrushColor(
                DiagramBrushes.DiagramBackground, VSColorTheme.GetThemedColor(EnvironmentColors.DesignerBackgroundColorKey));
            // Override lasso color for thumbnail view.
            styleSet.OverridePenColor(DiagramPens.ZoomLasso, VSColorTheme.GetThemedColor(EnvironmentColors.ClassDesignerLassoColorKey));
            // Notify EntityTypeShape that changes will require updating themable colors on next painting.
            ConfigurationElementShape.IsColorThemeSet = false;
            // Notify all Connectors that changes will require updating themable colors on next painting.
            InheritanceConnector.IsColorThemeSet = false;
            ConfigurationElementCollectionHasItemTypeConnector.IsColorThemeSet = false;
            ConfigurationSectionGroupHasConfigurationSectionGroupPropertiesConnector.IsColorThemeSet = false;
            ConfigurationSectionPropertyHasConfigurationSectionConnector.IsColorThemeSet = false;
            ElementPropertyHasTypeConnector.IsColorThemeSet = false;
        }

        /// <summary>
        ///     Check whether theme services are available, i.e. we are running inside VS
        /// </summary>
        private static bool IsThemeServiceAvailable()
        {
            return null != Package.GetGlobalService(typeof(SVsUIShell)) as IVsUIShell5;
        }

        /// <summary>
        ///     Contains the list of related shapes which emphasis shapes will be drawn around them.
        /// </summary>
        public EmphasizedShapes EmphasizedShapes
        {
            get { return _emphasizedShapes; }
        }

        /// <summary>
        ///     Performs an AutoLayoutDiagram command on all child shapes
        /// </summary>
        public void AutoLayoutDiagram()
        {
            AutoLayoutDiagram(NestedChildShapes);
        }

        /// <summary>
        ///     Performs an AutoLayoutDiagram command on the list of shapes passed in
        /// </summary>
        public void AutoLayoutDiagram(IList shapes)
        {
            // Put up an hourglass because this may take a while
            using (new VsUtils.HourglassHelper())
            {
                // Inheritance lines need to be placed using a different styling
                // so that the lines join at the same point. Sort out which shapes we have
                var inheritanceLinks = new List<ShapeElement>();
                var inheritanceShapes = new List<ShapeElement>();
                var otherShapes = new List<ShapeElement>();

                foreach (ShapeElement shape in shapes)
                {
                    // Fix a bug when the user tries to layout a diagram that contains inheritance classes multiple times, the shapes are moved to the bottom of the screen.
                    // The fix is to include both base class and derived class in the inheritance shapes list; before the fix we only includes the derived class in the list.
                    var isInheritanceClass = false;

                    var entityTypeShape = shape as ConfigurationElementShape;
                    // get a list of all lines leading to/from the shape
                    if (entityTypeShape != null)
                    {
                        var allLinks = new ArrayList(entityTypeShape.FromRoleLinkShapes);
                        allLinks.AddRange(entityTypeShape.ToRoleLinkShapes);

                        foreach (LinkShape link in allLinks)
                        {
                            if (link is InheritanceConnector)
                            {
                                isInheritanceClass = true;
                                break;
                            }
                        }
                    }

                    if (isInheritanceClass)
                    {
                        inheritanceShapes.Add(shape);
                    }
                    else if (shape is InheritanceConnector)
                    {
                        inheritanceLinks.Add(shape);
                    }
                    else
                    {
                        otherShapes.Add(shape);
                    }
                }

                // These flags are based on AutoLayout code from the ClassDesigner
                // Flags we set so the graph object ignores a shape (treats that shape as invisible) when doing layout
                const VGNodeFixedStates ignoreShapeFlags =
                    VGNodeFixedStates.FixedPlace | // don't consider for placement
                    VGNodeFixedStates.PermeablePlace; // place on top if desired (ignore for placement purposes)

                // Flags we set so the graph object recognizes but doesn't move a shape when doing layout
                const VGNodeFixedStates noMoveShapeFlags = VGNodeFixedStates.FixedPlace;

                // Perform the auto layout
                using (var t = Store.TransactionManager.BeginTransaction("LayoutDiagram"))
                {
                    //t.Context.Add(EfiTransactionOriginator.TransactionOriginatorDiagramId, DiagramId);

                    using (new SaveLayoutFlags(inheritanceShapes, ignoreShapeFlags))
                    {
                        // Since the inheritance shapes will be moved later,
                        // tell this layout to ignore them and place other objects on
                        // top if necessary
                        AutoLayoutShapeElements(
                            shapes,
                            VGRoutingStyle.VGRouteNetwork,
                            PlacementValueStyle.VGPlaceWE,
                            false);
                    }

                    using (new SaveLayoutFlags(otherShapes, noMoveShapeFlags))
                    {
                        // DD 40487: Move any classes that have inheritance, while keeping
                        // the others in place. Use org chart and PlaceSN so that parent
                        // classes appear above child ones
                        AutoLayoutShapeElements(
                            shapes,
                            VGRoutingStyle.VGRouteOrgChartNS,
                            PlacementValueStyle.VGPlaceSN,
                            false);
                    }

                    using (new SaveLayoutFlags(shapes, noMoveShapeFlags))
                    {
                        // DD 40516: Make inheritance lines connect at single point and
                        // don't move anything else
                        AutoLayoutShapeElements(
                            inheritanceLinks,
                            VGRoutingStyle.VGRouteRightAngle,
                            PlacementValueStyle.VGPlaceUndirected,
                            false);
                    }

                    Reroute();

                    t.Commit();
                }
            } // restore cursor
        }

        /// <summary>
        ///     This class saves the VGNodeFixedStates of the objects
        ///     passed, and restores them on the disposed. It can also
        ///     be used to change all the flags to the same value.
        /// </summary>
        private sealed class SaveLayoutFlags : IDisposable
        {
            private readonly IList _elements;
            private VGNodeFixedStates[] _savedFlags;

            public SaveLayoutFlags(IList elements)
            {
                _elements = elements;
                Save();
            }

            public SaveLayoutFlags(IList elements, VGNodeFixedStates flags)
                : this(elements)
            {
                SetFlags(flags);
            }

            public void Dispose()
            {
                Restore();
            }

            // Sets all the flags on the elements stored to this value
            public void SetFlags(VGNodeFixedStates flags)
            {
                for (var i = 0; i < _elements.Count; i++)
                {
                    var node = _elements[i] as NodeShape;
                    if (node != null)
                    {
                        node.LayoutObjectFixedFlags = flags;
                    }
                }
            }

            // Keeps a copy of the old values of all these flags
            private void Save()
            {
                _savedFlags = new VGNodeFixedStates[_elements.Count];
                for (var i = 0; i < _elements.Count; i++)
                {
                    var node = _elements[i] as NodeShape;
                    if (node != null)
                    {
                        _savedFlags[i] = node.LayoutObjectFixedFlags;
                    }
                }
            }

            // Restores the old flag values
            private void Restore()
            {
                for (var i = 0; i < _elements.Count; i++)
                {
                    var node = _elements[i] as NodeShape;
                    if (node != null)
                    {
                        node.LayoutObjectFixedFlags = _savedFlags[i];
                    }
                }
            }
        }

        internal void AddModelType(Func<Store, BaseConfigurationType> elementFactory, PointD creationPoint)
        {
            using (var t = Store.TransactionManager.BeginTransaction("Add new"))
            {
                var element = elementFactory(Store);

                var model = ModelElement as ConfigurationSectionModel;
                if (model != null)
                {
                    model.ConfigurationElements.Add(element);

                    var shapeType = Type.GetType(element.GetType().FullName + "Shape", false);
                    if (shapeType != null)
                    {
                        var shape = Activator.CreateInstance(shapeType, new object[] { Store, new PropertyAssignment[0] }) as ConfigurationShape;
                        if (shape != null)
                        {
                            shape.ModelElement = element;
                            shape.AbsoluteBounds = new RectangleD(creationPoint, shape.DefaultSize);

                            NestedChildShapes.Add(shape);

                            t.Commit();
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Collection of diagram items that will be emphasized.
    /// </summary>
    public sealed class EmphasizedShapes : DiagramItemCollection
    {
        /// <summary>
        ///     Adds the specified DiagramItems to the current emphasis list.
        /// </summary>
        /// <remarks>
        ///     If a DiagramItem in the collection is already in the emphasis list, the DiagramItem is ignored.
        /// </remarks>
        /// <param name="diagramItems">The collection of DiagramItems to add.</param>
        private void Add(DiagramItemCollection diagramItems)
        {
            // only add shapes that are not currently in the emphasis list.
            foreach (var diagramItem in diagramItems)
            {
                if (!Contains(diagramItem))
                {
                    base.Add(diagramItem);
                }
            }
        }

        /// <summary>
        ///     Replaces the current emphasis list with a new emphasis list.
        /// </summary>
        /// <param name="diagramItems">The collection of DiagramItems that is to replace the current emphasis list.</param>
        /// <remarks>
        ///     If the DiagramItemCollection is null, then the emphasis list is cleared.
        /// </remarks>
        internal void Set(DiagramItemCollection diagramItems)
        {
            Invalidate(); // Invalidate to ensure that the old shapes will be repainted.
            List.Clear();
            if (diagramItems != null)
            {
                Add(diagramItems);
                Invalidate();
            }
        }

        /// <summary>
        ///     Invalidates the current emphasis list of ShapeElements
        /// </summary>
        private void Invalidate()
        {
            foreach (DiagramItem diagramItem in List)
            {
                if (!(diagramItem.Shape is Diagram))
                {
                    diagramItem.Shape.Invalidate();
                }
            }
        }
    }
}
