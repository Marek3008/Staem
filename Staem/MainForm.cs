using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Staem
{
    public partial class MainForm : Form
    {
        private User user;

        public MainForm(User user)
        {
            InitializeComponent();
            this.user = user;
            labelLib.MouseHover += new EventHandler(labelPanel_hover);
            labelLib.MouseLeave += new EventHandler(labelPanel_unhover);
            labelStore.MouseHover += new EventHandler (labelPanel_hover);
            labelStore.MouseLeave += new EventHandler (labelPanel_unhover);
            labelNick.Text = user.checkedUserID;
            labelNick.MouseHover += new EventHandler(labelPanel_hover);
            labelNick.MouseLeave += new EventHandler(labelPanel_unhover);
        }

        private void labelPanel_hover(object sender, EventArgs e)
        {
            Label hoveredLabel = (Label)sender;
            hoveredLabel.ForeColor = Color.Aqua;
        }

        private void labelPanel_unhover(object sender, EventArgs e)
        {
            Label hoveredLabel = (Label)sender;
            hoveredLabel.ForeColor = Color.MidnightBlue;
        }


    }
}
