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
        Label nemasHru;

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

            MySqlCommand cmd = new MySqlCommand($"SELECT hry FROM users WHERE email='{user.Email}';", Database.connection);

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
                nemasHru = new Label
                {
                    Location = new Point(310, 100),
                    AutoSize = true,
                    ForeColor = Color.White,
                    Text = "Momentálne nemáš žiadnu hru",
                    Font = new Font(font.Families[0], 15, FontStyle.Italic)
                };

                Controls.Add(nemasHru);
            }


            Controls.Add(nadpis);

            int y = 0;

            foreach (var item in vlastneneHry)
            {
                string kategoria = null, cesta = null;


                MySqlCommand cmd2 = new MySqlCommand($"SELECT kategoria, cesta FROM games WHERE nazov='{item}';", Database.connection);

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
                    Font = new Font(font.Families[0], 14, FontStyle.Bold),
                    Name = "nazov"
                };

                Label labelKategoria = new Label
                {
                    Location = new Point(210, 55),
                    AutoSize = true,
                    ForeColor = Color.LightGray, // Gray
                    Text = kategoria,
                    Font = new Font(font.Families[0], 11, FontStyle.Italic)
                };
                Label odobrat = new Label
                {
                    Location = new Point(735, 20),
                    AutoSize = true,
                    ForeColor = Color.White,
                    Text = "Odobrať",
                    Font = new Font(font.Families[0], 11, FontStyle.Regular),
                    Cursor = Cursors.Hand
                };


                y += 125;


                hra.Controls.Add(odobrat);
                hra.Controls.Add(obrazok);
                hra.Controls.Add(nazov);
                hra.Controls.Add(labelKategoria);

                odobrat.Click += (sender3, e) => libOdobrat_Click(item);

                Controls.Add(hra);
            }

            

        }

        public void libOdobrat_Click(string hra)
        {
            string cena = "", balance = "";
            float temp, temp1;

            Database.dbConnect();

            //tento command odobera z databazy hry
            MySqlCommand cmd = new MySqlCommand($"UPDATE users SET hry = REPLACE(hry, '{hra};', '') WHERE email = '{user.Email}';", Database.connection);
            cmd.ExecuteNonQuery();

            Database.dbClose();

            Database.dbConnect();
            MySqlCommand cmd2 = new MySqlCommand($"SELECT cena FROM games WHERE nazov='{hra}';", Database.connection);
            MySqlDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                cena = reader2["cena"].ToString();
            }
            Database.dbClose();

            Database.dbConnect();
            MySqlCommand cmdn = new MySqlCommand($"SELECT balance FROM users WHERE email = '{user.Email}';", Database.connection);
            MySqlDataReader readern = cmdn.ExecuteReader();

            while (readern.Read())
            {
                balance = readern["balance"].ToString();
            }
            Database.dbClose();

            temp1 = float.Parse(balance);

            if (cena != "Zadarmo")
            {
                temp = float.Parse(cena);

                temp1 += temp;

                cena = temp1.ToString("#.##");

                Database.dbConnect();

                MySqlCommand cmd3 = new MySqlCommand($"UPDATE users SET balance = {cena}  WHERE email = '{user.Email}';", Database.connection);
                cmd3.ExecuteNonQuery();

                Database.dbClose();
            }

            /*
            this.Controls.Clear();
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            drawGames();*/
            
            
            foreach (Panel item in this.Controls.OfType<Panel>())
            {
                if (item.Controls["nazov"].Text == hra)
                {
                    foreach (Control c in item.Controls)
                    {
                        c.Dispose();
                    }

                    int index = Controls.IndexOf(item);

                    item.Dispose();

                    for (int i = index; i < Controls.Count; i++)
                    {
                        int y = Controls[i].Top;

                        Controls[i].Location = new Point(360, y - 125);
                    }

                }
            }

            if(Controls.Count == 1)
            {
                nemasHru = new Label
                {
                    Location = new Point(310, 100),
                    AutoSize = true,
                    ForeColor = Color.White,
                    Text = "Momentálne nemáš žiadnu hru",
                    Font = new Font(font.Families[0], 15, FontStyle.Italic)
                };

                Controls.Add(nemasHru);
            }

        }

    }
}
