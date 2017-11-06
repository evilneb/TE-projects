using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class Helper
    {
        public Park CreateParks(SqlDataReader results)
        {
            Park park = new Park();

            park.ParkID = Convert.ToInt32(results["park_id"]);
            park.Name = Convert.ToString(results["name"]);
            park.Location = Convert.ToString(results["location"]);
            park.EstabDate = Convert.ToDateTime(results["establish_date"]);
            park.Area = Convert.ToInt32(results["area"]);
            park.Visitors = Convert.ToInt32(results["visitors"]);
            park.Descrip = Convert.ToString(results["description"]);

            return park;
        }

        public Campground CreateCamps(SqlDataReader results)
        {
            Campground camp = new Campground(Convert.ToInt32(results["open_from_mm"]), Convert.ToInt32(results["open_to_mm"]));

            camp.CampgroundID = Convert.ToInt32(results["campground_id"]);
            camp.ParkID = Convert.ToInt32(results["park_id"]);
            camp.Name = Convert.ToString(results["name"]);
            camp.DailyFee = Convert.ToDecimal(results["daily_fee"]);

            return camp;
        }

        public Reservation CreateRes(SqlDataReader results)
        {
            Reservation res = new Reservation();

            res.ReservationID = Convert.ToInt32(results["reservation_id"]);
            res.SiteID = Convert.ToInt32(results["site_id"]);
            res.Name = Convert.ToString(results["name"]);
            res.FromDate = Convert.ToDateTime(results["from_date"]);
            res.ToDate = Convert.ToDateTime(results["to_date"]);
            res.CreateDate = Convert.ToDateTime(results["create_date"]);

            return res;
        }

        public Site CreateSite(SqlDataReader results)
        {
            Site site = new Site();

            site.SiteID = Convert.ToInt32(results["site_id"]);
            site.CampgroundID = Convert.ToInt32(results["campground_id"]);
            site.SiteNum = Convert.ToInt32(results["site_number"]);
            site.MaxOcc = Convert.ToInt32(results["max_occupancy"]);
            site.Accessible = Convert.ToBoolean(results["accessible"]);
            site.MaxRv = Convert.ToInt32(results["max_rv_length"]);
            site.Utilities = Convert.ToBoolean(results["utilities"]);

            return site;
        }
    }
}
