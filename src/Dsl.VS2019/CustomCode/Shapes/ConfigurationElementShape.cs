using System;
using System.Drawing;
using ConfigurationSectionDesigner.Properties;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;

namespace ConfigurationSectionDesigner
{
    public partial class ConfigurationElementShape
    {
        public static readonly string AttributesCompartmentName = "AttributeProperties";
        public static readonly string ElementsCompartmentName = "ElementProperties";

        private bool _isAdjustedForFillColor;

        #region Provide Icons For Compartment Entries

        protected override CompartmentMapping[] GetCompartmentMappings(Type melType)
        {
            // The ConfigurationElementShape is defined as Double Derived, so we
            // can override this method and modify the return value to include an icon image getter.

            // First we retrieve the basic compartment mappings as configured in the DSL definition.
            CompartmentMapping[] mappings = base.GetCompartmentMappings(melType);

            // Then, for each compartment, we provide an image getter that determines the icon to display.
            foreach (ElementListCompartmentMapping mapping in mappings)
            {
                mapping.ImageGetter = CompartmentImageProvider;
            }
            return mappings;
        }

        /// <summary>
        /// Determines the icon to show in a compartment entry, based on its properties.
        /// </summary>
        /// <param name="element">The configuration property being shown in the compartment.</param>
        /// <returns>The icon to use to represent the configuration property.</returns>
        private Image CompartmentImageProvider(ModelElement element)
        {
            // Show icons.
            ConfigurationProperty prop = (ConfigurationProperty)element;
            if (prop is AttributeProperty)
            {
                if (prop.IsKey)
                {
                    return Resources.KeyProperty;
                }
                else if (prop.IsRequired)
                {
                    return Resources.RequiredProperty;
                }
                else
                {
                    return Resources.Property;
                }
            }
            else if (prop is ElementProperty)
            {
                if (prop.IsKey)
                {
                    return Resources.KeyElement;
                }
                else if (prop.IsRequired)
                {
                    return Resources.RequiredElement;
                }
                else
                {
                    return Resources.Element;
                }
            }
            return null;
        }

        #endregion

        internal static Color EmphasisShapeOutlineColor
        {
            get { return VSColorTheme.GetThemedColor(EnvironmentColors.ClassDesignerEmphasisBorderColorKey); }
        }

        /// <summary>
        ///     Were theme colors already applied?
        /// </summary>
        internal static bool IsColorThemeSet { get; set; }

        public override void OnPaintEmphasis(DiagramPaintEventArgs e)
        {
            if (!IsColorThemeSet)
            {
                SetColorTheme();
            }
            base.OnPaintEmphasis(e);
        }

        private void SetColorTheme()
        {
            ClassStyleSet.OverridePenColor(DiagramPens.EmphasisOutline, EmphasisShapeOutlineColor);
            // We shouldn't need to do this again uless the user changes the theme.
            IsColorThemeSet = true;
        }

        public override void OnPaintShape(DiagramPaintEventArgs e)
        {
            // Have we adjusted the other colors in the shape for this fill color?
            if (!_isAdjustedForFillColor)
            {
                AdjustForFillColor();
            }

            base.OnPaintShape(e);

            if (e.View != null)
            {
                // If the shape is in the EmphasizedShapes list, draw the emphasis shape around the shape.
                if (Diagram.EmphasizedShapes.Contains(new DiagramItem(this)))
                {
                    ShapeGeometry.DoPaintEmphasis(e, this);
                }
            }
        }

        public new ConfigurationSectionDesignerDiagram Diagram
        {
            get { return base.Diagram as ConfigurationSectionDesignerDiagram; }
        }

        private void AdjustForFillColor()
        {
            dynamic brush = StyleSet.GetBrush(DiagramBrushes.ShapeBackground);

            // If the shape is very dark, we make the title text white, and viceversa.
            StyleSet.OverrideBrushColor(DiagramBrushes.ShapeText, CachedFillColorAppearance((Color)brush.Color).TextColor);
            // We draw a thin outline of a sligtly different color to improve distinguishability when shape color looks like background (and because it looks good).
            StyleSet.OverridePenColor(OutlinePenId, CachedFillColorAppearance((Color)brush.Color).OutlineColor);
            // We shouldn't need to do this again unless the user changes the color for this shape.
            _isAdjustedForFillColor = true;
        }

        internal struct FillColorAppearance
        {
            public Color TextColor;
            public Color OutlineColor;
        }

        /// <summary>
        ///     Calculates appropriate colors and icons for a shape's fill color.
        /// </summary>
        private static FillColorAppearance CalculateFillColorAppearance(Color fillColor)
        {
            var hslColor = HslColor.FromRgbColor(fillColor);
            return new FillColorAppearance
            {
                TextColor = fillColor.GetBrightness() < 0.5 ? Color.FromArgb(255, 246, 246, 246) : Color.FromArgb(255, 9, 9, 9),
                OutlineColor
                    = new HslColor
                    {
                        Hue = hslColor.Hue,
                        Saturation = hslColor.Saturation * 3 / 5,
                        Luminosity = GetHighlightLuminosity(hslColor.Luminosity)
                    }.ToRgbColor(),
            };
        }

        /// <summary>
        ///     Common part of formula to calculate outline and highlight colors.
        /// </summary>
        private static int GetHighlightLuminosity(int currentLuminosity)
        {
            return currentLuminosity < 120 ? currentLuminosity + 40 : currentLuminosity - 40;
        }

        protected override int ModifyLuminosity(int currentLuminosity, DiagramClientView view)
        {
            if (view.HighlightedShapes.Contains(new DiagramItem(this)))
            {
                return GetHighlightLuminosity(currentLuminosity);
            }
            return currentLuminosity;
        }

        /// <summary>
        ///     This method is used to cache the results of a function so the funcion is ever at most invoked
        ///     once for each possible value of the argument and only one copy of the result for every value
        ///     of the argument is ever allocated in memory
        /// </summary>
        /// <typeparam name="TArg">the type of the argument of the function</typeparam>
        /// <typeparam name="TResult">the type of the result of the function</typeparam>
        /// <param name="func">original function</param>
        /// <returns>"memoized" version of the original function</returns>
        /// <remarks>
        ///     The correct usage of the Memoize method is to only call it once passing as an argument the
        ///     function to memoize. The returned "memoized" function can be invoked many times to take
        ///     advantage of the cached results. This is a good general reference on memoization:
        ///     http://en.wikipedia.org/wiki/Memoization
        /// </remarks>
        private static Func<TArg, TResult> Memoize<TArg, TResult>(Func<TArg, TResult> func)
        {
            var map = new Dictionary<TArg, TResult>();
            return arg =>
            {
                TResult result;
                if (!map.TryGetValue(arg, out result))
                {
                    result = func(arg);
                    map.Add(arg, result);
                }
                return result;
            };
        }

        private static readonly Func<Color, FillColorAppearance> CachedFillColorAppearance
            = Memoize<Color, FillColorAppearance>(CalculateFillColorAppearance);

        /// <summary>
        ///     Remove shadow from the shape.
        /// </summary>
        public override bool HasShadow
        {
            get { return false; }
        }

        /// <summary>
        /// Keeps the size of the expanded bounds of the shape
        /// </summary>
        protected RectangleD ExpandedBounds;

        protected override void Collapse()
        {
            ExpandedBounds = Bounds;
            base.Collapse();
        }

        protected override void Expand()
        {
            base.Expand();
            Bounds = ExpandedBounds;
        }
    }
}