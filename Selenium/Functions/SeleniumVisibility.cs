using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Constants;
using SeleniumExtras.WaitHelpers;

namespace Selenium.Functions
{
    public static class SeleniumVisibility
    {
        public static bool IsElementVisible(By locator, int waitInSecs)
        {
            WebDriverWait wait = new WebDriverWait(SeleniumDriver.GetDriver(), TimeSpan.FromSeconds(waitInSecs));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
        }

        public static bool IsElementVisible(By locator) => IsElementVisible(locator, TimeConstants.DEFAULT_2_SECONDS);
    }
}
