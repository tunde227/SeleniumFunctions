using System;
using Selenium.Functions.Actions;

namespace Selenium.PageObject
{
    public partial class PageElement
    {
        public void Click(TimeSpan? maxWaitTime = null) => Click(this, maxWaitTime);
        public void DoubleClick(TimeSpan? maxWaitTime = null) => DoubleClick(this, maxWaitTime);
        public void ClickAndHold(TimeSpan? maxWaitTime = null) => ClickAndHold(this, maxWaitTime);
        public void RightClick(TimeSpan? maxWaitTime = null) => RightClick(this, maxWaitTime);
        public void ClickMultipleElements() => ClickMultipleElements(this);
        public void SendKeys(params string[] keys) => SendKeys(this, keys);
        public void SelectDropDownByVisibleText(string visibleText) => SelectDropDownByVisibleText(this, visibleText);
        public void SelectDropDownByValue(string value) => SelectDropDownByValue(this, value);
        public void MoveToElement(TimeSpan? maxWaitTime) => MoveToElement(this, maxWaitTime);
        public void Type(string input) => Type(this, input);
        public string GetText(TimeSpan? maxWaitTime) => ExtractText(this, maxWaitTime, ElementType);
        public bool IsElementVisible(TimeSpan? maxWaitTime = null) => IsElementVisible(this, maxWaitTime);
        public bool IsElementNotVisible(TimeSpan? maxWaitTime = null) => IsElementNotVisible(this, maxWaitTime);
        public bool IsElementSelected(TimeSpan? maxWaitTime = null) => IsElementSelected(this, maxWaitTime);
        public SeleniumJavascript JavaScript() => new SeleniumJavascript(this);
    }
}