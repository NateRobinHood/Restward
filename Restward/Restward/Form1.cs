using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restward
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GradientToolStripRenderer GradientRenderer = new GradientToolStripRenderer(Color.FromArgb(195, 195, 195), Color.FromArgb(110, 110, 110));
            GradientRenderer.RoundedEdges = false;
            MenuToolStripRenderer MenuRenderer = new MenuToolStripRenderer();

            menuStripMain.Renderer = MenuRenderer;
            toolStripServices.Renderer = GradientRenderer;

            Assembly ThisAssembly = Assembly.GetExecutingAssembly();
            Stream XButtonRedImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.CloseRed.png");
            Stream XButtonGrayImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.CloseGray.png");
            Image XButtonRedImage = Image.FromStream(XButtonRedImageStream);
            Image XButtonGrayImage = Image.FromStream(XButtonGrayImageStream);

            Stream TreeViewExpandedImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.TreeViewExpanded.png");
            Stream TreeViewCollapsedImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.TreeViewCollapsed.png");
            Image TreeViewExpandedImage = Image.FromStream(TreeViewExpandedImageStream);
            Image TreeViewCollapsedImage = Image.FromStream(TreeViewCollapsedImageStream);

            Stream WorkspaceImageStream = ThisAssembly.GetManifestResourceStream("Restward.Resources.WorkspaceBackground.png");
            Image WorkspaceImage = Image.FromStream(WorkspaceImageStream);

            this.tabControlMain.CloseButtonActive = XButtonRedImage;
            this.tabControlMain.CloseButtonInactive = XButtonGrayImage;

            this.treeViewServices.ExpandedImage = TreeViewExpandedImage;
            this.treeViewServices.CollapsedImage = TreeViewCollapsedImage;

            this.scMain.Panel2.BackgroundImage = WorkspaceImage;
            this.scMain.Panel2.BackgroundImageLayout = ImageLayout.Tile;
        }
    }
}
