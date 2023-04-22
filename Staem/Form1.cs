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
    public partial class Form1 : Form
    {
        User user;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //kontroluje ci su vsetky policka vyplnene
            if(string.IsNullOrEmpty(userbox.Text) || string.IsNullOrEmpty(emailbox.Text) || string.IsNullOrEmpty(passbox.Text) || string.IsNullOrEmpty(repeatpassbox.Text))
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
                    if(passbox.Text == repeatpassbox.Text)
                    {
                        Database.dbConnect();

                        //prida pouzivatela do databazy
                        user.addUser();

                        //vymaze udaje o pouzivatelovi aby sa objekt mohol pouzit nanovo
                        user = null;

                        label6.Text = "";

                        Database.dbClose();
                    }
                    else
                    {
                        label6.Text = "Heslá sa nezhodujú!";
                    }
                }                
                                
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}
