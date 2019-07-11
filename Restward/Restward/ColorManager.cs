//******************************************************************************
//
//  SOURCE FILE:   ColorManager.cs
//
//  DESCRIPTION:   <PLEASE TYPE FILE DESCRPTION>.
//
//  TARGET FILES:  Restward.exe
//
//  Copyright (c) 2015 by Intelligrated.
//  The information contained herein is the confidential and proprietary
//  information of Intelligrated.  This information is protected, among
//  others, by the patent, copyright, trademark, and trade secret laws of the
//  United States and its several states.  Any use, copying, or reverse
//  engineering is strictly prohibited.  This software has been developed at
//  private expense and accordingly, if used under Government contract, the use,
//  reproduction or disclosure of this information is subject to the
//  restrictions set forth under the contract between Intelligrated and its
//  customer.  By viewing or receiving this information, you consent to the
//  foregoing.
//
//******************************************************************************
//
//  $LastChangedDate: 2015-06-08 15:32:23 -0400 (Mon, 08 Jun 2015) $
//
//  $LastChangedRevision: 92943 $
//
//  $LastChangedBy: intellig\nathan.blessing $
//
//  $HeadURL: https://ilinkserv.intelligrated.com/svn/Intelligrated/!Private/Trunk/Tools/MC4Configuration-Sprint1-(Rev93283)/Source/Restward/Projects/Restward/ColorManager.cs $
//
//******************************************************************************

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
    }
}
