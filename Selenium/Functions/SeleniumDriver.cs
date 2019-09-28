using System.Collections.Generic;
using OpenQA.Selenium;
using Selenium.Functions.Browser;

namespace Selenium.Functions
{
    public static class SeleniumDriver
    {
        public static IWebDriver Driver { get; set; }

        public static Dictionary<string, IWebDriver> Drivers { get; set; }

        public static BrowserActions Actions() => new BrowserActions();
    }
}