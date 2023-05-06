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
using MySqlConnector;
using System.Data.SqlClient;
using System.Windows;

namespace Staem
{
    public partial class MainForm : Form
    {
        private User user;
        List<Game> games = new List<Game>();
        private int amount;
        string hry;
        string[] poleHier;

        // objekty pre popis a kupu hry
        Button kupit;
        Label nazovHry, popis;
        Panel hlavnyPanel;

        // objekty pre kniznicu
        List<Panel> libHry = new List<Panel> ();
        Label kniznica, libNazov, libKategoria;
        PictureBox libObrazok;

        // kontrola na ktorej stranke sme
        bool vHre = false;
        bool vKniznici = false;

        //vytvaram kolekciu mojich vlastnych fontov
        PrivateFontCollection font = new PrivateFontCollection();

        //ziskava absolutnu cestu ku suboru
        string fontPath = Path.GetFullPath("BrunoAceSC.ttf");


        //v atributoch je potiahnuty user z loginformu
        public MainForm(User user)
        {
            InitializeComponent();
            this.user = user;            
            
            labelNick.Text = (user.checkedUserID).ToUpper();            
        }




        //"""""""""""""""""""""""""""""""""""""""""""""""""              METODY         """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""             ZACIATOK         """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""              METODY         """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""



        //tato metoda zisti pocet hier v tabulke podla ID
        public void getAmount()
        {
            Database.dbConnect();

            //tento command zoradi vsetky hry podla IDcka od najemnsieho po najvacsie
            MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(*) FROM Games", Database.connection);

            cmd.CommandType = System.Data.CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();

            //tento cyklus prejde celu novo vyselectovanu tabulku a nesutale prepisuje premennu amount az kym nepride na poslednu hru ktorej IDcko je najvacsie a to zapise do amount na furt
            //tym padom ziskame pocet hier v tabulke -> pocet vsetkych hier
            while (reader.Read())
            {
                amount = Convert.ToInt32(reader["COUNT(*)"]);
            }

            Database.dbClose();
        }

        

        public void getGame()
        {
            //tento cyklus sa opakuje tolko krat kolko je pocet hier v tabulke
            for (int i = 1; i <= amount; i++)
            {
                Database.dbConnect();

                //tento command vyselectuje zakazdym iba jednu hru ktorej IDcko sa zhoduje s poradim iteracie
                //tymto sposobom postupne vyselectujeme kazdu hru zvlast
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Games WHERE ID = {i}", Database.connection);

                cmd.CommandType = CommandType.Text;

                MySqlDataReader reader = cmd.ExecuteReader();

                //tento cyklus robi to ze z vybraneho riadku v tabulke (vzdy len jeden naraz) vyberie vsetky potrebne informacie o hre a zapise ich do noveho objektu ->
                //ktory sa prida do listu s hrami => takze na konci for-cyklu je list plny hier s jedinecnymi vlastnostami
                while (reader.Read())
                {
                    games.Add(new Game(reader["nazov"].ToString(),
                        reader["kategoria"].ToString(),
                        reader["popis"].ToString(),
                        reader["cena"].ToString(),
                        reader["developer"].ToString(),
                        reader["cesta"].ToString()
                        ));
                }

                Database.dbClose();
            }
        }

