using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Capstone.UITests.PageObjects
{
    class AssignPartnerUserPage : BasePage
    {
        public AssignPartnerUserPage(IWebDriver driver)
                    : base(driver, "/Partner/AssignPartnerUser")
            {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "UserToPartnerHead")]
        public IWebElement UserToPartnerHead { get; set; }

        [FindsBy(How = How.Name, Using = "User")]
        public IWebElement User { get; set; }

        [FindsBy(How = How.Name, Using = "Partner")]
        public IWebElement Partner { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form button")]
        public IWebElement CreateButton { get; set; }
    }
}
