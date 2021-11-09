
namespace ConfigurationSectionDesigner
{
    public partial class ConfigurationShape
    {
        /// <summary>
        /// Gets the child compartment shape and verifies whether it can resize its parent compartment shape.
        /// </summary>
        /// <value></value>
        /// <returns>true if a child compartment shape can resize its parent compartment shape; otherwise, false.
        /// </returns>
        public override bool AllowsChildrenToResizeParent
        {
            get
            {
                // Overridden to return false so that the visual layout issue is solved, where a shape with a connector that "has custom source"
                // resizes to make the source shape wider until it reaches the edge of the referenced shape.
                // See http://social.msdn.microsoft.com/Forums/en-US/vsx/thread/4cc74f9e-1949-43ba-8407-934f6664167d.
                // It is applied to the base class here so that all inheriting compartment shapes automatically have this fix applied.
                return false;
            }
        }
    }
}