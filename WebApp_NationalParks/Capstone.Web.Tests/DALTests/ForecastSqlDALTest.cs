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
    public class ForecastSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=ParkWeather;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("insert into park values ('ABC', 'ABCPark', 'OH', '0', '0', '0', '0', 'AbcClimate', '2000', '0', 'super inspiring quote', 'SuperInspiring McGee', 'Nice park.', '0', '0'); insert into weather values ('ABC', 1, 0, 100, '')", conn);
                cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }


        [TestMethod]
        public void GetForecastsTest()
        {
            ParkWeatherSqlDal dal = new ParkWeatherSqlDal(connectionString);
            List<DayForecast> forecastABC = dal.GetForecasts("ABC");

            Assert.AreEqual("ABC", forecastABC[0].ParkCode);
            Assert.AreEqual("", forecastABC[0].Forecast);
            Assert.AreEqual(0, forecastABC[0].Low);
            Assert.AreEqual(100, forecastABC[0].High);
        }
    }
}
