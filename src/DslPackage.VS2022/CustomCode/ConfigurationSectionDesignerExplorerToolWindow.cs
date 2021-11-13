using Microsoft.VisualStudio.Modeling.Shell;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ConfigurationSectionDesigner
{

  internal partial class ConfigurationSectionDesignerExplorerToolWindow : ConfigurationSectionDesignerExplorerToolWindowBase
  {

    private IVsUIShell5 _shell5;

    protected override ModelExplorerTreeContainer CreateTreeContainer()
    {
      ModelExplorerTreeContainer treeContainer = base.CreateTreeContainer();

      treeContainer.ObjectModelBrowser.DrawMode = TreeViewDrawMode.OwnerDrawAll;
      treeContainer.ObjectModelBrowser.DrawNode += onDrawNode;
      treeContainer.ObjectModelBrowser.FullRowSelect = true;
      treeContainer.ObjectModelBrowser.NodeMouseClick += onNodeMouseClick;

      return treeContainer;
    }

    protected override void Initialize()
    {
      base.Initialize();

      _shell5 = Package.GetGlobalService(typeof(SVsUIShell)) as IVsUIShell5;
    }

    private void drawBackground(DrawTreeNodeEventArgs e)
    {
      Color backgroundColor = VsColors.GetThemedGDIColor(_shell5, TreeViewColors.BackgroundColorKey);
      if (e.Node.TreeView.BackColor != backgroundColor) { e.Node.TreeView.BackColor = backgroundColor; }
    }

    private static void drawExpandCollapseIcon(DrawTreeNodeEventArgs e)
    {
      Point expandIconPoint = new Point(e.Node.Bounds.Location.X - 20, e.Node.Bounds.Location.Y + 2);
      Size expandIconSize = new Size(16, 16);
      Rectangle expandIconRectangle = new Rectangle(expandIconPoint, expandIconSize);
      if (e.Node.Nodes.Count > 0)
      {
        if (e.Node.IsExpanded)
        {
          VisualStyleRenderer treeOpen = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Opened);
          treeOpen.DrawBackground(e.Graphics, expandIconRectangle);
        }
        else
        {
          VisualStyleRenderer treeClose = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Closed);
          treeClose.DrawBackground(e.Graphics, expandIconRectangle);
        }
      }
    }

    private static void drawNodeIcon(DrawTreeNodeEventArgs e)
    {
      Point nodeIconPoint = new Point(e.Node.Bounds.Location.X - 4, e.Node.Bounds.Location.Y + 2);
      Image nodeIconImage = e.Node.TreeView.ImageList.Images[e.Node.ImageIndex];
      e.Graphics.DrawImage(nodeIconImage, nodeIconPoint);
    }

    private void drawNodeSelection(object sender, DrawTreeNodeEventArgs e)
    {
      if (e.Node == e.Node.TreeView.SelectedNode)
      {
        if (((TreeView)sender).Focused)
        {
          Color selectedItemActiveColor = VsColors.GetThemedGDIColor(_shell5, TreeViewColors.SelectedItemActiveColorKey);
          Color focusVisualBorderColorKey = VsColors.GetThemedGDIColor(_shell5, TreeViewColors.FocusVisualBorderColorKey);
          e.Graphics.FillRectangle(new SolidBrush(selectedItemActiveColor), e.Bounds);
          ControlPaint.DrawBorder(e.Graphics, e.Bounds, focusVisualBorderColorKey, ButtonBorderStyle.Solid);
        }
        else
        {
          Color selectedItemInactiveColor = VsColors.GetThemedGDIColor(_shell5, TreeViewColors.SelectedItemInactiveColorKey);
          e.Graphics.FillRectangle(new SolidBrush(selectedItemInactiveColor), e.Bounds);
        }
      }
    }

    private void drawNodeText(object sender, DrawTreeNodeEventArgs e)
    {
      Font nodeTextFont = e.Node.NodeFont;
      if (nodeTextFont == null) { nodeTextFont = ((TreeView)sender).Font; }
      Color nodeTextColor;
      if (e.Node == e.Node.TreeView.SelectedNode)
      {
        Color selectedItemActiveTextColor = VsColors.GetThemedGDIColor(_shell5, TreeViewColors.SelectedItemActiveTextColorKey);
        nodeTextColor = selectedItemActiveTextColor;
      }
      else
      {
        Color backTextColor = VsColors.GetThemedGDIColor(_shell5, TreeViewColors.BackgroundTextColorKey);
        nodeTextColor = backTextColor;
      }
      Rectangle nodeTextRectangle = e.Node.Bounds;
      nodeTextRectangle.Width += 40;
      TextRenderer.DrawText(e.Graphics, e.Node.Text, nodeTextFont, Rectangle.Inflate(nodeTextRectangle, -15, 0), nodeTextColor, TextFormatFlags.GlyphOverhangPadding);
    }

    private void onDrawNode(object sender, DrawTreeNodeEventArgs e)
    {
      drawBackground(e);
      drawNodeSelection(sender, e);
      drawExpandCollapseIcon(e);
      drawNodeIcon(e);
      drawNodeText(sender, e);
    }

    private void onNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if ((e.Location.X > e.Node.Bounds.Location.X - 20) &&
          (e.Location.X < e.Node.Bounds.Location.X - 4) &&
          (e.Location.Y > e.Node.Bounds.Location.Y + 2) &&
          (e.Location.Y < e.Node.Bounds.Location.Y + 18))
      {
        e.Node.Toggle();
      }
    }

  }
}
