using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Dal;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Transactions;

namespace Capstone.Web.Tests.DALTests
{
    [TestClass]
    public class ParkSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=ParkWeather;Integrated Security=True";
        private string parkCode = "ABC";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("insert into park values ('ABC', 'ABCPark', 'OH', '0', '0', '0', '0', 'AbcClimate', '2000', '0', 'super inspiring quote', 'SuperInspiring McGee', 'Nice park.', '0', '0');", conn);
                cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }



        [TestMethod]
        public void GetParkTest()
        {
            ParkWeatherSqlDal dal = new ParkWeatherSqlDal(connectionString);

            List<Park> parkList = dal.GetParks();

            Park testPark = new Park();

            foreach (Park x in parkList)
            {
                if (x.ParkCode == parkCode)
                {
                    testPark = x;
                }
            }

            Assert.AreEqual(parkCode, testPark.ParkCode);
            Assert.AreEqual("ABCPark", testPark.Name);
            Assert.AreEqual("OH", testPark.State);
            Assert.AreEqual("0", testPark.Acreage);
            Assert.AreEqual("0", testPark.Elevation);

            Assert.AreEqual("0", testPark.MilesOfTrail);
            Assert.AreEqual("0", testPark.NumCampsites);
            Assert.AreEqual("AbcClimate", testPark.Climate);
            Assert.AreEqual("2000", testPark.YearFounded);
            Assert.AreEqual("0", testPark.AnnualVisitorCount);

            Assert.AreEqual("super inspiring quote", testPark.Quote);
            Assert.AreEqual("SuperInspiring McGee", testPark.QuoteSource);
            Assert.AreEqual("Nice park.", testPark.Description);
            Assert.AreEqual("0", testPark.EntryFee);
            Assert.AreEqual("0", testPark.NumAnimalSpecies);


        }
    }
}
