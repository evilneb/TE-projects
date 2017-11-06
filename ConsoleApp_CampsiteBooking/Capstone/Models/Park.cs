using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Models
{
    public class Park
    {
        public int ParkID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime EstabDate { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string Descrip { get; set; }
    }
}
