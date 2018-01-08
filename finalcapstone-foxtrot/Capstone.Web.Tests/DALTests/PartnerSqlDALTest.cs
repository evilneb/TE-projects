using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DataAccess;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Transactions;

namespace Capstone.Web.Tests.DALTests
{
    [TestClass]
    public class PartnerSqlDALTest
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

                //add test partner
                cmd = new SqlCommand("INSERT INTO partners VALUES ('Test Partner X');", conn);
                cmd.ExecuteNonQuery();

                //add test user
                cmd = new SqlCommand("INSERT INTO users VALUES ('TestUserNameY', 'password');", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void AddPartnerTest()
        {
            //Arrange
            PartnerSqlDAL dalObject = new PartnerSqlDAL(connectionString);
            Partner testPartner = new Partner()
            {
                PartnerName = "Test Partner Z"
            };

            //Act
            bool didWork = dalObject.AddPartner(testPartner);

            //Assert
            Assert.AreEqual(true, didWork);
        }

        [TestMethod]
        public void PartnerExistsTest()
        {
            //Arrange
            PartnerSqlDAL dalObject = new PartnerSqlDAL(connectionString);
            string testPartner1 = "Test Partner X";
            string testPartner2 = "Test Partner Y";

            //Act
            bool doesExist1 = dalObject.PartnerExists(testPartner1);
            bool doesExist2 = dalObject.PartnerExists(testPartner2);

            //Assert
            Assert.AreEqual(true, doesExist1);
            Assert.AreEqual(false, doesExist2);

        }

        [TestMethod]
        public void AssignPartnerUserTest()
        {
            //Arrange
            PartnerSqlDAL dalObject = new PartnerSqlDAL(connectionString);
            AssignPartnerUser partnerUser = new AssignPartnerUser()
            {
                Partner = "Test Partner X",
                User = "testUserNameY"
            };

            //Act
            bool wasAssigned = dalObject.AssignPartnerUser(partnerUser);

            //Assert
            Assert.AreEqual(true, wasAssigned);
        }
    }
}
