using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium.Functions.Browser
{
    public abstract class BrowserActions
    {
        private Browsers _browsers;

        public BrowserActions(Browsers browsers)
        {
            _browsers = browsers;
        }

        public string DriverLocation { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public abstract DriverOptions DriverOptions();


        public void Open(string url)
        {
            MaximizeWindow();
            SeleniumDriver.Driver = new RemoteWebDriver(DriverOptions());
            SeleniumDriver.Driver.Navigate().GoToUrl(url);
        }

        public void SwitchToTab(Tab tab)
        {
            bool isClosed = SeleniumDriver.Driver.WindowHandles.All(handle => !handle.Equals(tab.WindowHandle));
            if (isClosed)
            {
                tab.Open();
            }

            SeleniumDriver.Driver.SwitchTo().Window(tab.WindowHandle);
        }

        public void MaximizeWindow() => SeleniumDriver.Driver.Manage().Window.Maximize();

        public void MinimizeWindow() => SeleniumDriver.Driver.Manage().Window.Minimize();
        public static void Close() => SeleniumDriver.Driver.Close();
        public static void Quit() => SeleniumDriver.Driver.Quit();
    }
}