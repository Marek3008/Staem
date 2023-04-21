using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Staem
{
    class Database
    {
        public static MySqlConnection connection = new MySqlConnection("Server=banas.vybrat.eu;User ID=c48admin;Password=T2QZm7az#gR;Database=c48steamV2;AllowUserVariables=True;");

        public void dbConnect()
        {
            connection.Open();
        }
        public void dbClose()
        {
            connection.Close();
        }
    }
}
