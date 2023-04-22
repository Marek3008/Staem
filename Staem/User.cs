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

        //tu sa zapisu hodnoty ktore najdeme v databaze ak ich najdeme; pouziva sa na kontrolovanie duplikovanych pouzivatelov
        public string checkedEmail;
        public string checkedUserID;
        public string checkedPassword;

        public User(string userID, string email, string pass)
        {
            this.UserID = userID;
            this.Email = email; 
            this.Password = pass;
        }

        public void addUser()
        {
            //SQL prikaz ktorym pridavame pouzivatelov
            MySqlCommand cmd = new MySqlCommand($"INSERT INTO Users(email, pass, userID) VALUES('{this.Email}', '{this.Password}', '{this.UserID}')", Database.connection);

            //prikaz sa vykona (moze sa pouzit aj ExecuteReader)
            cmd.ExecuteNonQuery();
        }

        public void checkUser()
        {
            //SQL pirkaz na kontrolovanie duplikovanych pouzivatelov
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE email = '{this.Email}' OR userID = '{this.UserID}';", Database.connection);

            //toto tu popravde asi ani nemusi byt
            cmd.CommandType = CommandType.Text;

            //tento objekt pouzivame na to aby sme prebehli celu vyselectovanu tabulku
            MySqlDataReader reader = cmd.ExecuteReader();
            
            //vdaka while-u prebehnem vsetky riadky a mozem z nich vybrat jednotlive hodnoty a zapisat ich
            while (reader.Read())
            {
                checkedEmail = reader["email"].ToString();
                checkedUserID = reader["userID"].ToString();
            }
        }

        public void loginUser()
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE email = '{this.Email}' AND userID = '{this.UserID}' AND pass = '{this.Password}'", Database.connection);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                checkedEmail = reader["email"].ToString();
                checkedUserID = reader["userID"].ToString();
                checkedPassword = reader["pass"].ToString();
            }
        }
           
    }
}
