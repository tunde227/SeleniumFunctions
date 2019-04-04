using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Constants;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

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

        public static bool IsElementNotVisible(By locator, int waitInSecs)
        {
            WebDriverWait wait = new WebDriverWait(SeleniumDriver.GetDriver(), TimeSpan.FromSeconds(waitInSecs));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static bool IsElementNotVisible(By locator)
        {
            WebDriverWait wait = new WebDriverWait(SeleniumDriver.GetDriver(), TimeSpan.FromSeconds(TimeConstants.DEFAULT_10_SECONDS));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static bool IsSelected(By locator)
        {
            return IsSelected(locator, TimeConstants.DEFAULT_10_SECONDS);
        }

        public static bool IsSelected(By locator, int waitTimeInSecs)
        {
            WebDriverWait wait = new WebDriverWait(SeleniumDriver.GetDriver(), TimeSpan.FromSeconds(waitTimeInSecs));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Selected;
        }
    }
}
