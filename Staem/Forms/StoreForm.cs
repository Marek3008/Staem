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
    public partial class StoreForm : Form
    {
        User user;
        MainForm mainForm;

        private int amount;
        public static List<Game> games = new List<Game>();
        List<string> category = new List<string>();
        // kategoria
        string kliknutaKategoria = "";
        public List<Label> klikCategory = new List<Label>();


        //vytvaram kolekciu mojich vlastnych fontov
        PrivateFontCollection font = new PrivateFontCollection();

        //ziskava absolutnu cestu ku suboru
        string fontPath = Path.GetFullPath("BrunoAceSC.ttf");

        public StoreForm(User user, MainForm mainForm)
        {
            InitializeComponent();

            this.AutoScroll = true;
            this.AutoScaleMode = AutoScaleMode.None;

            font.AddFontFile(fontPath);

            foreach (Control c in this.Controls)
            {
                c.Font = new Font(font.Families[0], 16, FontStyle.Regular);
            }

            this.user = user;
            this.mainForm = mainForm;

            getAmount();
            getGame();
            drawGames();

            getCategory();
            category.Sort();
            drawCategory();
        }

        public void getAmount()
        {
            Database.dbConnect();

            //tento command zoradi vsetky hry podla IDcka od najemnsieho po najvacsie
            MySqlCommand cmd = new MySqlCommand($"SELECT COUNT(*) FROM games", Database.connection);

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
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM games WHERE ID = {i}", Database.connection);

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
                    Image = System.Drawing.Image.FromFile(path),
                    Location = new Point(110 + x, 85 + y),
                    Size = new Size(400, 200),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Cursor = Cursors.Hand
                };

                Panel panelCena = new Panel
                {
                    Location = new Point(110 + x, 285 + y),
                    Size = new Size(400, 35),
                    BackColor = Color.FromArgb(57, 102, 132),
                    Cursor = Cursors.Hand
                };

                Label cena = new Label
                {
                    Location = new Point(120 + x, 292 + y),
                    AutoSize = true,
                    Padding = new Padding(2),
                    BackColor = Color.FromArgb(38, 62, 85),
                    ForeColor = Color.White,
                    Font = new Font(font.Families[0], 10, FontStyle.Regular), // moze byt aj FontStyle.Bold
                    Cursor = Cursors.Hand
                };

                if(item.Price == "Zadarmo")
                {
                    cena.Text = "Zadarmo";
                }
                else
                {
                    cena.Text = item.Price + " €";
                }


                //vdaka tomuto divokemu zapisu mozem do onclick metody passnut argument
                picbox.Click += (sender, e) => picbox_Click(item);
                panelCena.Click += (sender, e) => picbox_Click(item);
                cena.Click += (sender, e) => picbox_Click(item);

                x += 450;

                counter++;

                if ((counter % 3) == 0)
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

        public void getCategory()
        {
            string[] temp;

            foreach (var item in games)
            {
                if (item.Category.Contains(","))
                {
                    temp = item.Category.Split(',');

                    foreach (var item2 in temp)
                    {
                        if (category.Contains(item2))
                        {
                            continue;
                        }

                        category.Add(item2);
                    }

                    temp = null;
                }
                else
                {
                    if (category.Contains(item.Category))
                    {
                        continue;
                    }

                    category.Add(item.Category);
                }
            }
        }

        public void drawCategory()
        {
            int y = 0;

            // vypise ponuku vyberu hier podla kategorii
            Label nazovKategorie = new Label
            {
                Location = new Point(1510, 85),
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Text = "Kategórie",
                Font = new Font(font.Families[0], 18, FontStyle.Bold)
            };
            Controls.Add(nazovKategorie);

            foreach (var categ in category)
            {
                Label vyberKategoria = new Label
                {
                    Location = new Point(1520, 125 + y),
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    Text = categ,
                    Font = new Font(font.Families[0], 13, FontStyle.Underline),
                    Cursor = Cursors.Hand
                };

                if (vyberKategoria.Text == kliknutaKategoria)
                {
                    vyberKategoria.Font = new Font(font.Families[0], 17, FontStyle.Bold);
                    vyberKategoria.ForeColor = Color.Aqua;
                    klikCategory.Add(vyberKategoria);
                    kliknutaKategoria = "";
                }

                vyberKategoria.Click += (sender, e) => vyberKategorie_Click(categ);

                y += 40;

                Controls.Add(vyberKategoria);
            }
        }

        private void picbox_Click(Game game)
        {
            this.Visible = false;

            GameForm gameForm = new GameForm(game, user);

            gameForm.TopLevel = false;
            gameForm.FormBorderStyle = FormBorderStyle.None;
            gameForm.Location = new Point(0, 50);
            gameForm.Width = mainForm.Width - 17;
            gameForm.Height = mainForm.Height - 100;

            mainForm.Controls.Add(gameForm);
            gameForm.Show();


        }

        public void vyberKategorie_Click(string c)
        {
            kliknutaKategoria = c;
            int x = 0, y = 0;
            int counter = 0;

            foreach (Control co in this.Controls)
            {
                if (co is Panel || co is Label || co is PictureBox)
                {
                    co.Visible = false;
                }
            }

            // z neznameho dovodu sa pri tejto funkcii vytvara label, ktory by sa nemal (netusim preco)
            //kniznica.Dispose();

            drawCategory();

            foreach (var item in games)
            {
                if (item.Category.Contains(c))
                {
                    //do premennej typu string sa zapise absolutna cesta obrazku; napr.: Path = "csgo.png" => v PC sa najde tento subor a zisti sa jeho absolutna cesta
                    //tento sposob je trosku komplikovanejsi ale aplikacia nechcela spracovat relativnu cestu obrazka, preto sa v hocijakom zariadeni zisti nova absolutna cesta
                    string path = Path.GetFullPath(item.Path);

                    //pocas kazdej iteracie cyklu vytvarame novy picturebox (nacitavame obrazok, nastavujeme poziciu a velkost a to ako sa ma obrazok v pictureboxe spravat)
                    //x-ova suradnica sa zvacsuje o nejaku hodnotu aby boli pictureboxy pekne od seba oddelene -> vdaka premennej a ktora sa po kazdej iteracii zvacsi o 300
                    PictureBox picbox = new PictureBox
                    {
                        Image = Image.FromFile(path),
                        Location = new Point(110 + x, 85 + y),
                        Size = new Size(400, 200),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Cursor = Cursors.Hand
                    };

                    Panel panelCena = new Panel
                    {
                        Location = new Point(110 + x, 285 + y),
                        Size = new Size(400, 35),
                        BackColor = Color.FromArgb(57, 102, 132),
                        Cursor = Cursors.Hand
                    };

                    Label cena = new Label
                    {
                        Location = new Point(120 + x, 292 + y),
                        AutoSize = true,
                        Padding = new Padding(2),
                        BackColor = Color.FromArgb(38, 62, 85),
                        ForeColor = Color.White,
                        Font = new Font(font.Families[0], 10, FontStyle.Regular), // moze byt aj FontStyle.Bold
                        Cursor = Cursors.Hand
                    };

                    if (item.Price == "Zadarmo")
                    {
                        cena.Text = "Zadarmo";
                    }
                    else
                    {
                        cena.Text = item.Price + " €";
                    }


                    //vdaka tomuto divokemu zapisu mozem do onclick metody passnut argument
                    picbox.Click += (sender, e) => picbox_Click(item);
                    panelCena.Click += (sender, e) => picbox_Click(item);
                    cena.Click += (sender, e) => picbox_Click(item);
                    x += 450;
                    counter++;

                    if ((counter % 3) == 0)
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

            kliknutaKategoria = "";
        }

        

        
    }
}
