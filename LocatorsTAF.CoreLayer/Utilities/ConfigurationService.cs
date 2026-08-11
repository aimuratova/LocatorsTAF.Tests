using LocatorsTAF.CoreLayer.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Utilities
{
    public static class ConfigurationService
    {
        public static string AppUrl { get; set; }
        public static BrowserType BrowserType { get; set; }
        public static int ImplicitWaitTime { get; set; }
                
        public static void LoadConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: false)
                .Build();

            var browser = configuration["Browser:Type"];

            if (Enum.TryParse(browser, true, out BrowserType parsedType))
            {
                BrowserType = parsedType;
            }
            else
            {
                throw new ArgumentException($"Invalid browser type: {browser}");
            }

            var appUrl = configuration["App:Url"];
            if (appUrl is null)
            {
                throw new ArgumentNullException("App:Url", "App:Url section is missing in appSettings.json");
            }
            AppUrl = appUrl;

            var implicitWaitTimeString = configuration["App:ImplicitWaitTime"];
            if (int.TryParse(implicitWaitTimeString, out int implicitWaitTime))
            {
                ImplicitWaitTime = implicitWaitTime;
            }
            else
            {
                throw new ArgumentException($"Invalid ImplicitWaitTime: {implicitWaitTimeString}");
            }
        }

        public static string GetConfigurationValue(string key)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: false)
                .Build();
            return configuration[key];
        }
    }
}
