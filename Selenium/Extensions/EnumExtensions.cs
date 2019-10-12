using System;
using System.IO;
using Selenium.Functions.Browser;

namespace Selenium.Extensions
{
    public static class EnumExtensions
    {
        public static void DownloadDriver(this Browsers browser, string version, string downloadLocation)
        {
            GetDriver.DriverBuilder().WithBrowser(browser)
                .WithDriverVersion(version).WithDownloadLocation(downloadLocation)
                .Build();
        }

        public static void DownloadDriverToProject(this Browsers browser, string version)
        {
            string downloadLocation = Directory.GetParent(Environment.CurrentDirectory).Parent?.FullName;
            GetDriver.DriverBuilder().WithBrowser(browser).WithDriverVersion(version)
                .WithDownloadLocation(downloadLocation)
                .Build();
        }
    }
}