        public void drawGames()
        {
            int x = 0, y = 0;
            int counter = 0;

            //cyklus bude prebiehat tolko krat kolko je hier v liste; hram pridelujeme docasny univerzalny nazov item ktory existuje iba v cykle
            foreach (var item in games)
            {
                //do premennej typu string sa zapise absolutna cesta obrazku; napr.: Path = "csgo.png" => v PC sa najde tento subor a zisti sa jeho absolutna cesta
                //tento sposob je trosku komplikovanejsi ale aplikacia nechcela spracovat relativnu cestu obrazka, preto sa v hocijakom zariadeni zisti nova absolutna cesta
                string path = Path.GetFullPath(item.Path);

                //pocas kazdej iteracie cyklu vytvarame novy picturebox (nacitavame obrazok, nastavujeme poziciu a velkost a to ako sa ma obrazok v pictureboxe spravat)
                //x-ova suradnica sa zvacsuje o nejaku hodnotu aby boli pictureboxy pekne od seba oddelene -> vdaka premennej a ktora sa po kazdej iteracii zvacsi o 300
                PictureBox picbox = new PictureBox
                {
                    Image = Image.FromFile(path),
                    Location = new Point(310 + x, 85 + y),
                    Size = new Size(400, 200),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Cursor = Cursors.Hand
                };

                Panel panelCena = new Panel
                {
                    Location = new Point(310 + x, 285 + y),
                    Size = new Size(400, 35),
                    BackColor = Color.FromArgb(57, 102, 132),
                    Cursor = Cursors.Hand
                };

                Label cena = new Label
                {
                    Location = new Point(320 + x, 292 + y),
                    AutoSize = true,
                    Padding = new Padding(2),
                    BackColor = Color.FromArgb(38, 62, 85),
                    ForeColor = Color.White,
                    Text = item.Price,
                    Font = new Font(font.Families[0], 10, FontStyle.Regular), // moze byt aj FontStyle.Bold
                    Cursor = Cursors.Hand
                };

                
                //vdaka tomuto divokemu zapisu mozem do onclick metody passnut argument
                picbox.Click += (sender, e) => picbox_Click(item);
                panelCena.Click += (sender, e) => picbox_Click(item);
                cena.Click += (sender, e) => picbox_Click(item);

                x += 450;

                counter++;

                if((counter % 3) == 0)
                {
                    x = 0; 
                    y += 278;
                }

                //VELMI DOLEZITE!!!; toto robi to ze dane "ovladanie" (label, textbox, ...) mozeme naozaj aj vidiet; bez tohoto by objekt picbox zaberal iba miesto v pamati
                this.Controls.Add(picbox);
                panelCena.Controls.Add(cena);
                this.Controls.Add(cena);
                this.Controls.Add(panelCena);
            }
        }
        //"""""""""""""""""""""""""""""""""""""""""""""""""              METODY         """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""               KONIEC         """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""              METODY         """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""






        //"""""""""""""""""""""""""""""""""""""""""""""""""         EVENT HANDLERY         """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""             ZACIATOK         """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""         EVENT HANDLERY         """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        private void MainForm_Load(object sender, EventArgs e)
        {
            //toto robi maximalizovane okno 
            this.WindowState = FormWindowState.Maximized;
            this.Width = Screen.PrimaryScreen.Bounds.Width;

            //pridavam font do mojej kolekcie
            font.AddFontFile(fontPath);

            //k fontu pristupujem tak ze: nazovkolekcie.Families[poradie daneho fontu]
            foreach (Control c in this.Controls)
            {
                c.Font = new Font(font.Families[0], 16, FontStyle.Regular);
            }

            labelLib.Font = new Font(font.Families[0], 16, FontStyle.Bold);
            labelStore.Font = new Font(font.Families[0], 16, FontStyle.Bold);
            labelNick.Font = new Font(font.Families[0], 16, FontStyle.Bold);

            //toto ziska hry pouzivatela
            Database.dbConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT hry FROM Users WHERE email = '{user.Email}'", Database.connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                hry = reader["hry"].ToString();
            }
            Database.dbClose();


            getAmount();
            getGame();
            drawGames();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void picbox_Click(Game game)
        {
            vHre = true;

            foreach (Control c in this.Controls)
            {
                c.Visible = false;
                labelLib.Visible = true;
                labelNick.Visible = true;
                labelStore.Visible = true;
                panel1.Visible = true;
            }
                        
            //tieto veci pridavaju nove controls
            hlavnyPanel = new Panel
            {
                Location = new Point((Screen.PrimaryScreen.Bounds.Width / 2) - (840 / 2), 150),
                AutoSize = true,
                MaximumSize = new Size(840, 0),
                BackColor = Color.FromArgb(103, 103, 178),
                Cursor = Cursors.Default
            };

            nazovHry = new Label
            {
                Location = new Point(100, 100),
                AutoSize = true,
                ForeColor = Color.White,
                Text = game.Name,
                Font = new Font(font.Families[0], 20, FontStyle.Bold)
            };

            popis = new Label
            {
                Location = new Point(20, 20),                
                AutoSize = true,
                MaximumSize = new Size(800, 0),
                ForeColor = Color.White,
                Text = game.Description,
                Font = new Font(font.Families[0], 13, FontStyle.Regular)
            };

            kupit = new Button
            {
                AutoSize = true,
                //Location = new Point((1920 / 2) - (100 / 2), popis.Height + 300 + 50),
                Location = new Point(200, 200),
                Text = game.Price,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Font = new Font(font.Families[0], 13, FontStyle.Regular),
                BackColor = Color.Black,
            };

            //divoky zapis na passnutie argumentu do event handleru
            kupit.Click += (sender, e) => kupit_Click(game);

            // nechytat sa toho, lebo takto to funguje
            Controls.Add(nazovHry);

            hlavnyPanel.Controls.Add(popis);
            hlavnyPanel.Controls.Add(kupit);

            Controls.Add(kupit);
            Controls.Add(hlavnyPanel);

            //toto kontroluje ci je hra zakupena (mohlo to byt aj vo funkcii ale vzhladom na error ktory neviem pochopit to tam byt nemoze)
            poleHier = hry.Split(';');

            if (poleHier != null)
            {
                foreach (string item in poleHier)
                {
                    if (item == game.Name)
                    {
                        kupit.Enabled = false;
                    }
                }
            }
                
        }

