using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Restward
{
    public partial class CustomDrawnTreeView : TreeView
    {
        private Image m_ExpandedImage = null;
        private Image m_CollapsedImage = null;
        private Font m_RenderFont;

        public CustomDrawnTreeView()
        {
            InitializeComponent();

            m_RenderFont = this.Font;

            this.DrawMode = TreeViewDrawMode.OwnerDrawText;
            //this.Indent = 19;
            this.ItemHeight = 17;

            this.DrawNode += new DrawTreeNodeEventHandler(TreeView_DrawNode);
            this.MouseMove += new MouseEventHandler(TreeView_MouseMove);
            this.MouseLeave += new EventHandler(TreeView_MouseLeave);
            this.MouseClick += new MouseEventHandler(TreeView_MouseClick);
            this.AfterExpand += new TreeViewEventHandler(TreeView_AfterExpand);

            this.Font = new Font(m_RenderFont.FontFamily.Name, m_RenderFont.Size + 0.5f);
        }

        public Image ExpandedImage
        {
            get
            {
                return m_ExpandedImage;
            }
            set
            {
                if (m_ExpandedImage != value)
                {
                    m_ExpandedImage = value;
                }
            }
        }

        public Image CollapsedImage
        {
            get
            {
                return m_CollapsedImage;
            }
            set
            {
                if (m_CollapsedImage != value)
                {
                    m_CollapsedImage = value;
                }
            }
        }

        private GraphicsPath GetRoundRect(float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(x + radius, y, x + width - (radius * 2), y); // Line
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90); // Corner
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2)); // Line
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90); // Corner
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height); // Line
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90); // Corner
            gp.AddLine(x, y + height - (radius * 2), x, y + radius); // Line
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90); // Corner
            gp.CloseFigure();

            return gp;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }

        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        //above solution fixes treeview flickering, http://stackoverflow.com/questions/10362988/treeview-flickering

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = false;

            if (e.Node.Bounds.IsEmpty)
                return;

            if (this.DrawMode == TreeViewDrawMode.OwnerDrawText)
            {
                Rectangle Bounds = e.Bounds;
                Bounds.Width += 4;
                Rectangle NodeBounds = e.Node.Bounds;

                int BoundsAdjustment = Convert.ToInt32(e.Graphics.MeasureString(e.Node.Text, this.Font).Width) - Convert.ToInt32(e.Graphics.MeasureString(e.Node.Text, m_RenderFont).Width);
                NodeBounds.Width -= (BoundsAdjustment + (BoundsAdjustment / 2));
                if (NodeBounds.Width == 0 && e.Node.Text != string.Empty)
                    NodeBounds.Width = Convert.ToInt32(e.Graphics.MeasureString(e.Node.Text, m_RenderFont).Width);
                GraphicsPath RoundedBounds = GetRoundRect(NodeBounds.X, NodeBounds.Y + 1, NodeBounds.Width + 4, NodeBounds.Height - 2, 2);

                BufferedGraphicsContext context = BufferedGraphicsManager.Current;
                BufferedGraphics BufGraphics = context.Allocate(e.Graphics, Bounds);

                Brush BackgroundBrush = new SolidBrush(this.BackColor);
                BufGraphics.Graphics.FillRectangle(BackgroundBrush, Bounds);

                try
                {
                    if (e.Node == this.SelectedNode)
                    {
                        Pen BorderPen = Pens.LightGray;
                        Color BeginGradient = Color.FromArgb(240, 240, 240);
                        Color EndGradient = Color.FromArgb(219, 219, 219);
                        if (this.Focused)
                        {
                            BorderPen = Pens.DimGray;
                            BeginGradient = Color.FromArgb(255, 195, 0);
                            EndGradient = Color.FromArgb(255, 110, 0);
                        }
                        LinearGradientMode GradientMode = LinearGradientMode.Vertical;

                        //Rectangle HighlightBounds = new Rectangle(NodeBounds.Location, NodeBounds.Size);
                        //if (HasImage)
                        //{
                        //    HighlightBounds.Width += (ImageWidth + 4);
                        //    HighlightBounds.X -= (ImageWidth + 4);

                        //    RoundedBounds = GetRoundRect(HighlightBounds.X, HighlightBounds.Y + 1, HighlightBounds.Width + 4, HighlightBounds.Height - 2, 3);
                        //}

                        using (LinearGradientBrush b = new LinearGradientBrush(NodeBounds, BeginGradient, EndGradient, GradientMode))
                        {
                            BufGraphics.Graphics.FillPath(b, RoundedBounds);
                        }
                        BufGraphics.Graphics.DrawPath(BorderPen, RoundedBounds);
                    }
                }
                catch
                {
                }

                try
                {
                    Color fontColor = this.ForeColor;
                    SolidBrush NodeTextBrush = new SolidBrush(Color.Black);
                    PointF TextRenderPoint = new PointF(NodeBounds.X, NodeBounds.Y + 2);
                    BufGraphics.Graphics.DrawString(e.Node.Text, m_RenderFont, Brushes.Black, TextRenderPoint);
                }
                catch
                {
                }

                BufGraphics.Render(e.Graphics);
            }
            else if (this.DrawMode == TreeViewDrawMode.OwnerDrawAll)
            {
                Rectangle Bounds = e.Bounds;
                Rectangle NodeBounds = e.Node.Bounds;
                if (NodeBounds.Width == 0 && e.Node.Text != string.Empty)
                    NodeBounds.Width = Convert.ToInt32(e.Graphics.MeasureString(e.Node.Text, m_RenderFont).Width);
                GraphicsPath RoundedBounds = GetRoundRect(NodeBounds.X, NodeBounds.Y + 1, NodeBounds.Width + 4, NodeBounds.Height - 2, 2);

                BufferedGraphicsContext context = BufferedGraphicsManager.Current;
                BufferedGraphics BufGraphics = context.Allocate(e.Graphics, Bounds);

                Brush BackgroundBrush = new SolidBrush(this.BackColor);
                BufGraphics.Graphics.FillRectangle(BackgroundBrush, Bounds);

                bool HasImage = false;
                int ImageWidth = 0;
                Image NodeImage = null;
                if (this.ImageList != null && e.Node.ImageIndex < this.ImageList.Images.Count)
                {
                    int ImageIndex = e.Node.ImageIndex >= 0 ? e.Node.ImageIndex : 0;
                    NodeImage = this.ImageList.Images[ImageIndex];
                    if (NodeImage != null)
                    {
                        HasImage = true;
                        ImageWidth = NodeBounds.Height;
                    }
                }

                try
                {
                    if (e.Node == this.SelectedNode)
                    {
                        Pen BorderPen = Pens.LightGray;
                        Color BeginGradient = Color.FromArgb(230, 230, 230);
                        Color EndGradient = Color.FromArgb(209, 209, 209);
                        if (this.Focused)
                        {
                            BorderPen = Pens.DimGray;
                            BeginGradient = Color.FromArgb(255, 195, 0);
                            EndGradient = Color.FromArgb(255, 110, 0);
                        }
                        LinearGradientMode GradientMode = LinearGradientMode.Vertical;

                        //Rectangle HighlightBounds = new Rectangle(NodeBounds.Location, NodeBounds.Size);
                        //if (HasImage)
                        //{
                        //    HighlightBounds.Width += (ImageWidth + 4);
                        //    HighlightBounds.X -= (ImageWidth + 4);

                        //    RoundedBounds = GetRoundRect(HighlightBounds.X, HighlightBounds.Y + 1, HighlightBounds.Width + 4, HighlightBounds.Height - 2, 3);
                        //}

                        using (LinearGradientBrush b = new LinearGradientBrush(NodeBounds, BeginGradient, EndGradient, GradientMode))
                        {
                            BufGraphics.Graphics.FillPath(b, RoundedBounds);
                        }
                        BufGraphics.Graphics.DrawPath(BorderPen, RoundedBounds);
                    }
                }
                catch
                {
                }

                try
                {
                    Color fontColor = this.ForeColor;
                    SolidBrush NodeTextBrush = new SolidBrush(Color.Black);
                    BufGraphics.Graphics.DrawString(e.Node.Text, m_RenderFont, Brushes.Black, new PointF(NodeBounds.X, NodeBounds.Y + 2));
                }
                catch
                {
                }

                try
                {
                    if (NodeImage != null)
                    {
                        Point ImagePoint = new Point((NodeBounds.X - 2) - NodeBounds.Height, NodeBounds.Y);
                        Rectangle ImageRect = new Rectangle(ImagePoint, new Size(NodeBounds.Height, NodeBounds.Height));
                        //BufGraphics.Graphics.DrawImage(img, ImagePoint);
                        BufGraphics.Graphics.DrawImage(NodeImage, ImageRect);
                    }
                }
                catch
                {
                }

                try
                {
                    if (e.Node.Nodes.Count > 0 && ExpandedImage != null && CollapsedImage != null)
                    {
                        Point ImagePoint = new Point(NodeBounds.X - 10, NodeBounds.Y + 4);
                        if (HasImage)
                        {
                            ImagePoint = new Point((NodeBounds.X - 12) - ImageWidth, NodeBounds.Y + 4);
                        }

                        if (e.Node.IsExpanded)
                        {
                            BufGraphics.Graphics.DrawImage(ExpandedImage, ImagePoint);
                        }
                        else
                        {
                            BufGraphics.Graphics.DrawImage(CollapsedImage, ImagePoint);
                        }
                    }
                }
                catch
                {
                }

                BufGraphics.Render(e.Graphics);
            }
        }

        private void TreeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeViewHitTestInfo hitTestInfo = this.HitTest(e.Location);
                if (hitTestInfo.Node != null)
                {
                    this.SelectedNode = hitTestInfo.Node;
                }
            }
        }

        private void TreeView_MouseLeave(object sender, EventArgs e)
        {
        }

        private void TreeView_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            //this.Invalidate();
        }
    }
}
