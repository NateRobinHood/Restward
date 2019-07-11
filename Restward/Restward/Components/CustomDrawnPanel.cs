//******************************************************************************
//
//  SOURCE FILE:   CustomDrawnPanel.cs
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
//  $LastChangedDate: 2015-05-15 13:07:09 -0400 (Fri, 15 May 2015) $
//
//  $LastChangedRevision: 92072 $
//
//  $LastChangedBy: intellig\nathan.blessing $
//
//  $HeadURL: https://ilinkserv.intelligrated.com/svn/Intelligrated/!Private/Trunk/Tools/MC4Configuration-Sprint1-(Rev93283)/Source/Restward/Projects/Restward/CustomDrawnPanel.cs $
//
//******************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restward
{
    public partial class CustomDrawnPanel : Panel
    {
        public CustomDrawnPanel()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
