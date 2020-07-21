using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium.Functions.Browser
{
    public abstract class BrowserActions
    {
        private Browser _browser;

        public BrowserActions(Browser browser)
        {
            _browser = browser;
        }

        public abstract DriverOptions DriverOptions();

        public void Open(string url)
        {
            MaximizeWindow();
            SeleniumDriver.Driver = new RemoteWebDriver(DriverOptions());
            SeleniumDriver.Driver.Navigate().GoToUrl(url);
        }

        public void SwitchToTab(Tab tab)
        {
            var isClosed = SeleniumDriver.Driver.WindowHandles.All(handle => !handle.Equals(tab.WindowHandle));
            if (isClosed) tab.Open();

            SeleniumDriver.Driver.SwitchTo().Window(tab.WindowHandle);
        }

        public void MaximizeWindow()
        {
            SeleniumDriver.Driver.Manage().Window.Maximize();
        }

        public void MinimizeWindow()
        {
            SeleniumDriver.Driver.Manage().Window.Minimize();
        }

        public static void Close()
        {
            SeleniumDriver.Driver.Close();
        }

        public static void Quit()
        {
            SeleniumDriver.Driver.Quit();
        }
    }
}