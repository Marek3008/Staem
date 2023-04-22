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

    public partial class LoginForm : Form
    {
        User user;
        bool loginMode = false;
        bool prihlaseny = false;

        public LoginForm()
        {
            InitializeComponent();

            //"vypne" label8 a.k.a chcem sa registrovat
            label8.ForeColor = Color.FromArgb(37, 66, 156);
            label8.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
            //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
            //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
            //registracia
            if (!loginMode) 
            {
                //kontroluje ci su vsetky policka vyplnene
                if (string.IsNullOrEmpty(userbox.Text) || string.IsNullOrEmpty(emailbox.Text) || string.IsNullOrEmpty(passbox.Text) || string.IsNullOrEmpty(repeatpassbox.Text))
                {
                    label6.Text = "Vyplň všetky políčka!";
                }
                else
                {
                    Database.dbConnect();

                    //vytvara noveho pouzivatela
                    user = new User(userbox.Text, emailbox.Text, passbox.Text);

                    //skontroluje tabulku v databaze ci sa nesnazime vytvorit pouzivatela s rovnakym UserID alebo emailom
                    //resp. sa zapisu hodnoty do premennych v User.cs ktore sa pouzivaju nizsie
                    user.checkUser();

                    Database.dbClose();

                    //ak sa snazime o vytvorenie pouzivatela s userID alebo emailom ktore uz existuje
                    if (user.checkedEmail == user.Email || user.checkedUserID == user.UserID)
                    {
                        label6.Text = "tento účet už existuje!";

                        //vymaze udaje o pouzivatelovi aby sa objekt mohol pouzit nanovo
                        user = null;
                    }
                    else
                    {
                        //kontroluje ci sa nam zhoduju hesla
                        if (passbox.Text == repeatpassbox.Text)
                        {
                            Database.dbConnect();

                            //prida pouzivatela do databazy
                            user.addUser();

                            //vymaze udaje o pouzivatelovi aby sa objekt mohol pouzit nanovo
                            user = null;

                            label6.ForeColor = Color.Green;
                            label6.Text = "Úspešne si sa zaregistroval!";

                            userbox.Text = "";
                            emailbox.Text = "";
                            passbox.Text = "";
                            repeatpassbox.Text = "";

                            Database.dbClose();
                        }
                        else
                        {
                            label6.Text = "Heslá sa nezhodujú!";
                        }
                    }
                }
                                
            }
            //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
            //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
            //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
            //prihlasovanie sa
            if (loginMode)
            {
                if (string.IsNullOrEmpty(userbox.Text) || string.IsNullOrEmpty(emailbox.Text) || string.IsNullOrEmpty(passbox.Text))
                {
                    label6.Text = "Vyplň všetky políčka!";
                }
                else
                {
                    user = new User(userbox.Text, emailbox.Text, passbox.Text);

                    Database.dbConnect();
                    user.loginUser();
                    Database.dbClose();

                    if (user.checkedUserID == user.UserID && user.checkedEmail == user.Email && user.checkedPassword == user.Password)
                    {
                        label6.Text = "prihlaseny";

                        MainForm mainform = new MainForm();
                        mainform.Show();
                        
                        prihlaseny = true;
                    }
                    else
                    {
                        label6.Text = "Nesprávne prihlasovacie údaje!";
                        user = null;
                    }
                }
            }

            
        }

        


        //tieto dve metody robia to ze vypinaju a zapinaju tlacidla
        private void label7_Click(object sender, EventArgs e)
        {              
            repeatpassbox.Enabled = false;
            label5.ForeColor = Color.FromArgb(37, 66, 156);
            panel4.BackColor = Color.FromArgb(37, 66, 156);
            button1.Text = "PRIHLÁSIŤ SA";

            label8.ForeColor = Color.White;
            label8.Cursor = Cursors.Hand;

            label7.ForeColor = Color.FromArgb(37, 66, 165);
            label7.Cursor = Cursors.Default;

            //meni mod
            loginMode = true;

            label1.Text = "PRIHLÁSENIE SA";
            label1.Location = new Point(44, 132);

            this.Text = "Prihlásenie";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            repeatpassbox.Enabled = true;
            label5.ForeColor = Color.White;
            panel4.BackColor = Color.White;
            button1.Text = "REGISTROVAŤ";

            label8.ForeColor = Color.FromArgb(37, 66, 156);
            label8.Cursor = Cursors.Default;

            label7.ForeColor = Color.White;
            label7.Cursor = Cursors.Hand;

            //meni mod
            loginMode = false;

            label1.Text = "REGISTRÁCIA";
            label1.Location = new Point(58, 132);

            this.Text = "Registrácia";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (prihlaseny)
            {
                this.Hide();
            }
        }
    }
}
