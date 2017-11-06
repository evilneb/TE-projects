using Capstone.Web.UITests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Web.UITests.SeleniumTests
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
        public void GoTo_DetailPage()
        {
            IndexPage entryPage = new IndexPage(driver);
            entryPage.Navigate();

            entryPage.IndexPicLink.Click();
            IWebElement head = driver.FindElement(By.Id("DetailHeader"));

            Assert.IsNotNull(head);
        }

        [TestMethod]
        public void GoTo_SurveyPage()
        {
            IndexPage entryPage = new IndexPage(driver);
            entryPage.Navigate();

            entryPage.SurveyLink.Click();
            //IWebElement head = driver.FindElement(By.Id("SurveyHeader"));
            string pageHead = driver.FindElement(By.Id("SurveyHeader")).Text;

            //Assert.IsNotNull(head);
            Assert.AreEqual("Survey", pageHead);
        }

        [TestMethod]
        public void GoTo_MostPopParkPage()
        {
            SurveyPage entryPage = new SurveyPage(driver);
            entryPage.Navigate();

            MostPopParkPage resultPage = entryPage.FillOutForm("ENP","evilneb@gmail.com","OH","Active");
            string pageHead = resultPage.MostPopHeader.Text;

            Assert.AreEqual("Survey Results To Date : The Most Popular Park is...", pageHead);
        }

        [TestMethod]
        public void GoTo_IndexPage()
        {
            MostPopParkPage entryPage = new MostPopParkPage(driver);
            entryPage.Navigate();

            //entryPage.IndexPicLink.Click();

            entryPage.IndexLink.Click();
            string pageHead = driver.FindElement(By.Id("IndexHeader")).Text;

            Assert.AreEqual("Explore The National Park System", pageHead);
        }
    }
}
