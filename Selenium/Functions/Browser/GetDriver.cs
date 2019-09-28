using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using Selenium.Extensions;
using static Selenium.Functions.Browser.Browsers;

namespace Selenium.Functions.Browser
{
    public static class GetDriver
    {
        public static Driver Download(Browsers browser, string version,
            string downloadLocation) => new Driver().Browser(browser)
            .WithDriverVersion(version).WithDownloadLocation(downloadLocation)
            .Build();

        public class Driver
        {
            public string Version { get; set; } = "";
            public string Location { get; set; } = "";
            public string Url { get; set; } = "";
            private Browsers _browser;

            public Driver Browser(Browsers browser)
            {
                _browser = browser;
                return this;
            }

            public Driver WithDriverVersion(string driverVersion)
            {
                Version = driverVersion.RequireNonNullOrEmpty();
                return this;
            }

            public Driver WithDownloadLocation(string downloadLocation)
            {
                if (Directory.Exists(downloadLocation))
                {
                    Location = downloadLocation.RequireNonNullOrEmpty();
                }
                else
                    throw new FileNotFoundException($"{downloadLocation} " +
                                                    "does not exist.");

                return this;
            }

            private string GetUrl()
            {
                switch (_browser)
                {
                    case CHROME:
                        return $"https://chromedriver.storage.googleapis.com/" +
                               $"index.html?path={Version}/{DriverZip()}";
                    case FIREFOX:
                        return $"https://github.com/mozilla/geckodriver/" +
                               $"releases/download/v{Version}/{DriverZip()}";
                    case IE:
                        return "";
                    case EDGE:
                        return "";
                    default:
                        throw new Exception($"{_browser.ToString()} is not " +
                                            "accounted for.  Unable to build URL.");
                }
            }

            private string DriverZip()
            {
                switch (_browser)
                {
                    case CHROME:
                        return ChromeDriverZip();
                    case FIREFOX:
                        return FirefoxDriverZip();
                    case IE:
                        return "";
                    case EDGE:
                        return "";
                    default:
                        return "";
                }
            }

            private string FirefoxDriverZip()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && Environment.Is64BitOperatingSystem)
                {
                    return $"geckodriver-v{Version}-win64.zip";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                         && !Environment.Is64BitOperatingSystem)
                {
                    return $"geckodriver-v{Version}-win32.zip";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return $"geckodriver-v{Version}-macos.tar.gz";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                         && Environment.Is64BitOperatingSystem)
                {
                    return $"geckodriver-v{Version}-linux64.tar.gz";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                         && !Environment.Is64BitOperatingSystem)
                {
                    return $"geckodriver-v{Version}-linux32.tar.gz";
                }
                else
                    throw new Exception($"{RuntimeInformation.OSDescription} " +
                                        "Operating System is not accounted for.");
            }

            private static string ChromeDriverZip()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return "chromedriver_win32.zip";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "chromedriver_mac64.zip";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return "chromedriver_linux64.zip";
                }
                else
                    throw new Exception($"{RuntimeInformation.OSDescription} " +
                                        "Operating System is not accounted for.");
            }

            public Driver Build()
            {
                Url = GetUrl();
                using (var client = new WebClient())
                {
                    client.DownloadFile(Url, Location);
                }

                ZipFile.ExtractToDirectory(Location, Path.Combine(Location,
                    $"\\{_browser.ToString().ToLower()}_{Version}"));
                return this;
            }
        }
    }
}