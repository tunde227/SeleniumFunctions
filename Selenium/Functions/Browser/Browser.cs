using System;
using System.IO;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using Selenium.Extensions;

namespace Selenium.Functions.Browser
{
    public class Browser
    {
        private class DownloadDriver
        {
            private string _driverVersion = "";
            private string _driverLocation = "";
            private string _url = "";

            public DownloadDriver WithDriverVersion(string driverVersion)
            {
                _driverVersion = driverVersion.RequireNonNullOrEmpty();
                return this;
            }

            public DownloadDriver WithDownloadLocation(string downloadLocation)
            {
                if (Directory.Exists(downloadLocation))
                {
                    _driverLocation = downloadLocation.RequireNonNullOrEmpty();
                }
                else throw new FileNotFoundException($"{downloadLocation} does not exist.");

                return this;
            }

            private DownloadDriver WithUrl(string url)
            {
                _url = url;
                return this;
            }

            public string GetDriverLocation()
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(_url, _driverLocation);
                }

                return _driverLocation;
            }
        }


        public IWebDriver GetChromeDriver()
        {
            return new ChromeDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultChromeOptions());
        }

        public IWebDriver GetChromeDriver(string version, string location)
        {
            return new ChromeDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultChromeOptions());
        }

        public IWebDriver GetFireFoxDriver()
        {
            return new FirefoxDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultFirefoxOptions());
        }

        public IWebDriver GetFireFoxDriver(string version, string location)
        {
            return new FirefoxDriver(
                new DownloadDriver().WithDriverVersion(version).WithDownloadLocation(location).GetDriverLocation(),
                DefaultFirefoxOptions());
        }

        public IWebDriver GetEdgeDriver()
        {
            return new EdgeDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), DefaultEdgeOptions());
        }

        public IWebDriver GetEdgeDriver(string version, string location)
        {
            return new EdgeDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), DefaultEdgeOptions());
        }

        public IWebDriver GetInternetExlporerDriver()
        {
            return new InternetExplorerDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultIeOptions());
        }

        public IWebDriver GetInternetExlporerDriver(string version, string location)
        {
            return new InternetExplorerDriver(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultIeOptions());
        }


        private ChromeOptions DefaultChromeOptions()
        {
            ChromeOptions options = new ChromeOptions();
            Console.WriteLine(options.BrowserName);

            options.AddArguments("test-type");
            options.AddArguments("--always-authorize-plugins=true");
            options.AddArguments("chrome.switches", "--disable-extensions");
            options.AddArguments("--start-maximized");
            options.AddArguments("incognito");
            return options;
        }

        private FirefoxOptions DefaultFirefoxOptions()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.SetPreference("app.update.enabled", false);
            Console.WriteLine(options.BrowserName);

            return options;
        }

        private EdgeOptions DefaultEdgeOptions()
        {
            EdgeOptions options = new EdgeOptions();
            return options;
        }

        private InternetExplorerOptions DefaultIeOptions()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            return options;
        }

        private SafariOptions GetSafariOptions()
        {
            SafariOptions options = new SafariOptions();
            Console.WriteLine(options.BrowserName);

            return options;
        }
    }
}