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
    public class SurveySqlDALTest
    {

        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=ParkWeather;Integrated Security=True";
        private int numRowsInSurveyTable;


        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("insert into park values ('ABC', 'ABCPark', 'OH', '0', '0', '0', '0', 'AbcClimate', '2000', '0', 'super inspiring quote', 'SuperInspiring McGee', 'Nice park.', '0', '0'); select COUNT(*) from survey_result as int", conn);
                numRowsInSurveyTable = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }



        [TestMethod]
        public void SaveSurveyTest()
        {
            ParkWeatherSqlDal dal = new ParkWeatherSqlDal(connectionString);
            Survey survey = new Survey()
            {
                ParkCode = "ABC",
                EmailAddress = "",
                State = "",
                ActivityLevel = ""
            };

            bool tableAffected = dal.SaveSurvey(survey);
            Assert.AreEqual(true, tableAffected);
            
            
        }

        [TestMethod]
        public void MostPopParkBySurveyTest()
        {
            ParkWeatherSqlDal dal = new ParkWeatherSqlDal(connectionString);
            Survey survey = new Survey()
            {
                ParkCode = "ABC",
                EmailAddress = "",
                State = "",
                ActivityLevel = ""
            };
            for (int i = 0; i < numRowsInSurveyTable + 1; i++)
            {
                dal.SaveSurvey(survey);
            }
            Park testPark = dal.MostPopularBySurvey();
            Assert.AreEqual("ABC", testPark.ParkCode);
        }

    }
}

