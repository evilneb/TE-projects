using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DataAccess;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Transactions;

namespace Capstone.Web.Tests.DALTests
{
    [TestClass]
    public class UserSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=3bar_foxtrot;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }


        [TestMethod]
        public void UserDALTest()
        {
            string joesUsername = "joeSmith";
            string joesPassword = "dfh456sdjfj789";

            User someUser = new User();
            
            someUser.Username = joesUsername;
            someUser.Password = joesPassword;
            someUser.Administrator = false;
            someUser.Researcher = false;
            someUser.Technician = false;
            someUser.Partneruser = false;


            UserSqlDAL dalObject = new UserSqlDAL(connectionString);

            dalObject.SaveUser(someUser);

            //checking if a username exists
            Assert.AreEqual(true, dalObject.UsernameExists(joesUsername));
            Assert.AreEqual(false, dalObject.UsernameExists("000"));

            //checking that a password is correct
            Assert.AreEqual(true, dalObject.PasswordIsCorrect(joesUsername, joesPassword));
            Assert.AreEqual(false, dalObject.PasswordIsCorrect(joesUsername, "000"));
            Assert.AreEqual(false, dalObject.PasswordIsCorrect("000", joesPassword));

            //checking roles for a user
            Assert.AreEqual(false, dalObject.IsAdmin(joesUsername));
            Assert.AreEqual(false, dalObject.IsResearcher(joesUsername));
            Assert.AreEqual(false, dalObject.IsTechnician(joesUsername));
            Assert.AreEqual(false, dalObject.IsPartner(joesUsername));

            //adding and removing roles for a user
            dalObject.AddAdminRole(joesUsername);
            Assert.AreEqual(true, dalObject.IsAdmin(joesUsername));
            Assert.AreEqual(false, dalObject.IsResearcher(joesUsername));
            Assert.AreEqual(false, dalObject.IsTechnician(joesUsername));
            Assert.AreEqual(false, dalObject.IsPartner(joesUsername));

            dalObject.RemoveAdminRole(joesUsername);
            dalObject.AddResearchRole(joesUsername);
            Assert.AreEqual(false, dalObject.IsAdmin(joesUsername));
            Assert.AreEqual(true, dalObject.IsResearcher(joesUsername));
            Assert.AreEqual(false, dalObject.IsTechnician(joesUsername));
            Assert.AreEqual(false, dalObject.IsPartner(joesUsername));

            dalObject.RemoveResearchRole(joesUsername);
            dalObject.AddTechRole(joesUsername);
            Assert.AreEqual(false, dalObject.IsAdmin(joesUsername));
            Assert.AreEqual(false, dalObject.IsResearcher(joesUsername));
            Assert.AreEqual(true, dalObject.IsTechnician(joesUsername));
            Assert.AreEqual(false, dalObject.IsPartner(joesUsername));

            dalObject.RemoveTechRole(joesUsername);
            dalObject.AddPartnerRole(joesUsername);
            Assert.AreEqual(false, dalObject.IsAdmin(joesUsername));
            Assert.AreEqual(false, dalObject.IsResearcher(joesUsername));
            Assert.AreEqual(false, dalObject.IsTechnician(joesUsername));
            Assert.AreEqual(true, dalObject.IsPartner(joesUsername));

            dalObject.RemovePartnerRole(joesUsername);
            Assert.AreEqual(false, dalObject.IsAdmin(joesUsername));
            Assert.AreEqual(false, dalObject.IsResearcher(joesUsername));
            Assert.AreEqual(false, dalObject.IsTechnician(joesUsername));
            Assert.AreEqual(false, dalObject.IsPartner(joesUsername));

            //Changing a user's password
            joesPassword = "pass36474word89";
            dalObject.ChangeUsersPassword(joesUsername, joesPassword);
            Assert.AreEqual(true, dalObject.PasswordIsCorrect(joesUsername, joesPassword));
        }
    }
}
