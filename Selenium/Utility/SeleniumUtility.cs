using System;
using System.Reflection;
using Core.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Functions;
using Selenium.Functions.Actions;

namespace Selenium.Utility
{
    public class SeleniumUtility : SeleniumText
    {
        private static readonly TimeSpan Default5Seconds = TimeSpan.FromSeconds(5);

        public static TResult WebDriverWait<TResult>(Func<IWebDriver, TResult> condition, TimeSpan? waitInSecs = null)
        {
            var wait = new WebDriverWait(SeleniumDriver.Driver, waitInSecs ?? Default5Seconds);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait.Until(condition.Invoke);
        }

        protected static By ToXPath(By locator, int index = 1)
        {
            string locatorType = locator.ToString().SubstringBefore(": ").Strip("By.");
            string element = locator.ToString().SubstringAfter(": ");

            return locatorType switch
            {
                "ID" => By.XPath($"(//*[@id='{element}'])[{index}]"),
                "Name" => By.XPath($"(//*[@name='{element}'])[{index}]"),
                "TagName" => By.XPath($"(//{element})[{index}]"),
                "LinkText" => By.XPath($"(//a[text()='{element}'])[{index}]"),
                "PartialLinkText" => By.XPath($"(//a[contains(text(), '{element}')])[{index}]"),
                "ClassName" => By.XPath($"(//*[@class='{element}'])[{index}]"),
                _ => throw new NotImplementedException(
                    $"{locatorType} is not implemented in {MethodBase.GetCurrentMethod().Name}")
            };
        }
    }
}