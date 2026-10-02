using LocatorsTAF.CoreLayer.Driver;
using LocatorsTAF.CoreLayer.Interfaces;
using LocatorsTAF.CoreLayer.Utilities;
using Reqnroll;

namespace LocatorsTAF.ReqnrollTests.Hooks;

[Binding]
public class Hooks
{
    private static TestSettings _settings = null!;

    private readonly DriverContext _driverContext;
    private readonly ScenarioContext _scenarioContext;

    private DriverManager? _driverManager;
    private ILoggingService? _logger;
    private IScreenshotMakerService? _screenshotMaker;

    public Hooks(DriverContext driverContext, ScenarioContext scenarioContext)
    {
        _driverContext = driverContext;
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        LoggingConfigurator.Configure();
        _settings = SettingsLoader.Load();
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _logger = new LoggerService();
        _logger.Info($"========== Started: {_scenarioContext.ScenarioInfo.Title} ==========");

        _driverManager = new DriverManager(_settings);
        _driverManager.StartBrowser();

        var driverWrapper = new WebDriverWrapper(_driverManager.Current, _logger);
        driverWrapper.NavigateTo(_settings.AppUrl);   // remove if a Given step already navigates

        _screenshotMaker = new ScreenshotMakerService(driverWrapper);
        _driverContext.DriverWrapper = driverWrapper;
        _driverContext.Logger = _logger;
        _driverContext.DownloadDirectory = _driverManager.DownloadDirectory;
    }

    [AfterScenario]
    public void AfterScenario()
    {
        try
        {
            if (_scenarioContext.TestError is not null)
            {
                _logger?.Error($"Scenario failed: {_scenarioContext.TestError.Message}");
                _screenshotMaker?.TakeScreenshot(_scenarioContext.ScenarioInfo.Title);
            }
        }
        catch (Exception ex)
        {
            _logger?.Error($"Could not take screenshot: {ex.Message}");
        }
        finally
        {
            _driverManager?.QuitBrowser();
            _logger?.Info("========== Finished ==========");
        }
    }
}
