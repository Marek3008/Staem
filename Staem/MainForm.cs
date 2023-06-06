﻿using System;
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
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using Staem.Forms;

namespace Staem
{
    public partial class MainForm : Form
    {
        private User user;

        public static StoreForm storeForm;
        LibForm libForm;
        AddFunds peniazky;
        GameForm gameForm;

        bool vLib = false, vGame = false, vStore = true, vPeniazky = false;

        // objekty pre popis a kupu hry
        /*
        Label nazovHry, popis, vyvojar;
        Panel hlavnyPanel;
        
        TransparentButton back, next;

        // objekty pre kniznicu
        
        Label kniznica, libNazov, libKategoria, libOdobrat, nemasHru, libHrat;
        PictureBox libObrazok;
        Panel libHra;

        // kontrola na ktorej stranke sme
        
        bool vKniznici = false;*/

        List<Control> libHry = new List<Control>();

        PrivateFontCollection font = new PrivateFontCollection();


        //ziskava absolutnu cestu ku suboru
        string fontPath = Path.GetFullPath("BrunoAceSC.ttf");




        //v atributoch je potiahnuty user z loginformu
        public MainForm(User user)
        {
            InitializeComponent();

            this.user = user;

            labelNick.Text = (user.checkedUserID).ToUpper();

            peniazeLoad();


            storeForm = new StoreForm(user, this);
            storeForm.TopLevel = false;
            storeForm.FormBorderStyle = FormBorderStyle.None;
            storeForm.Location = new Point(0, 50);
            storeForm.Width = this.Width - 17;
            storeForm.Height = this.Height - 100;

            Controls.Add(storeForm);

            storeForm.Show();
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


        public void peniazeLoad()
        {
            string temp = "";
            MySqlCommand cmd = new MySqlCommand($"SELECT balance FROM Users;", Database.connection);

            Database.dbConnect();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                temp = reader["balance"].ToString();
            }
            Database.dbClose();

            mainCena_label.Text = $"{temp} €";
        }


        //tato metoda zisti pocet hier v tabulke podla ID



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


        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }



        // ODPAD PROSTE
        private void labelStore_Click(object sender, EventArgs e)
        {

            if(this.Controls.Count > 7)
            {
                this.Controls.RemoveAt(Controls.Count - 1);
            }           
            

            storeForm.Show();
        }

        private void labelLib_Click(object sender, EventArgs e)
        {
            storeForm.Hide();

            libForm = new LibForm(user)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Location = new Point(0, 50),
                Width = this.Width - 17,
                Height = this.Height - 100
            };

            Controls.Add(libForm);
            
            //nechytat sa
            if (this.Controls.Count > 8)
            {
                Controls.RemoveAt(Controls.Count - 2);
            }

            libForm.Show();
        }

        private void libOdobrat_Click(string hra)
        {
            Database.dbConnect();

            //tento command odobera z databazy hry
            MySqlCommand cmd = new MySqlCommand($"UPDATE Users SET hry = REPLACE(hry, '{hra};', '') WHERE email = '{user.Email}';", Database.connection);
            cmd.ExecuteNonQuery();

            Database.dbClose();

            foreach(var item in libHry)
            {
                item.Dispose();
            }

            //tohoto sa nechytat; labelLib_Click chcel pri zavolani nejake argumetny typu vid nizsie tak som ich tam dal
            labelLib_Click(new object(), new EventArgs());
        }

        private void mainCena_label_Click(object sender, EventArgs e)
        {
            storeForm.Hide();

            if(this.Controls.Count > 7)
            {
                this.Controls.RemoveAt(Controls.Count - 1);
            } 

            peniazky = new AddFunds();
            peniazky.TopLevel = false;
            peniazky.FormBorderStyle = FormBorderStyle.None;
            peniazky.Location = new Point(0, 50);
            peniazky.Width = this.Width - 17;
            peniazky.Height = this.Height - 100;

            Controls.Add(peniazky);

            peniazky.Show();

            
        }

        private void peniazeTick_Tick(object sender, EventArgs e)
        {
            peniazeLoad();
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
