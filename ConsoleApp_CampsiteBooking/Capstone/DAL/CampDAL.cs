using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampDAL
    {
        private const string getParks = "SELECT * FROM park";
        private const string getCamps = "SELECT * FROM campground WHERE park_id = @parkSelect";
        private const string searchRes = "SELECT * from site WHERE site_id NOT IN (SELECT site_id FROM reservation WHERE (@resStart BETWEEN reservation.from_date and reservation.to_date OR @resEnd BETWEEN reservation.from_date and reservation.to_date) OR (reservation.from_date BETWEEN @resStart and @resEnd OR reservation.to_date BETWEEN @resStart and @resEnd)) AND campground_id = @campSelect ORDER BY site_id asc";
        private const string getCost = "SELECT daily_fee FROM campground WHERE campground_id = @campSelect";
        private const string bookRes = "INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@siteSelect, @resName, @resStart, @resEnd)";
        private const string getResID = "SELECT reservation_id FROM reservation WHERE name = @resName";
        private const string findRes = "SELECT * FROM reservation WHERE reservation_id = @resID";

        public string DBconnection { get; set; }

        private Helper helper = new Helper();

        public CampDAL(string connection)
        {
            DBconnection = connection;
        }
        public List<Park> GetParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(DBconnection))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(getParks, conn);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        parks.Add(helper.CreateParks(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return parks;
        }

        public List<Campground> GetCamps(int parkSelect)
        {
            List<Campground> camps = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(DBconnection))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(getCamps, conn);

                    cmd.Parameters.AddWithValue("@parkSelect", parkSelect);

                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        camps.Add(helper.CreateCamps(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return camps;
        }

        public decimal GetCampCost(int campSelect, DateTime resStart, DateTime resEnd)
        {
            decimal campCost = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(DBconnection))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(getCost, conn);

                    cmd.Parameters.AddWithValue("@campSelect", campSelect);

                    SqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        campCost = Convert.ToDecimal(r["daily_fee"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            TimeSpan stayLength = resEnd.Subtract(resStart);
            int days = Convert.ToInt32(stayLength.TotalDays);
            campCost *= days;
            return campCost;

        }

        public List<Site> SiteAvailable(int campSelect, DateTime resStart, DateTime resEnd)
        {
            List<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(DBconnection))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(searchRes, conn);

                    cmd.Parameters.AddWithValue("@campSelect", campSelect);
                    cmd.Parameters.AddWithValue("@resStart", resStart);
                    cmd.Parameters.AddWithValue("@resEnd", resEnd);

                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        sites.Add(helper.CreateSite(results));
                    }
                }

            }
            catch (SqlException ex)
            {
                throw;
            }

            return sites;
        }

        public int BookRes(int siteSelect, string resName, DateTime resStart, DateTime resEnd)
        {
            int resID = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(DBconnection))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(bookRes, conn);

                    cmd.Parameters.AddWithValue("@siteSelect", siteSelect);
                    cmd.Parameters.AddWithValue("@resName", resName);
                    cmd.Parameters.AddWithValue("@resStart", resStart);
                    cmd.Parameters.AddWithValue("@resEnd", resEnd);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        cmd = new SqlCommand(getResID, conn);
                        cmd.Parameters.AddWithValue("@resName", resName);
                        SqlDataReader r = cmd.ExecuteReader();

                        while (r.Read())
                        {
                            resID = Convert.ToInt32(r["reservation_id"]);
                        }
                    }
                }
                return resID;

            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public Reservation FindRes(int resID)
        {
            Reservation found = new Reservation();
            try
            {
                using (SqlConnection conn = new SqlConnection(DBconnection))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(findRes, conn);
                    cmd.Parameters.AddWithValue("@resID", resID);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        found = helper.CreateRes(r);
                    }
                }
                if (found == null)
                    found.ReservationID = 0;

                return found;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
