using System;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using Selenium.Exceptions;
using Selenium.PageObject;
using Selenium.Utility;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Selenium.Functions.Actions
{
    public class SeleniumSelection : SeleniumClick
    {
        private static readonly TimeSpan Default5Seconds = TimeSpan.FromSeconds(5);

        protected static void SelectDropDownByVisibleText(PageElement pageElement, string visibleText)
        {
            pageElement.GoToFrame();
            SelectElement dropdown = new SelectElement(SeleniumDriver.Driver.FindElement(pageElement.Locator));
            dropdown.SelectByText(visibleText);

            string dropdownText = dropdown.AllSelectedOptions.First(e => e.Selected).Text;
            if (!dropdownText.Equals(visibleText))
            {
                throw new ValueNotSelectedException($"Dropdown was not set to value: {dropdownText}");
            }
        }

        protected static void SelectDropDownByValue(PageElement pageElement, string value)
        {
            pageElement.GoToFrame();
            SelectElement dropdown = new SelectElement(SeleniumDriver.Driver.FindElement(pageElement.Locator));
            dropdown.SelectByValue(value);

            string dropdownValue = dropdown.AllSelectedOptions.First(e => e.Selected).GetAttribute("value");
            if (!dropdownValue.Equals(value, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ValueNotSelectedException($"Dropdown was not set to value: {dropdownValue}");
            }
        }

        protected static void MoveToElement(PageElement pageElement, TimeSpan? maxWaitTime = null)
        {
            pageElement.GoToFrame();
            OpenQA.Selenium.Interactions.Actions action =
                new OpenQA.Selenium.Interactions.Actions(SeleniumDriver.Driver);
            action.MoveToElement(SeleniumUtility.WebDriverWait(ExpectedConditions.ElementIsVisible(pageElement.Locator),
                maxWaitTime ?? Default5Seconds)).Build().Perform();
        }
    }
}