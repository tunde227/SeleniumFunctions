﻿using Selenium.Functions.Browser;

namespace Selenium.Extensions
{
    public static class EnumExtensions
    {
        public static Driver DownloadDriver(this Browser browser, string version, string downloadLocation) =>
            new Driver.DriverBuilder().WithBrowser(browser).WithDriverVersion(version)
                .WithDownloadLocation(downloadLocation).Build();

        public static Driver DownloadDriverToSolution(this Browser browser, string version) =>
            new Driver.DriverBuilder().WithBrowser(browser).WithDriverVersion(version).Build();
    }
}