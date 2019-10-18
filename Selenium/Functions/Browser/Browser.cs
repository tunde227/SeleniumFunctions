using OpenQA.Selenium;

namespace Selenium.Functions.Browser
{
    public enum Browser
    {
        CHROME,
        FIREFOX,
        SAFARI,
        EDGE
    }

    public abstract class BrowserOptions
    {
        public abstract DriverOptions Options();
    }
}