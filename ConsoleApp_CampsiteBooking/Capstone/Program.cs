using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbConnection = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
            CampDAL dbAccess = new CampDAL(dbConnection);
            DisplayMethods display = new DisplayMethods();
            CLI myMenu = new CLI(dbAccess, display);
            
            myMenu.Running();
        }
    }
}
