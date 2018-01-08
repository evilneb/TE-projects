using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Capstone.UITests.PageObjects
{
    class ChangePassFailPage : BasePage
    {
        public ChangePassFailPage(IWebDriver driver)
                    : base(driver, "/User/AdminPasswordChangeError")
            {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "PassChangeErrorHead")]
        public IWebElement ChangePassHead { get; set; }

        [FindsBy(How = How.Id, Using = "PassChangeTryAgainLink")]
        public IWebElement PassChangeTryAgainLink { get; set; }

        [FindsBy(How = How.Id, Using = "PassChangeToAdminLink")]
        public IWebElement PassChangeToAdminLink { get; set; }
    }
}
