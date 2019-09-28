using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Functions.Browser
{
    public class BrowserActions
    {
        public void Open(params IWebDriver[] drivers)
        {
            foreach (IWebDriver driver in drivers)
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("");
            }
        }


        public void Close(params IWebDriver[] drivers) => drivers.ToList().ForEach(driver => driver.Close());

        public void Quit(params IWebDriver[] drivers) => drivers.ToList().ForEach(driver => driver.Quit());

        public void OpenTab(params Tab[] tabs)
        {
        }

        public void CloseTab(params Tab[] tabs)
        {
        }

        public void SwitchTo(Tab tab)
        {
        }

        public void SwitchTo(IWebDriver driver)
        {
        }

        public void MaximizeWindow(IWebDriver driver) => driver.Manage().Window.Maximize();

        public void MinimizeWindow(IWebDriver driver) => driver.Manage().Window.Minimize();

        public static void Open(string url)
        {
            SeleniumDriver.Driver =
                new ChromeDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), null);
            SeleniumDriver.Driver.Manage().Window.Maximize();
            SeleniumDriver.Driver.Navigate().GoToUrl(url);
        }

        public static void Close()
        {
            SeleniumDriver.Driver.Close();
        }
    }
}