using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Data;

namespace Staem
{
    class User
    {
        public string Email
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string UserID
        {
            get; set;
        }
        public string checkedEmail;
        public string checkedUserID;

        public User(string userID, string email, string pass)
        {
            this.UserID = userID;
            this.Email = email; 
            this.Password = pass;
        }

        public void addUser()
        {
            MySqlCommand cmd = new MySqlCommand($"INSERT INTO Users(email, pass, userID) VALUES('{this.Email}', '{this.Password}', '{this.UserID}')", Database.connection);
            cmd.ExecuteNonQuery();
        }

        public void checkUser()
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE email = '{this.Email}' OR userID = '{this.UserID}';", Database.connection);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader reader = cmd.ExecuteReader(); 
            while (reader.Read())
            {
                checkedEmail = reader["email"].ToString();
                checkedUserID = reader["userID"].ToString();
            }
        }
           
    }
}
