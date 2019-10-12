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
        public static Driver DriverBuilder() => new Driver();


        public class Driver
        {
            private string Version { get; set; } = "";
            private string Location { get; set; } = "";
            private string Url { get; set; } = "";
            private Browsers Browser { get; set; }

            public Driver WithBrowser(Browsers browser)
            {
                Browser = browser;
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
                return Browser switch
                {
                    CHROME => ("https://chromedriver.storage.googleapis.com/" +
                               $"index.html?path={Version}/{DriverZip()}"),
                    FIREFOX => ("https://github.com/mozilla/geckodriver/" +
                                $"releases/download/v{Version}/{DriverZip()}"),
                    EDGE => $"https://msedgewebdriverstorage.z22.web.core.windows.net/?prefix={Version}/{DriverZip()}",
                    SAFARI => null,
                    _ => throw new Exception($"{Browser.ToString()} is not " + "accounted for.  Unable to build URL.")
                };
            }

            private string DriverZip()
            {
                return Browser switch
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
                {
                    return "edgedriver_win64.zip";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && !Environment.Is64BitOperatingSystem)
                {
                    return "edgedriver_win32.zip";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "edgedriver_mac64.zip";
                }

                throw new Exception($"{RuntimeInformation.OSDescription} " +
                                    "Operating System is not accounted for.");
            }

            private string FirefoxDriverZip()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && Environment.Is64BitOperatingSystem)
                {
                    return $"geckodriver-v{Version}-win64.zip";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    && !Environment.Is64BitOperatingSystem)
                {
                    return $"geckodriver-v{Version}-win32.zip";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return $"geckodriver-v{Version}-macos.tar.gz";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    && Environment.Is64BitOperatingSystem)
                {
                    return $"geckodriver-v{Version}-linux64.tar.gz";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    && !Environment.Is64BitOperatingSystem)
                {
                    return $"geckodriver-v{Version}-linux32.tar.gz";
                }

                throw new Exception($"{RuntimeInformation.OSDescription} " +
                                    "Operating System is not accounted for.");
            }

            private static string ChromeDriverZip()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return "chromedriver_win32.zip";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return "chromedriver_mac64.zip";
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return "chromedriver_linux64.zip";
                }

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
                    $"\\{Browser.ToString().ToLower()}_{Version}"));
                return this;
            }
        }
    }
}