using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Staem.Forms
{
    public partial class GameForm : Form
    {
        Game game;
        User user;

        public GameForm(Game game, User user)
        {
            InitializeComponent();

            this.game = game;   
            this.user = user;

            this.AutoScaleMode = AutoScaleMode.None;

            Label ahoj = new Label
            {
                Location = new Point(100, 120),
                AutoSize = true,
                ForeColor = Color.White,
                Text = game.Developer,
                Font = new Font("Arial", 15, FontStyle.Italic)
            };

            Controls.Add(ahoj);
        }
    }
}
