using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Capstone.UITests.PageObjects
{
    class ChangePassSuccessPage : BasePage
    {
        public ChangePassSuccessPage(IWebDriver driver)
                    : base(driver, "/User/PasswordChangeConfirm")
            {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "PasswordChangeSuccessHead")]
        public IWebElement PasswordChangeSuccessHead { get; set; }

        [FindsBy(How = How.Id, Using = "CreatePartnerLink")]
        public IWebElement CreatePartnerLink { get; set; }

    }
}
