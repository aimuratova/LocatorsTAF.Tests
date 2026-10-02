using LocatorsTAF.CoreLayer.Enums;
using LocatorsTAF.CoreLayer.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace LocatorsTAF.CoreLayer.Driver
{
    public static class DriverFactory
    {
        public static IWebDriver Create(TestSettings settings, string downloadDirectory) =>
            settings.BrowserType switch
            {
                BrowserType.Chrome => CreateChrome(settings, downloadDirectory),
                BrowserType.Firefox => CreateFirefox(settings),
                BrowserType.Edge => CreateEdge(settings),
                _ => throw new ArgumentOutOfRangeException(
                         nameof(settings.BrowserType), settings.BrowserType, "Browser not supported")
            };

        private static IWebDriver CreateChrome(TestSettings settings, string downloadDirectory)
        {
            var options = new ChromeOptions();
            options.AddArguments("--disable-notifications", "--disable-popup-blocking");

            if (settings.Headless)
                options.AddArguments("--headless=new", "--window-size=1920,1080");
            else
                options.AddArgument("--start-maximized");

            options.AddUserProfilePreference("download.default_directory", downloadDirectory);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options,
                                    TimeSpan.FromSeconds(30));
        }

        private static IWebDriver CreateFirefox(TestSettings settings)
        {
            var options = new FirefoxOptions();
            if (settings.Headless)
            {
                options.AddArgument("-headless");
                options.AddArguments("--width=1920", "--height=1080");
            }
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdge(TestSettings settings)
        {
            var options = new EdgeOptions();
            if (settings.Headless)
                options.AddArguments("--headless=new", "--window-size=1920,1080");
            else
                options.AddArgument("--start-maximized");
            return new EdgeDriver(options);
        }
    }
}
