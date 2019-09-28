using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Extensions;
using Selenium.Functions;
using Selenium.Functions.Actions;

namespace Selenium.Utility
{
    public class SeleniumUtility : SeleniumText
    {
        private static readonly TimeSpan Default5Seconds = TimeSpan.FromSeconds(5);

        public static TResult WebDriverWait<TResult>(Func<IWebDriver, TResult> condition, TimeSpan? waitInSecs = null)
        {
            WebDriverWait wait = new WebDriverWait(SeleniumDriver.Driver, waitInSecs ?? Default5Seconds);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait.Until(condition.Invoke);
        }

        protected static By ToXPath(By locator, int index = 1)
        {
            string locatorType = locator.ToString().SubstringBefore(": ").Strip("By.");
            string element = locator.ToString().SubstringAfter(": ");

            switch (locatorType)
            {
                case "ID":
                    return By.XPath($"(//*[@id='{element}'])[{index}]");
                case "Name":
                    return By.XPath($"(//*[@name='{element}'])[{index}]");
                case "TagName":
                    return By.XPath($"(//{element})[{index}]");
                case "LinkText":
                    return By.XPath($"(//a[text()='{element}'])[{index}]");
                case "PartialLinkText":
                    return By.XPath($"(//a[contains(text(), '{element}')])[{index}]");
                case "ClassName":
                    return By.XPath($"(//*[@class='{element}'])[{index}]");
                default:
                    throw new NotImplementedException(
                        $"{locatorType} is not implemented in {MethodBase.GetCurrentMethod().Name}");
            }
        }
    }
}