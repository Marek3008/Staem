using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace Staem
{
    class User
    {
        private string Email
        {
            get; set;
        }

        private string Password
        {
            get; set;
        }

        private string UserID
        {
            get; set;
        }

        public User(string userID, string email, string pass)
        {
            this.UserID = userID;
            this.Email = email; 
            this.Password = pass;
        }

        public void addUser()
        {
            MySqlCommand cmd = new MySqlCommand($"INSERT INTO Users(email, pass, userID) VALUES('{this.Email}', '{this.Password}', '{this.UserID}')", Database.connection);
            cmd.ExecuteReader();
        }
           
    }
}
