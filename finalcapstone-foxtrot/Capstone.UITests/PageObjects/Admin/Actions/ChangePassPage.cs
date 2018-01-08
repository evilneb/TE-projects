using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.UITests.PageObjects
{
    class ChangePassPage : BasePage
    {
        public ChangePassPage(IWebDriver driver)
                    : base(driver, "/User/AdminChangeUserPassword")
            {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "ChangePassHead")]
        public IWebElement ChangePassHead { get; set; }

        [FindsBy(How = How.Name, Using = "Username")]
        public IWebElement Username { get; set; }

        [FindsBy(How = How.Name, Using = "Password")]
        public IWebElement Password { get; set; }

        //Role Checkboxes
        [FindsBy(How = How.Name, Using = "Administrator")]
        public IWebElement Administrator { get; set; }

        [FindsBy(How = How.Name, Using = "Researcher")]
        public IWebElement Researcher { get; set; }

        [FindsBy(How = How.Name, Using = "Technician")]
        public IWebElement Technician { get; set; }

        [FindsBy(How = How.Name, Using = "Partner")]
        public IWebElement Partner { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form button")]
        public IWebElement CreateButton { get; set; }
    }
}
