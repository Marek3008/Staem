using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;

namespace Staem
{
    public partial class MainForm : Form
    {
        private User user;

        public MainForm(User user)
        {
            InitializeComponent();
            this.user = user;            
            
            labelNick.Text = (user.checkedUserID).ToUpper();            
        }

        private void labelPanel_hover(object sender, EventArgs e)
        {
            Label hoveredLabel = (Label)sender;
            hoveredLabel.ForeColor = Color.Aqua;
        }

        private void labelPanel_unhover(object sender, EventArgs e)
        {
            Label hoveredLabel = (Label)sender;
            hoveredLabel.ForeColor = Color.White;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            
            //toto meni font
            PrivateFontCollection font = new PrivateFontCollection();

            string fontPath = Path.GetFullPath("BrunoAceSC.ttf");

            font.AddFontFile(fontPath);

            foreach (Control c in this.Controls)
            {
                c.Font = new Font(font.Families[0], 16, FontStyle.Regular);
            }
        }
    }
}
