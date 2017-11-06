using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.UITests.PageObjects
{
    public class IndexPage : BasePage
    {
        public IndexPage(IWebDriver driver)
                : base(driver, "/Home/Index")
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "IndexHeader")]
        public IWebElement IndexHeader { get; set; }

        [FindsBy(How = How.Id, Using = "IndexPicLink")]
        public IWebElement IndexPicLink { get; set; }

        [FindsBy(How = How.Id, Using = "SurveyLink")]
        public IWebElement SurveyLink { get; set; }

    }
}