        private void kupit_Click(Game game)
        {
            //vdaka tomuto pridavam do databazy hry
            hry += $"{game.Name};";

            Database.dbConnect();
            MySqlCommand cmd1 = new MySqlCommand($"UPDATE Users SET hry = '{hry}' WHERE email = '{user.Email}';", Database.connection);
            cmd1.ExecuteNonQuery();
            Database.dbClose();


            //toto kontroluje ci je hra zakupena (mohlo to byt aj vo funkcii ale vzhladom na error ktory neviem pochopit to tam byt nemoze)
            poleHier = hry.Split(';');

            if (poleHier != null)
            {
                foreach (string item in poleHier)
                {
                    if (item == game.Name)
                    {
                        kupit.Enabled = false;
                    }
                }
            }

        }

        private void labelStore_Click(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if(control is PictureBox || control is Panel || control is Label)
                {
                    control.Visible = true;
                }
            }

            /*
            hlavnyPanel.Dispose();
            nazovHry.Dispose();
            kupit.Dispose();
            */

            foreach (var hra in libHry)
            {
                hra.Visible = false;
            }
        }

        private void labelLib_Click(object sender, EventArgs e)
        {
            vKniznici = true;
            libHry.Clear();

            foreach (Control c in this.Controls)
            {
                c.Visible = false;
                labelLib.Visible = true;
                labelNick.Visible = true;
                labelStore.Visible = true;
                panel1.Visible = true;
            }

            kniznica = new Label
            {
                Location = new Point(310, 100),
                AutoSize = true,
                ForeColor = Color.White,
                Text = "Knižnica",
                Font = new Font(font.Families[0], 20, FontStyle.Bold)
            };

            poleHier = hry.Split(';');
            // vymaze posledny index v poli
            poleHier = poleHier.Take(poleHier.Length - 1).ToArray();
            string kategoria = "";
            string cesta = "";
            int y = 0;
            int counter = 0;

            foreach (string item in poleHier)
            {
                Database.dbConnect();
                MySqlCommand cmd = new MySqlCommand($"SELECT kategoria, cesta FROM Games WHERE nazov = '{item}'", Database.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kategoria = reader["kategoria"].ToString();
                    cesta = reader["cesta"].ToString();
                }
                Database.dbClose();
                /*
                Panel libPanel = new Panel
                {
                    Location = new Point(310, 200 + y),
                    Size = new Size(840, 100),
                    BackColor = Color.FromArgb(57, 102, 132)
                };
                */

                libHry.Add(new Panel
                {
                    Location = new Point(310, 200 + y),
                    Size = new Size(840, 100),
                    BackColor = Color.FromArgb(57, 102, 132)
                });

                libObrazok = new PictureBox
                {
                    Image = Image.FromFile(cesta),
                    Location = new Point(0, 0),
                    Size = new Size(200, 100),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                libNazov = new Label
                {
                    Location = new Point(210, 20),
                    AutoSize = true,
                    ForeColor = Color.White,
                    Text = item,
                    Font = new Font(font.Families[0], 14, FontStyle.Bold)
                };

                libKategoria = new Label
                {
                    Location = new Point(210, 55),
                    AutoSize = true,
                    ForeColor = Color.LightGray, // Gray
                    Text = kategoria,
                    Font = new Font(font.Families[0], 11, FontStyle.Italic)
                };

                y += 110;

                Controls.Add(kniznica);
                libHry[counter].Controls.Add(libObrazok);
                libHry[counter].Controls.Add(libNazov);
                libHry[counter].Controls.Add(libKategoria);
                
                Controls.Add(libHry[counter]);

                counter++;
            }
        }

        private void labelPanel_hover(object sender, EventArgs e)
        {
            Label hoveredLabel = (Label)sender;
            hoveredLabel.ForeColor = Color.Aqua;
            hoveredLabel.Font = new Font(font.Families[0], 16, FontStyle.Bold);
        }

        private void labelPanel_unhover(object sender, EventArgs e)
        {
            Label hoveredLabel = (Label)sender;
            hoveredLabel.ForeColor = Color.White;
            hoveredLabel.Font = new Font(font.Families[0], 16, FontStyle.Bold);
        }
        //"""""""""""""""""""""""""""""""""""""""""""""""""         EVENT HANDLERY         """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""             KONIEC                  """""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""         EVENT HANDLERY         """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
    }
}
