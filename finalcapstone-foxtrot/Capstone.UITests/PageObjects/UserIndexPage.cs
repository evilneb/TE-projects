using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.UITests.PageObjects
{
    class UserIndexPage : BasePage
    {
        public UserIndexPage(IWebDriver driver)
                : base(driver, "/User/Index")
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "UserIndexHead")]
        public IWebElement UserIndexHead { get; set; }

        [FindsBy(How = How.Id, Using = "AdminLink")]
        public IWebElement AdminLink { get; set; }

        [FindsBy(How = How.Id, Using = "ResearcherLink")]
        public IWebElement ResearcherLink { get; set; }

        [FindsBy(How = How.Id, Using = "TechnicianLink")]
        public IWebElement TechnicianLink { get; set; }

        [FindsBy(How = How.Id, Using = "PartnerLink")]
        public IWebElement PartnerLink { get; set; }
    }
}
