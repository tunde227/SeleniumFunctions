using OpenQA.Selenium;

namespace Selenium.PageObject
{
    public class PageElement
    {
        internal string Key { get; set; }
        private By ElementLocation { get; set; }
        private ElementType ElementType { get; set; }

        public PageElement()
        {

        }
    }
}
