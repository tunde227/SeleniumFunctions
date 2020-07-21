using System;
using OpenQA.Selenium;
using Selenium.Utility;
using SeleniumExtras.WaitHelpers;

namespace Selenium.Functions.Actions
{
    public class SeleniumFrames
    {
        public static void SwitchToMainFrame()
        {
            SeleniumDriver.Driver.SwitchTo().DefaultContent();
        }

        public static void SwitchToParentFrame()
        {
            SeleniumDriver.Driver.SwitchTo().ParentFrame();
        }

        public static IWebDriver SwitchToFrame(By locator, TimeSpan? maxWaitTime)
        {
            return SeleniumUtility.WebDriverWait(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator),
                maxWaitTime);
        }

        public static IWebDriver SwitchToFrame(string name, TimeSpan? maxWaitTime)
        {
            return SeleniumUtility.WebDriverWait(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(name), maxWaitTime);
        }

        public static void SwitchToFrame(int index)
        {
            SeleniumDriver.Driver.SwitchTo().Frame(index);
        }
    }
}