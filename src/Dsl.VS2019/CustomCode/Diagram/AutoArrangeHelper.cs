using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace ConfigurationSectionDesigner.CustomCode.Diagram
{
    internal class AutoArrangeHelper
    {
        /// <summary>
        ///     This keeps track of new diagram objects that were added
        ///     during the model update. These can then be autoarranged
        ///     before the transaction has finished
        /// </summary>
        internal struct NewObjectTracker
        {
            public bool Tracking;
            public List<ShapeElement> Objects;
            public List<ShapeElement> HiddenObjects;

            public void Start()
            {
                if (Objects == null)
                {
                    Objects = new List<ShapeElement>();
                }
                if (HiddenObjects == null)
                {
                    HiddenObjects = new List<ShapeElement>();
                }

                Tracking = true;
            }

            public void End()
            {
                Tracking = false;
                Objects.Clear();
                HiddenObjects.Clear();
            }
        }

        private NewObjectTracker _autoArrangeInfo;
        private PointD _autoArrangeDropPoint;

        public void Start(PointD dropPoint)
        {
            if (!_autoArrangeInfo.Tracking)
            {
                _autoArrangeDropPoint = dropPoint;
                _autoArrangeInfo.Start();
            }
        }

        public void End()
        {
            _autoArrangeInfo.End();
        }

        public void TransactionCommit(ConfigurationSectionDesignerDiagram diagram)
        {
            //If this is a Drag & Drop from the SE transaction, then arrange the new elements
            if (_autoArrangeInfo.Tracking)
            {
                if (_autoArrangeInfo.Objects.Count > 0
                    || _autoArrangeInfo.HiddenObjects.Count > 0)
                {
                    //Arrange the new elemenst before the transaction finishes:
                    using (var t = diagram.Store.TransactionManager.BeginTransaction("LayoutDiagram"))
                    {
                        //Place single object where the user dropped, and multiple get autoarranged
                        if (_autoArrangeInfo.Objects.Count > 0)
                        {
                            //Place the first entity-type-shape to where the user released the mouse and auto layout the rest.
                            var haveDropPoint = (!_autoArrangeDropPoint.IsEmpty);
                            var shapesToAutoLayout = new List<ShapeElement>();

                            var hasSetUserDefinedPosition = false;

                            foreach (var shape in _autoArrangeInfo.Objects)
                            {
                                var firstShape = shape as ConfigurationElementShape;
                                if (haveDropPoint
                                    && firstShape != null
                                    && !hasSetUserDefinedPosition)
                                {
                                    firstShape.Location = _autoArrangeDropPoint;
                                    hasSetUserDefinedPosition = true;
                                }
                                else
                                {
                                    shapesToAutoLayout.Add(shape);
                                }
                            }

                            if (shapesToAutoLayout.Count > 0)
                            {
                                diagram.AutoLayoutDiagram(shapesToAutoLayout);
                            }

                            t.Commit();
                        }

                        _autoArrangeInfo.Objects.Clear();
                    }

                    //Add hidden objects to 0,0
                    if (_autoArrangeInfo.HiddenObjects.Count > 0)
                    {
                        foreach (var se in _autoArrangeInfo.HiddenObjects)
                        {
                            var cs = se as ConfigurationElementShape;
                            cs.Location = new PointD(0, 0);
                        }

                        _autoArrangeInfo.HiddenObjects.Clear();
                    }
                }
            }
        }

        public void Add(ShapeElement addedShape, bool hide)
        {
            if (addedShape == null)
            {
                return;
            }

            if (_autoArrangeInfo.Tracking)
            {
                if (hide)
                {
                    _autoArrangeInfo.HiddenObjects.Add(addedShape);
                }
                else
                {
                    _autoArrangeInfo.Objects.Add(addedShape);
                }
            }
        }
    }
}
