using System.Linq;

namespace Selenium.Functions.Browser
{
    public class BrowserActions
    {
        public void Open(string url)
        {
            MaximizeWindow();
            SeleniumDriver.Driver.Navigate().GoToUrl(url);
        }

        public void SwitchToTab(Tab tab)
        {
            bool isClosed = !SeleniumDriver.Driver.WindowHandles.Any(handle => handle.Equals(tab.WindowHandle));
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