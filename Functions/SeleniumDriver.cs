using System;
using OpenQA.Selenium;

namespace Selenium.Functions
{
    public static class SeleniumDriver
    {
        public static IWebDriver Driver;

        public static IWebDriver GetDriver() => SeleniumDriver.Driver;

        public static void SetDriver(IWebDriver Driver)
        {
            SeleniumDriver.Driver = Driver;
        }
    }
}
