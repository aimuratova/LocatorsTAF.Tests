using LocatorsTAF.CoreLayer.Interfaces;
namespace LocatorsTAF.CoreLayer.Utilities;

public class ScreenshotMakerService : IScreenshotMakerService
{
    private static readonly string ScreenshotsFolder =
        Path.Combine(AppContext.BaseDirectory, "Screenshots");

    private readonly IWebDriverWrapper _driver;

    public ScreenshotMakerService(IWebDriverWrapper driver)
    {
        _driver = driver;
    }

    public string TakeScreenshot(string testName)
    {
        Directory.CreateDirectory(ScreenshotsFolder);

        var fileName = $"{Sanitize(testName)}_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";
        var path = Path.Combine(ScreenshotsFolder, fileName);

        File.WriteAllBytes(path, _driver.TakeScreenshot());
        return path;
    }

    private static string Sanitize(string name)
    {
        var invalid = Path.GetInvalidFileNameChars();
        var clean = new string(name.Select(c => invalid.Contains(c) || c == ' ' ? '_' : c).ToArray());
        return clean.Length > 80 ? clean[..80] : clean;
    }
}

