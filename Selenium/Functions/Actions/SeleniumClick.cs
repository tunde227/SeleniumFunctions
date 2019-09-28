using System;
using System.Threading;
using OpenQA.Selenium;
using Selenium.PageObject;
using Selenium.Utility;
using SeleniumExtras.WaitHelpers;

namespace Selenium.Functions.Actions
{
    public class SeleniumClick : SeleniumVisibility
    {
        private static readonly TimeSpan Default5Seconds = TimeSpan.FromSeconds(5);

        protected static void Click(PageElement pageElement, TimeSpan? maxWaitTime = null)
        {
            pageElement.GoToFrame();
            SeleniumUtility
                .WebDriverWait(
                    ExpectedConditions.ElementToBeClickable(SeleniumDriver.Driver.FindElement(pageElement.Locator)),
                    maxWaitTime).Click();
        }


        protected static void ClickMultipleElements(PageElement pageElement)
        {
            pageElement.GoToFrame();
            foreach (IWebElement element in SeleniumDriver.Driver.FindElements(pageElement.Locator))
            {
                SeleniumUtility.WebDriverWait(ExpectedConditions.ElementToBeClickable(element), Default5Seconds)
                    .Click();
            }
        }

        protected static void ClickAndHold(PageElement pageElement, TimeSpan? maxWaitTime = null)
        {
            pageElement.GoToFrame();
            OpenQA.Selenium.Interactions.Actions actions =
                new OpenQA.Selenium.Interactions.Actions(SeleniumDriver.Driver);
            IWebElement onElement = SeleniumDriver.Driver.FindElement(pageElement.Locator);

            actions.ClickAndHold(onElement).Build().Perform();
            Thread.Sleep(maxWaitTime ?? Default5Seconds);
            actions.Release(onElement).Build().Perform();
        }

        protected static void DoubleClick(PageElement pageElement, TimeSpan? maxWaitTime = null)
        {
            if (IsElementVisible(pageElement, maxWaitTime ?? Default5Seconds))
            {
                pageElement.GoToFrame();
                new OpenQA.Selenium.Interactions.Actions(SeleniumDriver.Driver)
                    .DoubleClick(SeleniumDriver.Driver.FindElement(pageElement.Locator)).Build()
                    .Perform();
            }
        }

        protected static void SendKeys(PageElement pageElement, params string[] keys)
        {
            OpenQA.Selenium.Interactions.Actions actions =
                new OpenQA.Selenium.Interactions.Actions(SeleniumDriver.Driver);
            foreach (string key in keys)
            {
                pageElement.GoToFrame();
                actions.SendKeys(SeleniumDriver.Driver.FindElement(pageElement.Locator), key).Build().Perform();
                Thread.Sleep(1000);
            }
        }

        protected static void RightClick(PageElement pageElement, TimeSpan? maxWaitTime = null)
        {
            pageElement.GoToFrame();
            OpenQA.Selenium.Interactions.Actions actions =
                new OpenQA.Selenium.Interactions.Actions(SeleniumDriver.Driver);
            actions.ContextClick(SeleniumUtility.WebDriverWait(ExpectedConditions.ElementIsVisible(pageElement.Locator),
                maxWaitTime ?? Default5Seconds)).Build().Perform();
        }
    }
}