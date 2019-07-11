using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restward
{
    public partial class CustomDrawnTabControl : TabControl
    {
        private int m_HotLightIndex = -1;
        private bool m_CloseButtonHot = false;
        private Image m_CloseButtonActive = null;
        private Image m_CloseButtonInactive = null;
        private bool m_CloseButtonUsed = false;
        private bool m_AllowClosingTabs = true;

        public delegate void TabClosingEventHandler(object sender, TabClosingEventArgs e);
        public event TabClosingEventHandler OnTabClosing;

        public CustomDrawnTabControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer, true);

            this.DrawMode = TabDrawMode.OwnerDrawFixed;

            this.MouseClick += new MouseEventHandler(CustomDrawnTabControl_MouseClick);
            this.MouseMove += new MouseEventHandler(CustomDrawnTabControl_MouseMove);
            this.MouseLeave += new EventHandler(CustomDrawnTabControl_MouseLeave);
        }

        public Image CloseButtonActive
        {
            get
            {
                return m_CloseButtonActive;
            }
            set
            {
                if (m_CloseButtonActive != value)
                {
                    m_CloseButtonActive = value;
                    if (m_CloseButtonActive != null && m_CloseButtonInactive != null)
                    {
                        m_CloseButtonUsed = true;
                    }
                    else
                    {
                        m_CloseButtonUsed = false;
                    }
                }
            }
        }

        public Image CloseButtonInactive
        {
            get
            {
                return m_CloseButtonInactive;
            }
            set
            {
                if (m_CloseButtonInactive != value)
                {
                    m_CloseButtonInactive = value;
                    if (m_CloseButtonActive != null && m_CloseButtonInactive != null)
                    {
                        m_CloseButtonUsed = true;
                    }
                    else
                    {
                        m_CloseButtonUsed = false;
                    }
                }
            }
        }

        public bool AllowClosingTabs
        {
            get
            {
                return m_AllowClosingTabs;
            }
            set
            {
                if (m_AllowClosingTabs != value)
                {
                    m_AllowClosingTabs = value;
                }
            }
        }


        private Dictionary<TabPage, BufferedGraphics> StoredGraphics = new Dictionary<TabPage, BufferedGraphics>();

        private void AllocateGraphics(TabPage tabPage, DrawItemEventArgs e)
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            if (!StoredGraphics.ContainsKey(tabPage))
            {
                StoredGraphics.Add(tabPage, context.Allocate(e.Graphics, e.Bounds));
            }
            else
            {
                StoredGraphics[tabPage] = context.Allocate(e.Graphics, e.Bounds);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush BackgroundBrush = new SolidBrush(ColorManager.WorkspaceBackground);
            e.Graphics.Clear(Color.Transparent);
            if ((this.Parent != null))
            {
                Rectangle clipRect = this.ClientRectangle;
                clipRect.Offset(this.Location);
                PaintEventArgs args = new PaintEventArgs(e.Graphics, clipRect);
                GraphicsState state = e.Graphics.Save();
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                try
                {
                    e.Graphics.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                    this.InvokePaintBackground(this.Parent, e);
                    this.InvokePaint(this.Parent, e);
                }
                finally
                {
                    e.Graphics.Restore(state);
                    clipRect.Offset(-this.Location.X, -this.Location.Y);
                }
            }

            foreach (TabPage tabPage in this.TabPages)
            {
                TabPage TabPage = tabPage;
                Rectangle TabBounds = this.GetTabRect(this.TabPages.IndexOf(TabPage));
                TabBounds.X += 1;
                if (this.SelectedTab == TabPage)
                {
                    if (this.Alignment == TabAlignment.Bottom)
                    {
                        TabBounds.Height += 4;
                        TabBounds.Y -= 3;
                    }
                    else
                    {
                        TabBounds.Height += 3;
                        TabBounds.Y -= 1;
                    }
                }

                GraphicsPath RoundedTabPath;
                if (this.Alignment == TabAlignment.Bottom)
                {
                    RoundedTabPath = GetRoundRect(TabBounds.X, TabBounds.Y -1, TabBounds.Width - 1, TabBounds.Height, 2);
                }
                else
                {
                    RoundedTabPath = GetRoundRect(TabBounds.X, TabBounds.Y, TabBounds.Width - 1, TabBounds.Height, 2);
                }
                bool IsTabPageHot = (m_HotLightIndex == this.TabPages.IndexOf(TabPage));

                BufferedGraphicsContext context = BufferedGraphicsManager.Current;
                Rectangle BufferRectangle = TabBounds;
                if (this.SelectedTab == TabPage)
                {
                    if (this.Alignment == TabAlignment.Bottom)
                    {
                        BufferRectangle.Height -= 1;
                        BufferRectangle.Y += 1;
                    }
                    else
                    {
                        //BufferRectangle.Height += 3;
                        //BufferRectangle.Y -= 1;
                    }
                }

                if (!StoredGraphics.ContainsKey(TabPage))
                {
                    StoredGraphics.Add(TabPage, context.Allocate(e.Graphics, BufferRectangle));
                }
                else
                {
                    StoredGraphics[TabPage] = context.Allocate(e.Graphics, BufferRectangle);
                }

                BufferedGraphics bufGraphics = StoredGraphics[TabPage];

                //fill background
                bufGraphics.Graphics.FillRectangle(BackgroundBrush, BufferRectangle);

                //check tab sizes
                int MaxTabTextWidth = (int)this.TabPages.Cast<TabPage>().Select(c => bufGraphics.Graphics.MeasureString(c.Text, TabPage.Font).Width).Max();
                int MaxTabWidth = MaxTabTextWidth + 23;
                if (this.ItemSize.Width != MaxTabWidth)
                {
                    this.ItemSize = new Size(MaxTabWidth, this.ItemSize.Height);
                }

                Color m_BeginGradient = ColorManager.TabBeginGradient;//Color.FromArgb(195, 195, 195);
                Color m_EndGradient = ColorManager.TabEndGradient;// Color.FromArgb(110, 110, 110);
                if (this.SelectedTab == TabPage)
                {
                    m_BeginGradient = ColorManager.TabSelectedBeginGradient;// Color.FromArgb(255, 195, 0);
                    m_EndGradient = ColorManager.TabSelectedEndGradient;// Color.FromArgb(255, 110, 0);
                }
                if (IsTabPageHot)
                {
                    if (this.SelectedTab == TabPage)
                    {
                        m_BeginGradient = ColorManager.TabHoverSelectedBeginGradient;// Color.FromArgb(255, 215, 0);
                        m_EndGradient = ColorManager.TabHoverSelectedEndGradient;// Color.FromArgb(255, 130, 0);
                    }
                    else
                    {
                        m_BeginGradient = ColorManager.TabHoverBeginGradient;// Color.FromArgb(215, 215, 215);
                        m_EndGradient = ColorManager.TabHoverEndGradient;// Color.FromArgb(130, 130, 130);
                    }
                }

                if (this.SelectedTab == TabPage)
                {
                    Pen BorderPen = new Pen(Color.White, 4);
                    Pen BorderOutlinePen = new Pen(Color.FromArgb(137, 140, 149), 1);
                    Rectangle TabPageBorder = new Rectangle(TabPage.Bounds.X, TabPage.Bounds.Y, TabPage.Bounds.Width, TabPage.Bounds.Height + 2);
                    Rectangle TabPageOutline = new Rectangle(TabPage.Bounds.X - 1, TabPage.Bounds.Y - 1, TabPage.Bounds.Width + 1, TabPage.Bounds.Height + 1);

                    Rectangle BorderBufferRectangle = TabPageOutline;
                    BorderBufferRectangle.Width += 2;
                    BorderBufferRectangle.Height += 1;

                    BufferedGraphics BorderBufGraphics = context.Allocate(e.Graphics, BorderBufferRectangle);

                    BorderBufGraphics.Graphics.FillRectangle(BackgroundBrush, BorderBufferRectangle);

                    BorderBufGraphics.Graphics.FillRectangle(BackgroundBrush, TabPageBorder);
                    BorderBufGraphics.Graphics.DrawRectangle(BorderPen, TabPageBorder);
                    BorderBufGraphics.Graphics.DrawRectangle(BorderOutlinePen, TabPageOutline);

                    BorderBufGraphics.Render(e.Graphics);
                }

                LinearGradientMode mode = LinearGradientMode.Vertical;

                if (this.SelectedTab == TabPage)
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(TabBounds, m_BeginGradient, m_EndGradient, mode))
                    {
                        bufGraphics.Graphics.FillRectangle(BackgroundBrush, TabBounds);
                        bufGraphics.Graphics.FillPath(b, RoundedTabPath);
                        bufGraphics.Graphics.DrawPath(Pens.DimGray, RoundedTabPath);
                        //bufGraphics.Graphics.FillRectangle(b, TabBounds);
                    }

                    if (m_CloseButtonUsed && AllowClosingTabs)
                    {
                        Rectangle XRectangle = new Rectangle(TabBounds.Right - 16, TabBounds.Top + 5, 13, 12);
                        if (IsTabPageHot && m_CloseButtonHot)
                        {
                            bufGraphics.Graphics.DrawImage(m_CloseButtonActive, XRectangle);
                        }
                        else
                        {
                            bufGraphics.Graphics.DrawImage(m_CloseButtonInactive, XRectangle);
                        }
                    }

                    bufGraphics.Graphics.DrawString(TabPage.Text, TabPage.Font, Brushes.Black, TabBounds.Left + 6, TabBounds.Top + 3);
                }
                else
                {
                    Rectangle UnselectedTabBounds = TabBounds;
                    //if (this.Alignment == TabAlignment.Bottom)
                    //{
                    //    UnselectedTabBounds.Height += 2;
                    //    UnselectedTabBounds.Y -= 3;
                    //}
                    //else
                    //{
                    //    UnselectedTabBounds.Height += 2;
                    //    UnselectedTabBounds.Y += 1;
                    //}
                    using (LinearGradientBrush b = new LinearGradientBrush(UnselectedTabBounds, m_BeginGradient, m_EndGradient, mode))
                    {
                        bufGraphics.Graphics.FillRectangle(BackgroundBrush, UnselectedTabBounds);
                        bufGraphics.Graphics.FillPath(b, RoundedTabPath);
                        bufGraphics.Graphics.DrawPath(Pens.DimGray, RoundedTabPath);
                        //bufGraphics.Graphics.FillRectangle(b, UnselectedTabBounds);
                    }

                    if (IsTabPageHot && m_CloseButtonUsed && AllowClosingTabs)
                    {
                        //render close button
                        Rectangle XRectangle = new Rectangle(TabBounds.Right - 16, TabBounds.Top + 4, 13, 12);
                        if (m_CloseButtonHot)
                        {
                            bufGraphics.Graphics.DrawImage(m_CloseButtonActive, XRectangle);
                        }
                        else
                        {
                            bufGraphics.Graphics.DrawImage(m_CloseButtonInactive, XRectangle);
                        }
                    }

                    bufGraphics.Graphics.DrawString(TabPage.Text, TabPage.Font, Brushes.Black, UnselectedTabBounds.Left + 6, UnselectedTabBounds.Top + 1);
                }

                //dump buffer
                bufGraphics.Render(e.Graphics);
            }

            base.OnPaint(e);
        }

        private void CustomDrawnTabControl_MouseLeave(object sender, EventArgs e)
        {
            if (m_HotLightIndex > -1)
            {
                int TabIndexToRedraw = m_HotLightIndex;

                m_HotLightIndex = -1;
                m_CloseButtonHot = false;
                //DrawItemIntoBuffer(this.TabPages[TabIndexToRedraw]);
                this.Invalidate();
            }
        }

        private void CustomDrawnTabControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool tabFound = false;
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                Rectangle closeButtonRectangle = this.GetTabRect(i);
                if (closeButtonRectangle.Contains(e.Location))
                {
                    tabFound = true;
                    if (m_HotLightIndex != i)
                    {
                        m_HotLightIndex = i;
                        //DrawItemIntoBuffer(this.TabPages[m_HotLightIndex]);
                        this.Invalidate();
                    }

                    Rectangle closeButton = new Rectangle(closeButtonRectangle.Right - 16, closeButtonRectangle.Top + 5, 13, 12);
                    if (closeButton.Contains(e.Location))
                    {
                        if (m_CloseButtonHot != true)
                        {
                            m_CloseButtonHot = true;
                            //DrawItemIntoBuffer(this.TabPages[m_HotLightIndex]);
                            this.Invalidate();
                        }
                    }
                    else
                    {
                        if (m_CloseButtonHot != false)
                        {
                            m_CloseButtonHot = false;
                            //DrawItemIntoBuffer(this.TabPages[m_HotLightIndex]);
                            this.Invalidate();
                        }
                    }
                }
            }

            if (!tabFound)
            {
                m_HotLightIndex = -1;
                m_CloseButtonHot = false;
            }
        }

        private void CustomDrawnTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (AllowClosingTabs)
            {
                for (int i = 0; i < this.TabPages.Count; i++)
                {
                    Rectangle r = this.GetTabRect(i);
                    //Getting the position of the "x" mark.
                    Rectangle closeButton = new Rectangle(r.Right - 16, r.Top + 5, 13, 12);
                    if (closeButton.Contains(e.Location))
                    {
                        TabClosingEventArgs tabClosingArgs = new TabClosingEventArgs(this.TabPages[i]);
                        if (OnTabClosing != null)
                        {
                            OnTabClosing(this, tabClosingArgs);
                        }
                        if (!tabClosingArgs.Cancel)
                        {
                            this.TabPages.RemoveAt(i);
                            if (i < this.TabPages.Count)
                            {
                                this.SelectedIndex = i;
                            }
                            else
                            {
                                this.SelectedIndex = i - 1;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    public class TabClosingEventArgs : EventArgs
    {
        private TabPage m_TabPage;
        private bool m_Cancel = false;

        public TabClosingEventArgs(TabPage tabPage)
        {
            m_TabPage = tabPage;
        }

        public TabPage TabPage
        {
            get
            {
                return m_TabPage;
            }
        }

        public bool Cancel
        {
            get
            {
                return m_Cancel;
            }
            set
            {
                if (m_Cancel != value)
                {
                    m_Cancel = value;
                }
            }
        }
    }
}
