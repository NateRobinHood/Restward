using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restward
{
    public class ColorManager
    {
        public static Color DataGridSelectedColor = Color.Orange;
        public static Color DataGridAlternatingRowColor = Color.FromArgb(255, 229, 173);

        public static Color TabBeginGradient = Color.FromArgb(195, 195, 195);
        public static Color TabEndGradient = Color.FromArgb(110, 110, 110);

        public static Color TabSelectedBeginGradient = Color.FromArgb(255, 195, 0);
        public static Color TabSelectedEndGradient = Color.FromArgb(255, 110, 0);

        public static Color TabHoverBeginGradient = Color.FromArgb(215, 215, 215);
        public static Color TabHoverEndGradient = Color.FromArgb(130, 130, 130);

        public static Color TabHoverSelectedBeginGradient = Color.FromArgb(255, 215, 0);
        public static Color TabHoverSelectedEndGradient = Color.FromArgb(255, 130, 0);

        public static Color WorkspaceBackground = Color.FromArgb(229, 229, 229);

        public static Color TreeViewSelectedBeginGradient = Color.FromArgb(240, 240, 240);
        public static Color TreeViewSelectedEndGradient = Color.FromArgb(219, 219, 219);

        public static Color TreeViewFocusedBeginGradient = Color.FromArgb(255, 195, 0);
        public static Color TreeViewFocusedEndGradient = Color.FromArgb(255, 110, 0);
    }
}
