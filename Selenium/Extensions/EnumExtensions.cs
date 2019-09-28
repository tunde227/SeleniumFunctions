using System;
using System.IO;
using Selenium.Functions.Browser;

namespace Selenium.Extensions
{
    public static class EnumExtensions
    {
        public static void DownloadDriver(this Browsers browser, string version,
            string downloadLocation)
        {
            GetDriver.Download(browser, version, downloadLocation);
        }

        public static void DownloadDriverToProject(this Browsers browser, string version)
        {
            GetDriver.Download(browser, version, Directory.GetParent(Environment.CurrentDirectory).Parent.FullName);
        }
    }
}