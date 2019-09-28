using System;
using Selenium.PageObject;
using Selenium.Utility;
using SeleniumExtras.WaitHelpers;

namespace Selenium.Functions.Actions
{
    public class SeleniumVisibility
    {
        private static readonly TimeSpan Default5Seconds = TimeSpan.FromSeconds(5);

        protected static bool IsElementVisible(PageElement pageElement, TimeSpan? maxWaitTime = null)
        {
            pageElement.GoToFrame();
            return SeleniumUtility
                .WebDriverWait(ExpectedConditions.ElementIsVisible(pageElement.Locator), maxWaitTime ?? Default5Seconds)
                .Displayed;
        }


        protected static bool IsElementNotVisible(PageElement pageElement, TimeSpan? maxWaitTime = null)
        {
            pageElement.GoToFrame();
            return SeleniumUtility.WebDriverWait(ExpectedConditions.InvisibilityOfElementLocated(pageElement.Locator),
                maxWaitTime ?? Default5Seconds);
        }

        protected static bool IsElementSelected(PageElement pageElement, TimeSpan? maxWaitTime = null)
        {
            pageElement.GoToFrame();
            return SeleniumUtility.WebDriverWait(ExpectedConditions.ElementToBeClickable(pageElement.Locator),
                maxWaitTime ?? Default5Seconds).Selected;
        }
    }
}