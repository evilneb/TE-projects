using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using System.Data.SqlClient;
using Capstone.Models;
using System.Configuration;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class DALTests
    {
        private TransactionScope tran;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=campground;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetParksTest()
        {
            CampDAL test = new CampDAL(connectionString);
            Assert.IsNotNull(test.GetParks());
        }

        [TestMethod]
        public void GetCampsTest()
        {
            CampDAL test = new CampDAL(connectionString);
            Assert.IsNotNull(test.GetCamps(1));
        }

        [TestMethod]
        public void GetCampCostTest()
        {
            int campID = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO campground(park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES(1, 'reginald', 11, 03, 500)", conn);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT campground_id FROM campground WHERE name = 'reginald'", conn);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        campID = Convert.ToInt32(r["campground_id"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            CampDAL test = new CampDAL(connectionString);
            DateTime start = new DateTime(2017, 10, 05);
            DateTime end = new DateTime(2017, 10, 10);
            int result = Convert.ToInt32(test.GetCampCost(campID, start, end));
            Assert.AreEqual(2500, result);
        }

        [TestMethod]
        public void SitesAvailableTest()
        {
            CampDAL test = new CampDAL(connectionString);
            DateTime start = new DateTime(2017, 03, 05);
            DateTime end = new DateTime(2017, 03, 10);
            List<Site> list = test.SiteAvailable(1, start, end);
            Assert.AreEqual(12, list.Count);
        }

        [TestMethod]
        public void BookResTest()
        {
            int resID = 0;
            CampDAL test = new CampDAL(connectionString);
            DateTime start = new DateTime(2017, 04, 05);
            DateTime end = new DateTime(2017, 04, 10);
            int id = test.BookRes(5, "Test Fam", start, end);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM reservation WHERE name = 'Test Fam'", conn);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        resID = Convert.ToInt32(r["reservation_id"]);
                    }
                }
            }
            catch
            {
                throw;
            }
            Assert.AreNotEqual(0, resID);
        }

        [TestMethod]
        public void FindResTest()
        {
            CampDAL test = new CampDAL(connectionString);
            Reservation res = test.FindRes(1);
            Assert.IsNotNull(res);
        }
    }
}
