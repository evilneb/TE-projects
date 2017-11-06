using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampgroundID { get; set; }
        public int ParkID { get; set; }
        public string Name { get; set; }
        public string OpenFrom { get; set; } //MM
        public string OpenTo { get; set; } //MM
        public decimal DailyFee { get; set; } //DB is 'money' might have to convert.

        Dictionary<int, string> months = new Dictionary<int, string>()
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };

        public Campground(int openFrom, int openTo)
        {
            foreach (KeyValuePair<int, string> m in months)
            {
                if (openFrom == m.Key)
                    OpenFrom = m.Value;

                if (openTo == m.Key)
                    OpenTo = m.Value;
            }
        }
    }
}
