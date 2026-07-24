using LocatorsTAF.CoreLayer.Driver;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;
namespace LocatorsTAF.CoreLayer.Utilities
{
    public class ScreenshotMakerService : IScreenshotMakerService
    {
        private readonly string _screenshotsFolder;
        private readonly IWebDriverWrapper _webDriverWrapper;

        public ScreenshotMakerService(IWebDriverWrapper webDriverWrapper)
        {
            _screenshotsFolder = Path.Combine(AppContext.BaseDirectory, "Screenshots");
            Directory.CreateDirectory(_screenshotsFolder);

            _webDriverWrapper = webDriverWrapper;
        }
                
        public string TakeScreenshot()
        {
            var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";
            var driver = _webDriverWrapper.GetWebDriver();

            if (driver is not ITakesScreenshot screenshotDriver)
            {
                throw new InvalidOperationException(
                    "Current driver does not support screenshots.");
            }

            var screenshot = screenshotDriver.GetScreenshot();

            var filePath = Path.Combine(_screenshotsFolder, fileName);

            screenshot.SaveAsFile(filePath);

            return filePath;
        }

    }
}
