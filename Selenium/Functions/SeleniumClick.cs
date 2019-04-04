using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Selenium.Constants;
using System;
using System.Threading;
using System.Linq;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Selenium.Functions
{
    public static class SeleniumClick
    {

        public static void Click(By locator, int waitInSecs)
        {
            WebDriverWait wait = new WebDriverWait(SeleniumDriver.GetDriver(), TimeSpan.FromSeconds(waitInSecs));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
        }

        public static void Click(By elementLocation) => Click(elementLocation, TimeConstants.DEFAULT_2_SECONDS);

        public static void ClickMultipleElements(By elementLocation)
        {
            foreach (IWebElement element in SeleniumDriver.GetDriver().FindElements(elementLocation))
            {
                WebDriverWait wait = new WebDriverWait(SeleniumDriver.GetDriver(), TimeSpan.FromSeconds(TimeConstants.DEFAULT_2_SECONDS));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
            }
        }

        public static void ClickAndHold(By locator, int waitInSecs)
        {
            IWebDriver webDriver = SeleniumDriver.GetDriver();
            Actions actions = new Actions(webDriver);
            IWebElement onElement = webDriver.FindElement(locator);

            actions.ClickAndHold(onElement).Build().Perform();
            Thread.Sleep(TimeSpan.FromSeconds(waitInSecs).Milliseconds);
            actions.Release(onElement).Build().Perform();
        }

        public static void DoubleClick(By locator) => DoubleClick(locator, TimeConstants.DEFAULT_2_SECONDS);

        public static void DoubleClick(By locator, int waitInSecs)
        {
            if (SeleniumVisibility.IsElementVisible(locator, waitInSecs))
            {
                IWebDriver webDriver = SeleniumDriver.GetDriver();
                Actions actions = new Actions(webDriver);
                actions.DoubleClick(webDriver.FindElement(locator)).Build().Perform();
            }
        }

        public static void SendKeys(By locator, params string[] keys)
        {
            IWebDriver webDriver = SeleniumDriver.GetDriver();
            Actions actions = new Actions(webDriver);
            foreach (string key in keys)
            {
                actions.SendKeys(webDriver.FindElement(locator), key);
                Thread.Sleep(1000);
            }
            actions.Build().Perform();
        }

        public static void SelectDropDownByVisibleText(By locator, string visibleText)
        {
            SelectElement dropdown = new SelectElement(SeleniumDriver.GetDriver().FindElement(locator));
            dropdown.SelectByText(visibleText);

            string dropdownText = dropdown.AllSelectedOptions.First(e => e.Selected).Text;
            if (!dropdownText.Equals(visibleText))
            {
                Console.WriteLine("Dropdown was not set to value: " + visibleText);
            }
        }
    }
}