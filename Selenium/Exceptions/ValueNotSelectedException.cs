using System;
using System.Runtime.Serialization;
using OpenQA.Selenium;

namespace Selenium.Exceptions
{
    [Serializable]
    public class ValueNotSelectedException : WebDriverException
    {
        public ValueNotSelectedException()
        {
        }

        public ValueNotSelectedException(string message) : base(message)
        {
        }

        protected ValueNotSelectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}