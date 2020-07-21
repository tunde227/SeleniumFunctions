using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Selenium.Functions;
using Selenium.Utility;

namespace Selenium.PageObject
{
    public sealed partial class PageElement : SeleniumUtility, IEquatable<PageElement>
    {
        public string Name { get; set; } = "";
        public By Locator { get; set; }
        public ElementType ElementType { get; set; }
        public Action Frame { get; set; }

        public bool Equals(PageElement other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, StringComparison.CurrentCultureIgnoreCase) &&
                   Equals(Locator, other.Locator) && ElementType == other.ElementType && Equals(Frame, other.Frame);
        }

        public void GoToFrame()
        {
            Frame?.Invoke();
        }

        public List<T> ToList<T>()
        {
            if (typeof(T) == typeof(IWebElement))
                return SeleniumDriver.Driver.FindElements(Locator).ToList() as List<T>;

            if (typeof(T) != typeof(PageElement))
                throw new NotSupportedException($"No Implementation for converting {typeof(T).Name} to List.");

            var pageElements = new List<PageElement>();

            for (var index = 1; index <= SeleniumDriver.Driver.FindElements(Locator).Count; index++)
                pageElements.Add(new PageElement
                {
                    Locator = ToXPath(Locator, index),
                    Name = $"{Name} {index}",
                    Frame = Frame,
                    ElementType = ElementType
                });

            return pageElements as List<T>;
        }

        public IWebElement ToWebElement()
        {
            return SeleniumDriver.Driver.FindElement(Locator);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PageElement) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Name) : 0;
                hashCode = (hashCode * 397) ^ (Locator != null ? Locator.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) ElementType;
                hashCode = (hashCode * 397) ^ (Frame != null ? Frame.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(PageElement left, PageElement right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PageElement left, PageElement right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}, {nameof(Locator)}: {Locator}, {nameof(ElementType)}: {ElementType}, {nameof(Frame)}: {Frame}";
        }
    }
}