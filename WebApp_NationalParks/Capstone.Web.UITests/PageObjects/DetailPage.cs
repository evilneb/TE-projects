using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.UITests.PageObjects
{
    public class DetailPage : BasePage
    {
        public DetailPage(IWebDriver driver)
                : base(driver, "/Home/Detail")
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "DetailHeader")]
        public IWebElement DetailHeader { get; set; }

        [FindsBy(How = How.Id, Using = "IndexLink")]
        public IWebElement IndexLink { get; set; }

    }
}
