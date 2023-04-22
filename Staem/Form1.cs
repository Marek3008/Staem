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
        Database db = new Database();
        User user;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(string.IsNullOrEmpty(userbox.Text) || string.IsNullOrEmpty(emailbox.Text) || string.IsNullOrEmpty(passbox.Text) || string.IsNullOrEmpty(repeatpassbox.Text))
            {
                label6.Text = "Vyplň všetky políčka!";
            }
            else 
            {
                if (passbox.Text == repeatpassbox.Text)
                {
                    Database.dbConnect();

                    user = new User(userbox.Text, emailbox.Text, passbox.Text);

                    user.checkUser();

                    Database.dbClose(); 

                    if (user.checkedEmail == user.Email || user.checkedUserID == user.UserID)
                    {                       
                        label6.Text = "tento účet už existuje!";

                        user = null;
                    }
                    else
                    {
                        Database.dbConnect();

                        user.addUser();

                        user = null;

                        Database.dbClose();
                    }

                    
                }
                else
                {
                    label6.Text = "Heslá sa nezhodujú!";
                }

            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
