using System;
using log4net;
using OpenQA.Selenium.Support.Extensions;

namespace Selenium.Functions.Browser
{
    public class Tab : IEquatable<Tab>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Tab));

        public Tab()
        {
        }

        public Tab(string name, string windowHandle, string url)
        {
            Name = name;
            WindowHandle = windowHandle;
            Url = url;
        }

        public string Name { get; set; }
        public string WindowHandle { get; set; }
        public string Url { get; set; }

        public bool Equals(Tab other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(WindowHandle, other.WindowHandle, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(Url, other.Url, StringComparison.CurrentCultureIgnoreCase);
        }

        public void Open()
        {
            SeleniumDriver.Driver.ExecuteJavaScript("window.open();");
            SeleniumDriver.Driver.Navigate().GoToUrl(Url);
        }

        public void Close()
        {
            try
            {
                SeleniumDriver.Driver.SwitchTo().Window(WindowHandle).Close();
            }
            catch (Exception e)
            {
                Logger.Warn("Will try to close using JavaScript.\n Error closing tab " +
                            $"{e.Message}{e.StackTrace}");
                SeleniumDriver.Driver.ExecuteJavaScript("window.close();");
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Tab) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Name) : 0);
                hashCode = (hashCode * 397) ^ (WindowHandle != null
                               ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(WindowHandle)
                               : 0);
                hashCode = (hashCode * 397) ^
                           (Url != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Url) : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Tab left, Tab right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tab left, Tab right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(WindowHandle)}: {WindowHandle}, {nameof(Url)}: {Url}";
        }
    }
}