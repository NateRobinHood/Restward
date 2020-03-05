using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restward
{
    public partial class CustomDrawnListView : ListView
    {
        private int HotLightIndex = -1;
        private bool IsMouseDown = false;

        public event ListViewItemSelectionChangedEventHandler ItemClicked;

        public CustomDrawnListView()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.OwnerDraw = true;
            this.HideSelection = false;

            this.DrawItem += new DrawListViewItemEventHandler(listView_DrawItem);
            this.DrawColumnHeader += listView_DrawColumnHeader;
            this.DrawSubItem += ListView_DrawSubItem;
            this.MouseMove += new MouseEventHandler(listView_MouseMove);
            this.MouseLeave += new EventHandler(listView_MouseLeave);
            this.MouseClick += new MouseEventHandler(listView_MouseClick);
            this.MouseDown += new MouseEventHandler(listView_MouseDown);
            this.MouseUp += new MouseEventHandler(CustomDrawnListView_MouseUp);
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

        private string GetEllipsisString(Graphics graphics, string text, Font font, Point textStartPoint, int containerWidth)
        {
            if (graphics.MeasureString(text, font).Width + textStartPoint.X > containerWidth)
            {
                string truncatedText = text;
                int loopCounter = 0;
                do
                {
                    truncatedText = new string(truncatedText.Take(truncatedText.Count() - 1).ToArray());
                    loopCounter++;
                } while (graphics.MeasureString(truncatedText + "...", font).Width + textStartPoint.X > containerWidth && loopCounter < text.Count());

                truncatedText += "...";
                return truncatedText;
            }

            return text;
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Rectangle Bounds = e.Bounds;
            GraphicsPath RoundedBounds = GetRoundRect(Bounds.X, Bounds.Y, Bounds.Width - 4, Bounds.Height - 1, 3);
            //Bounds.Width = this.listViewProjectOptions.Width;

            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            BufferedGraphics BufGraphics = context.Allocate(e.Graphics, Bounds);

            Brush BackgroundBrush = new SolidBrush(e.Item.ListView.BackColor);
            BufGraphics.Graphics.FillRectangle(BackgroundBrush, Bounds);

            if (HotLightIndex == e.Item.ListView.Items.IndexOf(e.Item))
            {
                Pen BorderPen = Pens.DimGray;
                Color BeginGradient = Color.FromArgb(255, 195, 0);
                Color EndGradient = Color.FromArgb(255, 110, 0);

                if (IsMouseDown)
                {
                    //BorderPen = Pens.DarkGray;
                    BeginGradient = Color.FromArgb(255, 215, 0);
                    EndGradient = Color.FromArgb(255, 130, 0);
                }

                LinearGradientMode RightMode = LinearGradientMode.Vertical;

                using (LinearGradientBrush b = new LinearGradientBrush(Bounds, BeginGradient, EndGradient, RightMode))
                {
                    //BufGraphics.Graphics.FillRectangle(b, Bounds);
                    BufGraphics.Graphics.FillPath(b, RoundedBounds);
                }
                BufGraphics.Graphics.DrawPath(BorderPen, RoundedBounds);
            }
            else if(e.Item.Selected) //if((e.State & ListViewItemStates.Selected) == ListViewItemStates.Selected)
            {
                Pen BorderPen = Pens.LightGray;
                Color BeginGradient = ColorManager.TreeViewSelectedBeginGradient;// Color.FromArgb(255, 195, 0);
                Color EndGradient = ColorManager.TreeViewSelectedEndGradient;// Color.FromArgb(255, 110, 0);

                LinearGradientMode RightMode = LinearGradientMode.Vertical;

                using (LinearGradientBrush b = new LinearGradientBrush(Bounds, BeginGradient, EndGradient, RightMode))
                {
                    //BufGraphics.Graphics.FillRectangle(b, Bounds);
                    BufGraphics.Graphics.FillPath(b, RoundedBounds);
                }
                BufGraphics.Graphics.DrawPath(BorderPen, RoundedBounds);
            }

            if (e.Item.ImageIndex != -1)
            {
                Image Icon = e.Item.ListView.SmallImageList.Images[e.Item.ImageIndex];
                BufGraphics.Graphics.DrawImage(Icon, new Point(4, Bounds.Y + ((Bounds.Height - Icon.Height) / 2)));

                Font font = e.Item.Font;
                Point TextPoint = new Point(Bounds.X + 5 + Icon.Width, Bounds.Y + 1);
                string renderText = GetEllipsisString(BufGraphics.Graphics, e.Item.Text, font, TextPoint, Bounds.Width);

                BufGraphics.Graphics.DrawString(renderText, font, Brushes.Black, TextPoint);
            }
            else
            {
                Font font = e.Item.Font;
                Point TextPoint = new Point(Bounds.X + 4, Bounds.Y + 1);
                string renderText = GetEllipsisString(BufGraphics.Graphics, e.Item.Text, font, TextPoint, Bounds.Width);

                BufGraphics.Graphics.DrawString(renderText, font, Brushes.Black, TextPoint);
            }

            BufGraphics.Render(e.Graphics);
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_MouseMove(object sender, MouseEventArgs e)
        {
            bool tabFound = false;
            for (int i = 0; i < this.Items.Count; i++)
            {
                Rectangle itemRectangle = this.GetItemRect(i);
                if (itemRectangle.Contains(e.Location))
                {
                    tabFound = true;
                    if (HotLightIndex != i)
                    {
                        this.Cursor = Cursors.Hand;
                        HotLightIndex = i;
                        //DrawItemIntoBuffer(this.TabPages[m_HotLightIndex]);
                        this.Invalidate();
                    }
                }
            }

            if (!tabFound)
            {
                if (HotLightIndex != -1)
                {
                    this.Cursor = Cursors.Arrow;
                    HotLightIndex = -1;
                    this.Invalidate();
                }
            }
        }

        private void listView_MouseLeave(object sender, EventArgs e)
        {
            if (HotLightIndex != -1)
            {
                this.Cursor = Cursors.Arrow;
                HotLightIndex = -1;
                this.Invalidate();
            }

            IsMouseDown = false;
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                Rectangle itemRectangle = this.GetItemRect(i);
                if (itemRectangle.Contains(e.Location))
                {
                    //do stuff
                    if (ItemClicked != null)
                    {
                        ItemClicked(this, new ListViewItemSelectionChangedEventArgs(this.Items[i], i, true));
                    }

                    this.Items[i].Selected = true;
                }
                else
                {
                    this.Items[i].Selected = false;
                }
            }
        }

        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            this.Invalidate();
        }

        private void CustomDrawnListView_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }
    }
}
