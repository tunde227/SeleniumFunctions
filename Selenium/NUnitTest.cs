using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.PageObject;

namespace Selenium
{
    [TestFixture]
    public class NUnitTest
    {
        public NUnitTest()
        {
        }

        [TestCase]
        public void Testts()
        {
            //SeleniumDriver.Driver(new ChromeDriver());
            PageElement test = new PageElement
            {
                Locator = By.Id(""),
                Name = "Test",
                ElementType = ElementType.TEXT
            };

            test.ToList<PageElement>().ForEach(Console.WriteLine);

//            List<IWebElement> webElements = test.ToList<IWebElement>().Where(e => e.Selected).ToList();
//            Console.WriteLine(webElements);
        }
    }
}