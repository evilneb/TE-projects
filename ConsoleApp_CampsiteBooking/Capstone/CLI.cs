using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    public class CLI
    {
        public CampDAL DB { get; set; }
        public DisplayMethods Display { get; set; }

        public CLI(CampDAL access, DisplayMethods display)
        {
            DB = access;
            Display = display;
        }

        public void Running()
        {
            bool running = true;

            while (running)
            {
                running = ParkMenu();
            }
            Environment.Exit(0);
        }

        public bool ParkMenu()
        {
            List<Park> parks = DB.GetParks();
            string input = Display.DisplayParkMenu(parks);
            if (input == "Q")
                return false;

            Park usePark = new Park();
            foreach (Park p in parks)
            {
                if (p.ParkID == Convert.ToInt32(input))
                    usePark = p;
            }

            bool running = true;
            while (running)
            {
                running = ParkCampgroundMenu(usePark);
            }
            return true;
        }

        public bool ParkCampgroundMenu(Park usePark)
        {
            string input = Display.DisplayParkInfo(usePark);
            if (input == "3")
                return false;
            else if (input == "2")
            {
                int resID = Display.DisplayFindReservation();
                Reservation found = DB.FindRes(resID);
                Display.DisplayFoundReservation(found);
                return true;
            }
            else
            {
                bool running = true;
                while (running)
                {
                    running = CampgroundsMenu(usePark);
                }
                return true;
            }
        }



        public bool CampgroundsMenu(Park usePark)
        {
            List<Campground> camps = DB.GetCamps(usePark.ParkID);
            string input = Display.DisplayCamps(usePark, camps);

            if (input == "p")
                return false;

            DateTime[] resDates = Display.GetResDates();
            DateTime startDate = resDates[0];
            DateTime endDate = resDates[1];

            bool running = true;
            while (running)
            {
                running = AvailableSitesMenu(Convert.ToInt32(input), startDate, endDate);
            }
            return true;
        }

        public bool AvailableSitesMenu(int campSelect, DateTime resStart, DateTime resEnd)
        {
            List<Site> sites = DB.SiteAvailable(campSelect, resStart, resEnd);
            if (sites.Count == 0)
                return false;

            decimal totalCost = DB.GetCampCost(campSelect, resStart, resEnd);
            Display.DisplayCostOfStay(totalCost);

            string[] output = Display.GetSiteList(sites);
            string useSite = output[0];
            string resName = output[1];

            int resId = DB.BookRes(Convert.ToInt32(useSite), resName, resStart, resEnd);
            Display.DisplayReservation(resId);

            return false;
        }
    }
}

