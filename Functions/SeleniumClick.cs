using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Constants;
using System;

namespace Selenium.Functions
{
    public class SeleniumClick
    {
        public SeleniumClick()
        {
        }

        public void Click(By ElementLocation, int WaitTime)
        {
            WebDriverWait Wait = new WebDriverWait(SeleniumDriver.GetDriver(), TimeSpan.FromSeconds(WaitTime));
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ElementLocation)).Click();
        }

        public void Click(By ElementLocation)
        {
            Click(ElementLocation, TimeConstants.DEFAULT_2_SECONDS);
        }

    }
}