using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.UITests.PageObjects
{
    public class SurveyPage : BasePage
    {
        public SurveyPage(IWebDriver driver)
                : base(driver, "/Home/Survey")
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "IndexLink")]
        public IWebElement IndexLink { get; set; }

        [FindsBy(How = How.Name, Using = "ParkCode")]
        public IWebElement ParkCode { get; set; }

        [FindsBy(How = How.Name, Using = "EmailAddress")]
        public IWebElement EmailAddress { get; set; }

        [FindsBy(How = How.Name, Using = "State")]
        public IWebElement State { get; set; }

        [FindsBy(How = How.Name, Using = "ActivityLevel")]
        public IWebElement ActivityLevel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form button")]
        public IWebElement CalculateButton { get; set; }

        public MostPopParkPage FillOutForm(string parkCode, string email, string state, string activity)
        {
            SelectElement parkDrop = new SelectElement(ParkCode);
            parkDrop.SelectByValue(parkCode);

            EmailAddress.SendKeys(email.ToString());

            State.SendKeys(state.ToString());

            SelectElement activityDrop = new SelectElement(ActivityLevel);
            activityDrop.SelectByText(activity);

            //string id = $"Number_Of_Years_{(int)loanTerm}";
            //IWebElement radioButton = driver.FindElement(By.Id(id));
            //radioButton.Click();

            CalculateButton.Click();

            return new MostPopParkPage(driver);
        }
    }
}
