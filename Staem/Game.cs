using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data.SqlClient;

namespace Staem
{
    //tato klasa sluzi len na to aby som mohol vytvorit objekt s vlastnostami uvedenymi nizsie
    public class Game
    {
        public string Name
        {
            get; set;
        }

        public string Category
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }
        public string Price
        {
            get; set;
        }
        public string Developer
        {
            get; set;
        }

        public string Path
        {
            get; set;
        }

        public bool MamTutoHru
        {
            get; set;
        }

        public Game(string name, string category, string description, string price, string developer, string path)
        {
            this.Name = name;
            this.Category = category;
            this.Description = description;
            this.Price = price;
            this.Developer = developer;
            this.Path = path;
        }
         

    }
}
