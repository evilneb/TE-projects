using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.UITests.PageObjects
{
    public class MostPopParkPage : BasePage
    {
        public MostPopParkPage(IWebDriver driver)
                : base(driver, "/Home/MostPopPark")
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "MostPopHeader")]
        public IWebElement MostPopHeader { get; set; }

        [FindsBy(How = How.Id, Using = "IndexLink")]
        public IWebElement IndexLink { get; set; }

    }
}
