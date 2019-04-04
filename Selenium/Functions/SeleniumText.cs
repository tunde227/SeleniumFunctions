using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Constants;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Selenium.Functions
{
    public static class SeleniumText
    {
        private const string _textNotFound = "Text not found for object!";
        
        public static void Type(By locator, string input)
        {
            WebDriverWait wait = new WebDriverWait(SeleniumDriver.GetDriver(), TimeSpan.FromSeconds(TimeConstants.DEFAULT_5_SECONDS));
            SeleniumClick.Click(locator, TimeConstants.DEFAULT_10_SECONDS);
            wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Clear();
            wait.Until(ExpectedConditions.ElementToBeClickable(locator)).SendKeys(input);
            string text = GetValue(locator, TimeConstants.DEFAULT_5_SECONDS);
            if (text == null || text.Equals(_textNotFound))
            {
                Console.WriteLine(text);
            }
        }

        public static string GetValue(By locator, int waitInSecs)
        {
            string text = null;
            try
            {
                if (SeleniumVisibility.IsElementVisible(locator, waitInSecs))
                {
                    text = SeleniumDriver.GetDriver().FindElement(locator).Text;
                }
                else
                {
                    text = _textNotFound;
                    Console.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting value: " + e.StackTrace);
            }
            return text;
        }
    }
}
