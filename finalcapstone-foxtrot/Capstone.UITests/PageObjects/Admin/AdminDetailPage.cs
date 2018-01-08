using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.UITests.PageObjects
{
    class AdminDetailPage : BasePage
    {
        public AdminDetailPage(IWebDriver driver)
                    : base(driver, "/User/AdminDetail")
            {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "AdminDetailHead")]
        public IWebElement AdminDetailHead { get; set; }

        [FindsBy(How = How.Id, Using = "CreateUserLink")]
        public IWebElement CreateUserLink { get; set; }

        [FindsBy(How = How.Id, Using = "RemoveUserLink")]
        public IWebElement RemoveUserLink { get; set; }

        [FindsBy(How = How.Id, Using = "CreatePartnerLink")]
        public IWebElement CreatePartnerLink { get; set; }

        [FindsBy(How = How.Id, Using = "ChangePasswordLink")]
        public IWebElement ChangePasswordLink { get; set; }

        [FindsBy(How = How.Id, Using = "AssignPartnerUserLink")]
        public IWebElement AssignPartnerUserLink { get; set; }

    }
}

