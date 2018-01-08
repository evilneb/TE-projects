using Capstone.UITests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.UITests.SeleniumTests
{
    [TestClass]
    public class NavTests
    {
        private static IWebDriver driver;

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:55601/");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void GoTo_AdminDetailPage()
        {
            UserIndexPage entryPage = new UserIndexPage(driver);
            entryPage.Navigate();

            entryPage.AdminLink.Click();
            IWebElement head = driver.FindElement(By.Id("AdminDetailHead"));

            Assert.IsNotNull(head);
        }

        [TestMethod]
        public void GoTo_CreateUserPage()
        {
            AdminDetailPage entryPage = new AdminDetailPage(driver);
            entryPage.Navigate();

            entryPage.CreateUserLink.Click();
            IWebElement head = driver.FindElement(By.Id("CreateUserHead"));

            Assert.IsNotNull(head);
        }

        [TestMethod]
        public void GoTo_CreatePartnerPage()
        {
            AdminDetailPage entryPage = new AdminDetailPage(driver);
            entryPage.Navigate();

            entryPage.CreatePartnerLink.Click();
            IWebElement head = driver.FindElement(By.Id("CreatePartnerHead"));

            Assert.IsNotNull(head);
        }

        [TestMethod]
        public void GoTo_ChangePasswordPage()
        {
            AdminDetailPage entryPage = new AdminDetailPage(driver);
            entryPage.Navigate();

            entryPage.ChangePasswordLink.Click();
            IWebElement head = driver.FindElement(By.Id("ChangePassHead"));

            Assert.IsNotNull(head);
        }

        [TestMethod]
        public void GoTo_AssignPartnerUserPage()
        {
            AdminDetailPage entryPage = new AdminDetailPage(driver);
            entryPage.Navigate();

            entryPage.AssignPartnerUserLink.Click();
            IWebElement head = driver.FindElement(By.Id("UserToPartnerHead"));

            Assert.IsNotNull(head);
        }

    }
}
