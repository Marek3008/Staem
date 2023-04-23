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

namespace Staem
{
    public partial class MainForm : Form
    {
        private User user;
        List<Game> games = new List<Game>();
        private int amount;


        public MainForm(User user)
        {
            InitializeComponent();
            this.user = user;            
            
            labelNick.Text = (user.checkedUserID).ToUpper();            
        }

        public void getAmount()
        {
            Database.dbConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT ID FROM Games ORDER BY ID ASC", Database.connection);

            cmd.CommandType = System.Data.CommandType.Text;

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                amount = Convert.ToInt32(reader["ID"]);
            }
            Database.dbClose();
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

        public void getGame()
        {
            
            for (int i = 1; i <= amount; i++)
            {
                Database.dbConnect();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Games WHERE ID = {i}", Database.connection);
                cmd.CommandType = CommandType.Text;

                MySqlDataReader reader = cmd.ExecuteReader();
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
            int a = 0;

            foreach (var item in games)
            {
                string path = Path.GetFullPath(item.Path);

                PictureBox picbox = new PictureBox();
                picbox.Image = Image.FromFile(path);
                picbox.Location = new Point(52 + a, 85);
                picbox.Size = new Size(200, 200);
                picbox.SizeMode = PictureBoxSizeMode.StretchImage;
                a += 300;
            }
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

            getAmount();
            getGame();
            drawGames();

            labelpokus.Text = games[0].Path;
        }

        
    }
}
