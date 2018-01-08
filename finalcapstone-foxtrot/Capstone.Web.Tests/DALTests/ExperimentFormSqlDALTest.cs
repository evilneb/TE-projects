using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DataAccess;
using Capstone.Web.Models.ExperimentForms;
using System.Data.SqlClient;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;

namespace Capstone.Web.Tests.DALTests
{
    [TestClass]
    public class ExperimentFormSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=3bar_foxtrot;Integrated Security=True;";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();

                cmd = new SqlCommand("INSERT INTO users VALUES ('testuser', 'password');", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void CreateStorageStabilityFormTest()
        {
            //Arrange
            ExperimentFormSqlDAL dalObject = new ExperimentFormSqlDAL(connectionString);
            string username = "testuser";
            List<string> trts = new List<string> { "baselinetest", "beadstest" };
            List<string> ages = new List<string> { "0", "2", "4", "6" };
            List<string> dpas = new List<string> { "2", "7", "14"};

            StorageStabilityForm testSSForm = new StorageStabilityForm()
            {
                Strain = "ss test strain",
                FormTreatments = trts,
                Reps = 3,
                PreActivationAges = ages,
                TempdegreesCelsius = "RT",
                DPAs = dpas
            };

            //Act
            int[] check = dalObject.CreateStorageStabilityForm(username, testSSForm);

            //Assert
            Assert.AreEqual(true, check[1] > 0);
        }

        [TestMethod]
        public void CreateBeadStabilityFormTest()
        {
            //Arrange
            ExperimentFormSqlDAL dalObject = new ExperimentFormSqlDAL(connectionString);
            string username = "testuser";
            List<string> trts = new List<string> { "B" };
            List<string> ages = new List<string> { "0", "16", "20", "24" };

            BeadStabilityForm testBSForm = new BeadStabilityForm()
            {
                Strain = "bs test strain",
                FormTreatments = trts,
                TotalReps = 2,
                NumPerRep = 3,
                BeadAge = ages
            };

            //Act
            int[] check = dalObject.CreateBeadStabilityForm(username, testBSForm);

            //Assert
            Assert.AreEqual(true, check[1] > 0);
        }

        [TestMethod]
        public void CreateChemCompatFormTest()
        {
            //Arrange
            ExperimentFormSqlDAL dalObject = new ExperimentFormSqlDAL(connectionString);
            string username = "testuser";
            List<string> chems = new List<string> { "6-24-6", "8-19-3-1S-.2Zn", "4-24-0" };
            List<string> rateDilutions = new List<string> { "4X", "2X", "1X", "1/2X", "1/4X", "1/8X" };
            ChemCompatForm testCCForm = new ChemCompatForm()
            {
                Strain = "cc test strain",
                Chemicals = chems,
                Reps = 2,
                RateDilutions = rateDilutions
            };

            //Act
            int[] check = dalObject.CreateChemicalCompatibilityForm(username, testCCForm);

            //Assert
            Assert.AreEqual(true, check[1] > 0);
        }

    }
}
