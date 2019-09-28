using System;
using OpenQA.Selenium;
using Selenium.Utility;

namespace Selenium.PageObject
{
    public class PageElement : SeleniumUtility, IEquatable<PageElement>
    {
        public string Name { get; set; } = "";
        public By Locator { get; set; }
        public ElementType ElementType { get; set; }
        public Action Frame { get; set; } = null;

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
        public void GoToFrame() => Frame?.Invoke();

        public bool Equals(PageElement other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, StringComparison.CurrentCultureIgnoreCase) &&
                   Equals(Locator, other.Locator) && ElementType == other.ElementType && Equals(Frame, other.Frame);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PageElement) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Name) : 0);
                hashCode = (hashCode * 397) ^ (Locator != null ? Locator.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) ElementType;
                hashCode = (hashCode * 397) ^ (Frame != null ? Frame.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(PageElement left, PageElement right) => Equals(left, right);

        public static bool operator !=(PageElement left, PageElement right) => !Equals(left, right);

        public override string ToString() =>
            $"{nameof(Name)}: {Name}, {nameof(Locator)}: {Locator}, {nameof(ElementType)}: {ElementType}, {nameof(Frame)}: {Frame}";
    }
}