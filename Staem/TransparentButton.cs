using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Staem
{
    public class TransparentButton : Button
    {
        public TransparentButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Popup;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
        }
    }
}
