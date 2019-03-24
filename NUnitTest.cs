using System;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.Functions;

namespace Selenium
{
    public class NUnitTest
    {
        public NUnitTest()
        {
        }

        public static void Main(string[] args)
        {
            SeleniumDriver.SetDriver(new ChromeDriver());
            SeleniumDriver.GetDriver().Url = "http://www.google.com";

            Boolean isDisplayed = SeleniumDriver.GetDriver().FindElement(By.Id("gb")).Displayed;
            if (isDisplayed)
            {
                SeleniumDriver.GetDriver().Close();
            }

        }
    }
}
