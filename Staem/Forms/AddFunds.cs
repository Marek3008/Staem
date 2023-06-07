using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Staem.Forms
{
    public partial class AddFunds : Form
    {
        float[] eura = { 5, 10, 25, 50, 100 };
        int y = 0;

        User user;

        PrivateFontCollection font = new PrivateFontCollection();
        //ziskava absolutnu cestu ku suboru
        string fontPath = Path.GetFullPath("BrunoAceSC.ttf");

        public AddFunds(User user)
        {
            InitializeComponent();
            font.AddFontFile(fontPath);

            this.user = user;

            Label addFunds = new Label
            {
                Location = new Point(310, 100),
                AutoSize = true,
                ForeColor = Color.White,
                Text = "Pridať peniaze",
                Font = new Font(font.Families[0], 20, FontStyle.Bold)
            };
            Controls.Add(addFunds);

            foreach (float i in eura)
            {
                Panel peniazePanel = new Panel
                {
                    Location = new Point(710, 150 + y),
                    Size = new Size(500, 85),
                    BackColor = Color.FromArgb(57, 102, 132)
                };

                Label peniazeLabel = new Label
                {
                    Location = new Point(20, 29),
                    AutoSize = true,
                    ForeColor = Color.White,
                    Text = $"Pridaj {i},-- €",
                    Font = new Font(font.Families[0], 14, FontStyle.Bold)
                };

                Label pridajPeniaze = new Label
                {
                    Location = new Point(390, 19),
                    AutoSize = true,
                    Padding = new Padding(10, 10, 10, 10),
                    ForeColor = Color.Black,
                    BackColor = Color.Aqua,
                    Text = "Pridať",
                    Font = new Font(font.Families[0], 12, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };

                pridajPeniaze.Click += (_, clickEvent) => pridat_Click(i);

                peniazePanel.Controls.Add(peniazeLabel);
                peniazePanel.Controls.Add(pridajPeniaze);
                Controls.Add(peniazePanel);

                y += 100;
            }
        }

        private void pridat_Click(float i)
        {
            float num;
            string temp = "";

            MySqlCommand cmd = new MySqlCommand($"SELECT balance FROM Users WHERE email='{user.Email}';", Database.connection);

            Database.dbConnect();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                temp = reader["balance"].ToString();
            }
            Database.dbClose();

            num = float.Parse(temp);
            num += i;
            //temp = num.ToString();


            Database.dbConnect();
            MySqlCommand cmd2 = new MySqlCommand($"UPDATE Users SET balance = {num} WHERE email='{user.Email}'", Database.connection);
            cmd2.ExecuteNonQuery();

            Database.dbClose();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AddFunds
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "AddFunds";
            this.ResumeLayout(false);

        }
    }
}
