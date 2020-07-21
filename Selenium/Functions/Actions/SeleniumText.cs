using System;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.PageObject;
using Selenium.Utility;
using SeleniumExtras.WaitHelpers;

namespace Selenium.Functions.Actions
{
    public class SeleniumText : SeleniumSelection
    {
        private const string TextNotFound = "Text not found for object!";
        private static readonly TimeSpan Default5Seconds = TimeSpan.FromSeconds(5);

        protected static void Type(PageElement pageElement, string input)
        {
            pageElement.GoToFrame();
            var element =
                SeleniumUtility.WebDriverWait(ExpectedConditions.ElementToBeClickable(pageElement.Locator));
            element.Clear();
            element.SendKeys(input);

            Assert.AreEqual(input, ExtractText(pageElement, Default5Seconds));
        }

        protected static string ExtractText(PageElement pageElement, TimeSpan? maxWaitTime)
        {
            try
            {
                pageElement.GoToFrame();
                var element = SeleniumUtility.WebDriverWait(
                    ExpectedConditions.ElementIsVisible(pageElement.Locator),
                    maxWaitTime ?? Default5Seconds);

                var text = GetTextByType(pageElement.ElementType, element);

                if (text == null) throw new NotFoundException($"{TextNotFound} Element: {pageElement}");

                return text;
            }
            catch (Exception e)
            {
                throw new NotFoundException($"Error getting value: {e.StackTrace}");
            }
        }

        private static string GetTextByType(ElementType elementType, IWebElement element)
        {
            return elementType switch
            {
                ElementType.ATTRIBUTE_VALUE => element.GetAttribute("value"),
                ElementType.TEXT => element.Text,
                _ => throw new NotImplementedException($"Extracting text by {elementType} not implemented " +
                                                       $"in {typeof(SeleniumText).Name} | {MethodBase.GetCurrentMethod().Name}")
            };
        }
    }
}