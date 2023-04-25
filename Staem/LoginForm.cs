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

namespace Staem
{

    public partial class LoginForm : Form
    {
        User user;
        bool loginMode = false;
        bool prihlaseny = false;

        //menim fonty
        PrivateFontCollection privateFontCollection = new PrivateFontCollection();
        string bruno = Path.GetFullPath("BrunoAceSC.ttf");
        string bauhaus = Path.GetFullPath("Bauhaus93.ttf");

        public LoginForm()
        {
            InitializeComponent();

            //"vypne" label8 a.k.a chcem sa registrovat
            label8.ForeColor = Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            label8.Cursor = Cursors.Default;

            privateFontCollection.AddFontFile(bauhaus);
            privateFontCollection.AddFontFile(bruno);
            label1.Font = new Font(privateFontCollection.Families[0], 26, FontStyle.Regular);
            label2.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            label3.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            label4.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            label5.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            label9.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            userbox.Font = new Font(privateFontCollection.Families[1], 11, FontStyle.Regular);
            emailbox.Font = new Font(privateFontCollection.Families[1], 11, FontStyle.Regular);
            passbox.Font = new Font(privateFontCollection.Families[1], 11, FontStyle.Regular);
            repeatpassbox.Font = new Font(privateFontCollection.Families[1], 11, FontStyle.Regular);
            button1.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
        }

        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

        //tato metoda ma za ulohu registrovat a prihlasovat pouzivatelov
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
                    label6.Location = new Point(66, 366);
                    label6.ForeColor = Color.FromArgb(255, 65, 65);
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
                        label6.Location = new Point(60, 366);
                        label6.ForeColor = Color.FromArgb(255, 65, 65);
                        label6.Text = "Tento účet už existuje!";

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

                            label6.Location = new Point(34, 366);
                            label6.ForeColor = Color.Green;
                            label6.Text = "Úspešne si sa zaregistroval!";

                            //vymaze text v textboxoch
                            userbox.Text = "";
                            emailbox.Text = "";
                            passbox.Text = "";
                            repeatpassbox.Text = "";

                            Database.dbClose();
                        }
                        else
                        {
                            label6.Location = new Point(71, 366);
                            label6.ForeColor = Color.FromArgb(255, 65, 65);
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
                //kontrola ci niesu prazdne policka
                if (string.IsNullOrEmpty(userbox.Text) || string.IsNullOrEmpty(emailbox.Text) || string.IsNullOrEmpty(passbox.Text))
                {
                    label6.Location = new Point(66, 366);
                    label6.ForeColor = Color.FromArgb(255, 65, 65);
                    label6.Text = "Vyplň všetky políčka!";
                }
                else
                {
                    //vytvara sa pouzivatel
                    user = new User(userbox.Text, emailbox.Text, passbox.Text);

                    Database.dbConnect();

                    //nie je dobry nazov pre metodu; zapise userID, email a heslo z databazy do checkovacich premennych v User.cs
                    user.loginUser();

                    Database.dbClose();

                    //kontroluje ci udaje z databazy sa rovnaju udajom ktore pouzivatel zadal
                    if (user.checkedUserID == user.UserID && user.checkedEmail == user.Email && user.checkedPassword == user.Password)
                    {
                        label6.ForeColor = Color.Green;
                        label6.Text = "prihlaseny";

                        //ak su udaje spravne tak si prihlaseny a otvori sa hlavne okno
                        MainForm mainform = new MainForm(user);
                        mainform.Show();
                        
                        //pouziva sa nizsie
                        prihlaseny = true;
                    }
                    else
                    {
                        label6.Location = new Point(18, 366);
                        label6.ForeColor = Color.FromArgb(255, 65, 65);
                        label6.Text = "Nesprávne prihlasovacie údaje!";
                        user = null;
                    }
                }
            }            
        }
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""


        //tieto dve metody robia to ze vypinaju a zapinaju tlacidla
        private void label7_Click(object sender, EventArgs e)
        {              
            repeatpassbox.Enabled = false;
            label5.ForeColor = Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            label9.ForeColor = Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            panel4.BackColor = Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            button1.Text = "PRIHLÁSIŤ SA";

            label8.ForeColor = Color.White;
            label8.Cursor = Cursors.Hand;

            label7.ForeColor = Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            label7.Cursor = Cursors.Default;

            //meni mod
            loginMode = true;

            label1.Text = "PRIHLÁSENIE";
            label1.Location = new Point(63, 132);

            label1.Font = new Font(privateFontCollection.Families[0], 26, FontStyle.Regular);
            
            passbox.Text = "";
            userbox.Text = "";
            repeatpassbox.Text = "";
            emailbox.Text = "";

            //meni titel okna
            this.Text = "Prihlásenie";

            label6.Text = "";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            repeatpassbox.Enabled = true;
            label5.ForeColor = Color.White;
            label9.ForeColor = Color.White;
            panel4.BackColor = Color.White;
            button1.Text = "REGISTROVAŤ";

            label8.ForeColor = Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            label8.Cursor = Cursors.Default;

            label7.ForeColor = Color.White;
            label7.Cursor = Cursors.Hand;

            //meni mod
            loginMode = false;

            label1.Text = "REGISTRÁCIA";
            label1.Location = new Point(58, 132);

            label1.Font = new Font(privateFontCollection.Families[0], 26, FontStyle.Regular);


            passbox.Text = "";
            userbox.Text = "";
            repeatpassbox.Text = "";
            emailbox.Text = "";

            //meni titel okna
            this.Text = "Registrácia";

            label6.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //kontroluje ci sa pouzivatel prihlasil
            if (prihlaseny)
            {
                //ak sa prihlasil tak schova login okno
                this.Hide();
            }

            /*
            label2.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            label3.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            label4.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            label5.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            label9.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            userbox.Font = new Font(privateFontCollection.Families[1], 11, FontStyle.Regular);
            emailbox.Font = new Font(privateFontCollection.Families[1], 11, FontStyle.Regular);
            passbox.Font = new Font(privateFontCollection.Families[1], 11, FontStyle.Regular);
            repeatpassbox.Font = new Font(privateFontCollection.Families[1], 11, FontStyle.Regular);
            button1.Font = new Font(privateFontCollection.Families[1], 12, FontStyle.Bold);
            */
        }

    }
}
