using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Staem.Forms
{
    public partial class LibForm : Form
    {
        User user;
        List<Game> hry = StoreForm.games;

        //vytvaram kolekciu mojich vlastnych fontov
        PrivateFontCollection font = new PrivateFontCollection();

        //ziskava absolutnu cestu ku suboru
        string fontPath = Path.GetFullPath("BrunoAceSC.ttf");

        public LibForm(User user)
        {
            InitializeComponent();

            this.user = user;
            font.AddFontFile(fontPath);

            this.AutoScaleMode = AutoScaleMode.None;

            drawGames();
        }

        private void drawGames()
        {
            List<string> vlastneneHry = new List<string>();

            MySqlCommand cmd = new MySqlCommand($"SELECT hry FROM Users WHERE email='{user.Email}';", Database.connection);

            Database.dbConnect();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                vlastneneHry = reader["hry"].ToString().Split(';').ToList();
            }

            vlastneneHry.RemoveAt(vlastneneHry.Count - 1);

            Database.dbClose();


            Label nadpis = new Label
            {
                Location = new Point(310, 50),
                AutoSize = true,
                ForeColor = Color.White,
                Text = "Knižnica",
                Font = new Font(font.Families[0], 20, FontStyle.Bold)
            };

            if (vlastneneHry.Count == 0)
            {
                Label nemasHru = new Label
                {
                    Location = new Point(310, 100),
                    AutoSize = true,
                    ForeColor = Color.White,
                    Text = "Momentálne nemáš žiadnu hru",
                    Font = new Font(font.Families[0], 15, FontStyle.Italic)
                };

                Controls.Add(nemasHru);
            }

            int y = 0;

            foreach (var item in vlastneneHry)
            {
                string kategoria = null, cesta = null;


                MySqlCommand cmd2 = new MySqlCommand($"SELECT kategoria, cesta FROM Games WHERE nazov='{item}';", Database.connection);

                Database.dbConnect();
                MySqlDataReader reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    kategoria = reader2["kategoria"].ToString();
                    cesta = reader2["cesta"].ToString();
                }
                Database.dbClose();


                Panel hra = new Panel
                {
                    Location = new Point(360, 115 + y),
                    Size = new Size(840, 100),
                    BackColor = Color.FromArgb(57, 102, 132)
                };

                PictureBox obrazok = new PictureBox
                {
                    Image = Image.FromFile(cesta),
                    Location = new Point(0, 0),
                    Size = new Size(200, 100),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                Label nazov = new Label
                {
                    Location = new Point(210, 20),
                    AutoSize = true,
                    ForeColor = Color.White,
                    Text = item,
                    Font = new Font(font.Families[0], 14, FontStyle.Bold)
                };

                Label labelKategoria = new Label
                {
                    Location = new Point(210, 55),
                    AutoSize = true,
                    ForeColor = Color.LightGray, // Gray
                    Text = kategoria,
                    Font = new Font(font.Families[0], 11, FontStyle.Italic)
                };


                y += 125;

                               

                hra.Controls.Add(obrazok);
                hra.Controls.Add(nazov);
                hra.Controls.Add(labelKategoria);

                Controls.Add(hra);
            }

            Controls.Add(nadpis);

        }

    }
}
