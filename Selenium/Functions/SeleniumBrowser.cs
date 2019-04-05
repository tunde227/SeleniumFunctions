using System;
using System.Net;
using Selenium.Utility;

namespace Selenium.Functions
{
    public class SeleniumBrowser
    {
        internal string driverVersion = "";
        internal string downloadLocation = "";
        internal string url = "";

        public SeleniumBrowser()
        {
        }

        public SeleniumBrowser WithDriverVersion(string driverVersion)
        {
            this.driverVersion = driverVersion;
            return this;
        }

        public SeleniumBrowser WithDownloadLocation(string downloadLocation)
        {
            this.downloadLocation = downloadLocation;
            return this;
        }

        public SeleniumBrowser WithDriverDownloadUrl(string url)
        {
            this.url = url;
            return this;
        }

        public static void GetChromeDriver()
        {
        }

        public static void GetFireFoxDriver()
        {

        }

        public static void GetSafariDriver()
        {

        }

        public static void GetEdgeDriver()
        {

        }

        private void DownloadDriver(string url, string fileLocation)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("http://example.com/file/song/a.mpeg", "a.mpeg");
            }
        }

    }
}
