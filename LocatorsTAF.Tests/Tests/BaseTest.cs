using LocatorsTAF.CoreLayer.Driver;
using LocatorsTAF.CoreLayer.Interfaces;
using LocatorsTAF.CoreLayer.Utilities;
using NUnit.Framework.Interfaces;

namespace LocatorsTAF.Tests.Tests;

public abstract class BaseTest
{
    private DriverManager _driverManager;
    private IScreenshotMakerService _screenshotMaker;

    protected ILoggingService Logger { get; private set; } = null!;
    protected IWebDriverWrapper DriverWrapper { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        Logger = new LoggerService();
        Logger.Info($"========== Started: {TestContext.CurrentContext.Test.FullName} ==========");

        _driverManager = new DriverManager(TestSetup.Settings);
        _driverManager.StartBrowser();

        DriverWrapper = new WebDriverWrapper(_driverManager.Current, Logger);
        _screenshotMaker = new ScreenshotMakerService(DriverWrapper);
        DriverWrapper.NavigateTo(TestSetup.Settings.AppUrl);
    }

    [TearDown]
    public void TearDown()
    {
        var result = TestContext.CurrentContext.Result;
        try
        {
            if (result.Outcome.Status == TestStatus.Failed)
            {
                Logger?.Error($"Test failed: {result.Message}");
                _screenshotMaker?.TakeScreenshot(TestContext.CurrentContext.Test.FullName);
            }
        }
        catch (Exception ex)
        {
            Logger?.Error($"Could not take screenshot: {ex.Message}");
        }
        finally
        {
            _driverManager?.QuitBrowser();
            Logger?.Info($"========== Finished: {result.Outcome.Status} ==========");
        }
    }
}
