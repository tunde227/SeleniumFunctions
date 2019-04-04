using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Functions
{
    public static class SeleniumDriver
    {
        public static IWebDriver Driver;

        public static IWebDriver GetDriver() => Driver;

        public static void SetDriver(IWebDriver Driver)
        {
            SeleniumDriver.Driver = Driver;
        }

        public static void Open(string url)
        {
            SetDriver(new ChromeDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), GetOptions()));
            GetDriver().Manage().Window.Maximize();
            GetDriver().Navigate().GoToUrl(url);
        }

        public static void Close()
        {
            GetDriver().Close();
        }

        private static ChromeOptions GetOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("test-type");
            options.AddArguments("--always-authorize-plugins=true");
            options.AddArguments("chrome.switches", "--disable-extensions");
            options.AddArguments("--start-maximized");
            options.AddArguments("incognito");
            return options;
        }
    }
}
