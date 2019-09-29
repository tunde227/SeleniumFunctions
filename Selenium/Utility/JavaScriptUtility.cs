using OpenQA.Selenium;

namespace Selenium.Utility
{
    public static class JavaScriptUtility
    {
        public static T ExecuteScript<T>(this IWebDriver driver, string script)
        {
            return (T) (driver as IJavaScriptExecutor)?.ExecuteScript(script);
        }

        public static T ExecuteScript<T>(this IWebDriver driver, string script, IWebElement element)
        {
            return (T) (driver as IJavaScriptExecutor)?.ExecuteScript(script, element);
        }

        public static T ExecuteScript<T>(this IWebDriver driver, string script, By locator)
        {
            return (T) (driver as IJavaScriptExecutor)?.ExecuteScript(script, driver.FindElement(locator));
        }
    }
}