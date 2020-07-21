using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Core.Extensions;
using static Selenium.Functions.Browser.Browser;

namespace Selenium.Functions.Browser
{
    public class Driver
    {
        public string Version { get; protected internal set; } = "";
        public string Location { get; protected internal set; } = DefaultLocation();
        public string Url { get; protected internal set; } = "";
        public Browser Browser { get; protected internal set; }

        private static string DefaultLocation()
        {
            var solutionRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent;

            var driversDirectory =
                (solutionRoot?.GetDirectories() ?? throw new InvalidOperationException()).FirstOrDefault(dir =>
                    dir.Name.EqualsIgnoreCase("Drivers")) ??
                Directory.CreateDirectory($"{solutionRoot.FullName}/Drivers");

            return driversDirectory.FullName;
        }

        public class DriverBuilder
        {
            private readonly Driver _driver = new Driver();

            public DriverBuilder WithBrowser(Browser browser)
            {
                _driver.Browser = browser;
                return this;
            }

            public DriverBuilder WithDriverVersion(string driverVersion)
            {
                _driver.Version = driverVersion.RequireNonNullOrEmpty();
                return this;
            }

            public DriverBuilder WithDownloadLocation(string downloadLocation)
            {
                if (Directory.Exists(downloadLocation))
                    _driver.Location = downloadLocation.RequireNonNullOrEmpty();
                else
                    throw new FileNotFoundException($"{downloadLocation} " +
                                                    "does not exist.");

                return this;
            }

            private string GetUrl()
            {
                return _driver.Browser switch
                {
                    CHROME => ("https://chromedriver.storage.googleapis.com/" +
                               $"index.html?path={_driver.Version}/{DriverZip()}"),
                    FIREFOX => ("https://github.com/mozilla/geckodriver/" +
                                $"releases/download/v{_driver.Version}/{DriverZip()}"),
                    EDGE => "https://msedgewebdriverstorage.z22.web.core.windows.net/" +
                            $"?prefix={_driver.Version}/{DriverZip()}",
                    SAFARI => null,
                    _ => throw new Exception($"{_driver.Browser.ToString()} is not " +
                                             "accounted for.  Unable to build URL.")
                };
            }

            private string DriverZip()
            {
                return _driver.Browser switch
                {
                    CHROME => ChromeDriverZip(),
                    FIREFOX => FirefoxDriverZip(),
                    EDGE => EdgeDriverZip(),
                    SAFARI => null,
                    _ => ""
                };
            }

            private static string EdgeDriverZip()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && Environment.Is64BitOperatingSystem)
                    return "edgedriver_win64.zip";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && !Environment.Is64BitOperatingSystem)
                    return "edgedriver_win32.zip";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "edgedriver_mac64.zip";

                throw new Exception($"{RuntimeInformation.OSDescription} " +
                                    "Operating System is not accounted for.");
            }

            private string FirefoxDriverZip()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && Environment.Is64BitOperatingSystem)
                    return $"geckodriver-v{_driver.Version}-win64.zip";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && !Environment.Is64BitOperatingSystem)
                    return $"geckodriver-v{_driver.Version}-win32.zip";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return $"geckodriver-v{_driver.Version}-macos.tar.gz";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    && Environment.Is64BitOperatingSystem)
                    return $"geckodriver-v{_driver.Version}-linux64.tar.gz";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    && !Environment.Is64BitOperatingSystem)
                    return $"geckodriver-v{_driver.Version}-linux32.tar.gz";

                throw new Exception($"{RuntimeInformation.OSDescription} " +
                                    "Operating System is not accounted for.");
            }

            private static string ChromeDriverZip()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "chromedriver_win32.zip";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "chromedriver_mac64.zip";

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return "chromedriver_linux64.zip";

                throw new Exception($"{RuntimeInformation.OSDescription} " +
                                    "Operating System is not accounted for.");
            }

            public Driver Build()
            {
                _driver.Url = GetUrl();
                using (var client = new WebClient())
                {
                    client.DownloadFile(_driver.Url, _driver.Location);
                }

                ZipFile.ExtractToDirectory(_driver.Location, Path.Combine(_driver.Location,
                    $"\\{_driver.Browser.ToString().ToLower()}_{_driver.Version}"));
                return _driver;
            }
        }
    }
}