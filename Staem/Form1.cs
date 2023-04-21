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
            db.dbConnect();

            user = new User();

            user.Email = textBox1.Text;
            user.Password = textBox2.Text;

            user.addUser();
            user = null;

            db.dbClose();
        }
    }
}
