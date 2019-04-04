using System;
using OpenQA.Selenium;
using Selenium.Constants;
using Selenium.Functions;

namespace Selenium.PageObject
{
    public class PageElement
    {
        public By locator;

        public PageElement WithLocator(By locator)
        {
            this.locator = locator;
            return this;
        }

        public void Click()
        {
            SeleniumClick.Click(this.locator, TimeConstants.DEFAULT_2_SECONDS);
        }
        public string GetValue()
        {
            return SeleniumText.GetValue(this.locator, TimeConstants.DEFAULT_5_SECONDS);
        }
        public void SetValue(string input)
        {
            SeleniumText.Type(this.locator, input);
        }
        public bool IsElementVisible()
        {
            return SeleniumVisibility.IsElementVisible(this.locator, TimeConstants.DEFAULT_10_SECONDS);
        }

        public bool IsElementVisible(int waitTimeInSecs)
        {
            return SeleniumVisibility.IsElementVisible(this.locator, waitTimeInSecs);
        }

        public bool IsElementNotVisible(int waitTimeInSecs)
        {
            return SeleniumVisibility.IsElementNotVisible(this.locator, waitTimeInSecs);
        }

        public bool IsElementNotVisible()
        {
            return SeleniumVisibility.IsElementNotVisible(this.locator);
        }

        public void SetDropdownByVisibleText(string visibleTextOnDropdown)
        {
            SeleniumClick.SelectDropDownByVisibleText(this.locator, visibleTextOnDropdown);
        }
    }
}
