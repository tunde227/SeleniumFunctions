using OpenQA.Selenium;

namespace Selenium.PageObject
{
    public class PageElement
    {
        private string Key;
        private By ElementLocation;
        private ElementType ElementType;

        public PageElement()
        {

        }

        public string GetKey()
        {
            return this.Key;
        }

        public void SetKey(string Key)
        {
            this.Key = Key;
        }

        public ElementType GetElementType()
        {
            return this.ElementType;
        }

        public void SetElementType(ElementType ElementType)
        {
            this.ElementType = ElementType;
        }

        public By GetElementLocation()
        {
            return this.ElementLocation;
        }
 
        public void SetElementLocation(By ElementLocation)
        {
            this.ElementLocation = ElementLocation;
        }

    }
}
