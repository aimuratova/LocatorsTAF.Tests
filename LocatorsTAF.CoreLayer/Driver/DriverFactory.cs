using LocatorsTAF.CoreLayer.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Driver
{
    public static class DriverFactory
    {

        public static IWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions options = new();
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-notifications");
                    options.AddArgument("--disable-popup-blocking");

                    options.AddArgument(@"user-data-dir=C:\Users\Myrzaliyev\Desktop\Epam\Work\Selenium");

                    options.AddArgument("--profile-directory=Default");

                    var downloadFolder = Path.Combine(Path.GetTempPath(), "Downloads");
                    options.AddUserProfilePreference("download.default_directory", downloadFolder);
                    options.AddUserProfilePreference("download.prompt_for_download", false);
                    options.AddUserProfilePreference("download.directory_upgrade", true);
                    options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

                    return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(30));
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                case BrowserType.Edge:
                    return new EdgeDriver();
                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, "Browser not supported");
            }
        }

    }
}
