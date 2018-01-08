using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Capstone.UITests.PageObjects
{
    class CreatePartnerPage : BasePage
    {
        public CreatePartnerPage(IWebDriver driver)
                    : base(driver, "/Partner/CreatePartner")
            {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "CreatePartnerHead")]
        public IWebElement CreatePartnerHead { get; set; }

        [FindsBy(How = How.Name, Using = "PartnerName")]
        public IWebElement PartnerName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form button")]
        public IWebElement CreateButton { get; set; }
    }
}
