using System;
using System.Reflection;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using Selenium.Extensions;
using Selenium.PageObject;
using Selenium.Utility;

namespace Selenium.Functions.Actions
{
    public class SeleniumJavascript : IEquatable<SeleniumJavascript>
    {
        private const string Id = "document.getElementById(\"@\")";
        private const string Css = "document.querySelectorAll(\"@\")";
        private const string Class = "document.getElementsByClassName(\"@\")";
        private const string Name = "document.getElementsByName(\"@\")";
        private const string TagName = "document.getElementsByTagName(\"@\")";

        private const string XPath = "(function getElementByXpath(\"@\") { return document.evaluate(path, document, " +
                                     "null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue; })";

        private static readonly ILog Logger = LogManager.GetLogger(typeof(SeleniumJavascript));
        private static readonly TimeSpan Default5Seconds = TimeSpan.FromSeconds(5);

        public SeleniumJavascript(PageElement element)
        {
            Element = element;
            By(element);
        }

        private string JavaScript { get; set; }
        private string Locator { get; set; }
        private bool HasIndex { get; set; }
        private PageElement Element { get; }

        public bool Equals(SeleniumJavascript other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(JavaScript, other.JavaScript) && string.Equals(Locator, other.Locator) &&
                   HasIndex == other.HasIndex && Equals(Element, other.Element);
        }

        private void By(PageElement element)
        {
            string type = element.Locator.ToString().SubstringBefore(":").Strip("By.");
            string locator = element.Locator.ToString().SubstringAfter(": ");

            switch (type.ToLowerInvariant())
            {
                case "id":
                    ToJavaScript(Id, locator, false);
                    break;
                case "css":
                    ToJavaScript(Css, locator, true);
                    break;
                case "class":
                    ToJavaScript(Class, locator, true);
                    break;
                case "tagname":
                    ToJavaScript(TagName, locator, true);
                    break;
                case "name":
                    ToJavaScript(Name, locator, true);
                    break;
                case "xpath":
                    ToJavaScript(XPath, locator, false);
                    break;
                case "linktext":
                    ToJavaScript(XPath, $"//a[text()='{locator}']", false);
                    break;
                case "partiallinktext":
                    ToJavaScript(XPath, $"a[contains(text(), '{locator}')]", false);
                    break;
                default:
                    throw new InvalidCastException($"Not able to case {type} to JavaScript.");
            }
        }

        private void ToJavaScript(string method, string locator, bool hasIndex)
        {
            JavaScript = method;
            Locator = locator;
            HasIndex = hasIndex;
        }

        private string AddIndex(int index = 0) => HasIndex ? $"[{index}]" : "";

        public void Focus(int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.focus()";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public void Blur(int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.blur()";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public void Type(string input, int index = 0, TimeSpan? maxWaitTime = null)
        {
            Clear();
            var script = ElementType.INPUT_ATTRIBUTE_VALUE.Equals(Element.ElementType)
                ? $"{WaitForElementToDisplay(index, maxWaitTime)}.setAttribute(value, {input})"
                : $"{WaitForElementToDisplay(index, maxWaitTime)}.innerHTML = {input}";

            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public void Clear(int index = 0) => Type("");

        public void Click(int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.click()";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public string GetAttribute(string attribute, int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.getAttribute({attribute})";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<string>(script);
        }

        public string GetAttributeNode(string attribute, int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.getAttributeNode({attribute}).value";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<string>(script);
        }

        public void RemoveAttribute(string attribute, int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.removeAttribute({attribute})";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public bool HasAttribute(string attribute, int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.hasAttribute({attribute})";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<bool>(script);
        }

        public bool HasAttributes(int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.hasAttributes()";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<bool>(script);
        }

        public bool ContainsElement(string element, int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.contains({element})";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<bool>(script);
        }

        public void Normalize(int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.normalize()";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public string TextContent(int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.textContent";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<string>(script);
        }

        public string Text(int index = 0, TimeSpan? maxWaitTime = null)
        {
            if (Element.ElementType == ElementType.INPUT_ATTRIBUTE_VALUE ||
                Element.ElementType == ElementType.ATTRIBUTE_VALUE)
            {
                return GetAttribute("value", index);
            }

            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.innerHTML";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<string>(script);
        }

        public void SetContentEditable(bool isEditable = true, int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.contentEditable = {isEditable}";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public bool IsContentEditable(int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.contentEditable";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<bool>(script);
        }

        public void ScrollIntoView(bool top, int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.scrollIntoView({top})";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public void ScrollIntoView(int index = 0, TimeSpan? maxWaitTime = null)
        {
            string script = $"{WaitForElementToDisplay(index, maxWaitTime)}.scrollIntoView()";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            SeleniumDriver.Driver.ExecuteJavaScript(script);
        }

        public string ReadyState(int index = 0)
        {
            string script = "document.readyState";
            Logger.Debug($"{MethodBase.GetCurrentMethod().Name} -> Script: {script}");
            return SeleniumDriver.Driver.ExecuteJavaScript<string>(script);
        }

        private string WaitForElementToDisplay(int index, TimeSpan? maxWaitTime = null)
        {
            string locator = $"{this}{AddIndex(index)}";
            if (SeleniumUtility.WebDriverWait(driver => driver.ExecuteJavaScript<string>(locator).IsBlank(),
                maxWaitTime ?? Default5Seconds))
            {
                throw new NotFoundException($"Did not find element within {maxWaitTime ?? Default5Seconds}: {locator}");
            }

            return locator;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SeleniumJavascript) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (JavaScript != null ? JavaScript.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Locator != null ? Locator.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ HasIndex.GetHashCode();
                hashCode = (hashCode * 397) ^ (Element != null ? Element.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(SeleniumJavascript left, SeleniumJavascript right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SeleniumJavascript left, SeleniumJavascript right)
        {
            return !Equals(left, right);
        }

        public override string ToString() => JavaScript.Replace("@", Locator);
    }
}