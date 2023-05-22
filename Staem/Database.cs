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
        //tymto "stringom" sa pripajame k databaze
        public static MySqlConnection connection = new MySqlConnection("Server=localhost;User ID=root;Password=;Database=c48steamV2;AllowUserVariables=True;");

        public static void dbConnect()
        {
            connection.Open();
        }
        public static void dbClose()
        {
            connection.Close();
        }
    }
}
