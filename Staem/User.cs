using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data.SqlClient;

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

        public void addUser()
        {
            MySqlCommand cmd = new MySqlCommand($"INSERT INTO Users(email, pass) VALUES('{this.Email}', '{this.Password}')", Database.connection);
            cmd.ExecuteReader();
        }
           
    }
}
