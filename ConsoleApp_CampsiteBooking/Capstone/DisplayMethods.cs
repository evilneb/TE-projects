using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    public class DisplayMethods
    {
        public string DisplayParkMenu(List<Park> parks)
        {
            Dictionary<string, string> options = new Dictionary<string, string>();
            int i = 1;
            foreach (Park p in parks)
            {
                options[i.ToString()] = p.Name;
                i++;
            }

            options["Q"] = "Quit";

            Console.Clear();
            Console.WriteLine("Campsite Booker");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
            foreach (KeyValuePair<string, string> o in options)
            {
                Console.WriteLine("(" + o.Key + ")".PadRight(10) + o.Value);
            }
            bool go = false;
            string input = "";
            while (!go)
            {
                Console.WriteLine();
                Console.WriteLine("In which park are you looking for a site?");
                input = Console.ReadLine().ToUpper();
                go = options.ContainsKey(input);
                if (!go)
                    Console.WriteLine("Invalid Selection!");
            }
            return input;
        }

        public string DisplayParkInfo(Park park)
        {
            Dictionary<string, string> options = new Dictionary<string, string>()
            {
                { "1", "View Campgrounds" },
                { "2", "Search for an existing Reservation" },
                { "3", "Previous Menu" }
            };

            Console.Clear();
            Console.WriteLine(park.Name);
            Console.WriteLine("Location:".PadRight(30) + park.Location);
            Console.WriteLine("Established:".PadRight(30) + park.EstabDate.ToShortDateString());
            Console.WriteLine("Area:".PadRight(30) + park.Area.ToString() + " acres");
            Console.WriteLine("Annual Visitors:".PadRight(30) + park.Visitors.ToString());
            Console.WriteLine();
            Console.WriteLine(park.Descrip);
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine();
            foreach (KeyValuePair<string, string> o in options)
                Console.WriteLine("(" + o.Key + ") " + o.Value);

            bool go = false;
            string input = "";
            while (!go)
            {
                Console.WriteLine();
                Console.WriteLine("Next Selection:");
                input = Console.ReadLine();
                go = options.ContainsKey(input);
                if (!go)
                    Console.WriteLine("Invalid Selection!");
            }
            return input;
        }

        public int DisplayFindReservation()
        {
            Console.WriteLine();
            Console.WriteLine("Reservation ID to look for:");
            int resID = Convert.ToInt32(Console.ReadLine());
            return resID;
        }

        public void DisplayFoundReservation(Reservation foundRes)
        {
            Console.WriteLine();
            if (foundRes.ReservationID == 0)
            {
                Console.WriteLine("There is no Reservation with that ID number.");
            }
            else
            {
                Console.WriteLine(foundRes.ReservationID.ToString().PadRight(10) + foundRes.SiteID.ToString().PadRight(10) + foundRes.Name.PadRight(25) + foundRes.FromDate.ToShortDateString().PadRight(15) + foundRes.ToDate.ToShortDateString().PadRight(15) + foundRes.CreateDate.ToString());
            }
            Console.ReadLine();
        }

        public string DisplayCamps(Park park, List<Campground> camps)
        {
            Dictionary<string, Campground> options = new Dictionary<string, Campground>();
            int i = 1;
            foreach (Campground c in camps)
            {
                options[i.ToString()] = c;
                i++;
            }

            Console.Clear();
            Console.WriteLine(park.Name + " Campgrounds");
            Console.WriteLine();
            Console.WriteLine("".PadRight(10) + "Name".PadRight(35) + "Open".PadRight(15) + "Close".PadRight(15) + "Daily Rate");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            foreach (KeyValuePair<string, Campground> o in options)
            {
                Console.WriteLine(("(" + o.Key + ")").PadRight(10) + o.Value.Name.PadRight(35) + o.Value.OpenFrom.PadRight(15) + o.Value.OpenTo.PadRight(15) + "$" + Convert.ToInt32(o.Value.DailyFee).ToString());
            }
            Console.WriteLine();
            Console.WriteLine("(" + i.ToString() + ")  Previous Menu");

            bool go = false;
            string input = "";
            string output = "";
            while (!go)
            {
                Console.WriteLine();
                Console.WriteLine("Make a selection:");
                input = Console.ReadLine();
                go = options.ContainsKey(input);
                if (input == i.ToString())
                {
                    output = "p";
                    break;
                }
                else if (!go)
                    Console.WriteLine("Invalid Selection!");
                else
                    output = options[input].CampgroundID.ToString();
            }
            return output;
        }

        public DateTime[] GetResDates()
        {
            DateTime[] input = new DateTime[2];
            bool go = false;
            while (!go)
            {
                Console.WriteLine("What is your desired arrival date? (yyyy-mm-dd)");
                go = DateTime.TryParse(Console.ReadLine(), out input[0]);
                if (!go)
                    Console.WriteLine("Invalid Date.");
            }

            go = false;
            while (!go)
            {
                Console.WriteLine("What is your desired departure date? (yyyy-mm-dd)");
                go = DateTime.TryParse(Console.ReadLine(), out input[1]);
                if (!go)
                    Console.WriteLine("Invalid Date.");
            }
            return input;
        }

        public void DisplayCostOfStay(decimal total)
        {
            Console.Clear();
            Console.WriteLine("Your stay will total to $" + Convert.ToInt32(total).ToString());
        }

        public string[] GetSiteList(List<Site> available)
        {
            string[] result = new string[2];
            Dictionary<int, string> sites = new Dictionary<int, string>();
            foreach (Site s in available)
                sites[s.SiteID] = s.MaxOcc.ToString().PadRight(15) + s.Accessible.ToString().PadRight(15) + s.MaxRv.ToString().PadRight(15) + s.Utilities.ToString();

            Console.WriteLine("Available Campsites:");
            Console.WriteLine("Site No.".PadRight(15) + "Max Occup.".PadRight(15) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(15) + "Utility");
            Console.WriteLine();
            foreach (KeyValuePair<int, string> o in sites)
            {
                Console.WriteLine(o.Key.ToString().PadRight(15) + o.Value);
            }

            int input = 0;
            bool go = false;
            while (!go)
            {
                Console.WriteLine("Site to Reserve: ");
                input = Convert.ToInt32(Console.ReadLine());
                go = sites.ContainsKey(input);
                if (!go)
                    Console.WriteLine("Invalid Selection.");
                else
                    result[0] = input.ToString();
            }

            Console.WriteLine("Name for Reservation: ");
            result[1] = Console.ReadLine();
            return result;
        }

        public void DisplayReservation(int resId)
        {
            Console.WriteLine("Your reservation was booked! Confirmation #: " + resId);

            Console.ReadLine();
        }
    }
}
