using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Capstone.UITests.PageObjects
{
    class CreateUserFailPage : BasePage
    {
        public CreateUserFailPage(IWebDriver driver)
                    : base(driver, "/User/NewUserError")
            {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "ChangePassHead")]
        public IWebElement ChangePassHead { get; set; }

        [FindsBy(How = How.Id, Using = "CreatePartnerLink")]
        public IWebElement CreatePartnerLink { get; set; }
    }
}
