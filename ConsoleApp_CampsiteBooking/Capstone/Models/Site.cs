using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Site
    {
        public int SiteID { get; set; }
        public int CampgroundID { get; set; }
        public int SiteNum { get; set; }
        public int MaxOcc { get; set; }
        public bool Accessible { get; set; } //Handicap accessible t/f
        public int MaxRv { get; set; }
        public bool Utilities { get; set; } //Utilities available t/f
    }
}
