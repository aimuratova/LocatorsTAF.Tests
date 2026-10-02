using LocatorsTAF.CoreLayer.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace LocatorsTAF.CoreLayer.Utilities;

public static class SettingsLoader
{
    public static TestSettings Load()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appSettings.json", optional: false)
            .AddEnvironmentVariables()   // env vars override the file
            .Build();

        // Flat keys keep env var names simple: BROWSER, APP_URL, HEADLESS, EXPLICIT_WAIT_SECONDS
        var browserText = configuration["BROWSER"] ?? configuration["Browser:Type"];
        if (!Enum.TryParse(browserText, ignoreCase: true, out BrowserType browser))
            throw new InvalidOperationException($"Invalid browser type: '{browserText}'");

        var appUrl = configuration["APP_URL"] ?? configuration["App:Url"];
        if (string.IsNullOrWhiteSpace(appUrl))
            throw new InvalidOperationException("App URL is missing (APP_URL or App:Url).");

        var apiBaseUrl = configuration["API_BASE_URL"] ?? configuration["ApiSettings:BaseUrl"];
        if (string.IsNullOrWhiteSpace(apiBaseUrl))
            throw new InvalidOperationException("API base URL is missing (API_BASE_URL or ApiSettings:BaseUrl).");


        return new TestSettings
        {
            BrowserType = browser,
            AppUrl = appUrl,
            ApiBaseUrl = apiBaseUrl,
            Headless = bool.TryParse(configuration["HEADLESS"] ?? configuration["Browser:Headless"], out var h) && h,
            ExplicitWaitSeconds = int.TryParse(
                configuration["EXPLICIT_WAIT_SECONDS"] ?? configuration["App:ExplicitWaitSeconds"], out var w) ? w : 10
        };
    }
}
