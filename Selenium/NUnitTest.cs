using System;
using System.IO;
using System.Reflection;
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
            test.Click();

            Console.WriteLine("TEST");
            Console.WriteLine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName);
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name);
        }
    }
}