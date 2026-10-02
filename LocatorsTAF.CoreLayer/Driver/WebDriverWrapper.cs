using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace LocatorsTAF.CoreLayer.Driver;

public class WebDriverWrapper : IWebDriverWrapper
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

    private readonly IWebDriver _driver;
    private readonly ILoggingService _logger;

    public WebDriverWrapper(IWebDriver driver, ILoggingService logger)
    {
        _driver = driver;
        _logger = logger;
    }

    public string Url => _driver.Url;
    public T WaitFor<T>(Func<IWebDriver, T> condition, TimeSpan? timeout = null) => CreateWait(timeout).Until(condition);
    public void WaitUntilUrlChanges(string oldUrl) => WaitFor(d => d.Url != oldUrl);
    public void MoveToElement(IWebElement element) => new Actions(_driver).MoveToElement(element).Perform();
    public void NavigateTo(string url)
    {
        _logger.Info($"Navigate to {url}");
        _driver.Navigate().GoToUrl(url);
    }

    public IWebElement FindElement(By by, TimeSpan? timeout = null)
    {
        return CreateWait(timeout).Until(d => d.FindElement(by));
    }

    public IReadOnlyCollection<IWebElement> FindElements(By by) =>
        _driver.FindElements(by);

    public IWebElement WaitUntilClickable(By by, TimeSpan? timeout = null)
    {
        return CreateWait(timeout).Until(d =>
        {
            var element = d.FindElement(by);
            return element.Displayed && element.Enabled ? element : null;
        });
    }

    public IWebElement WaitUntilVisible(By by, TimeSpan? timeout = null)
    {
        return CreateWait(timeout).Until(d =>
        {
            var element = d.FindElement(by);
            return element.Displayed && element.Location.Y > 0 
            && element.Size.Height > 0 ? element : null;
        });
    }

    public void WaitUntilInvisible(By by, TimeSpan? timeout = null)
    {
        CreateWait(timeout).Until(d => !d.FindElements(by).Any(e => e.Displayed));
    }

    public void ExecuteScript(string script, IWebElement element) =>
        ((IJavaScriptExecutor)_driver).ExecuteScript(script, element);

    public IReadOnlyList<string> GetDisplayedTexts(By by, TimeSpan? timeout = null)
    {
        try
        {
            return CreateWait(timeout).Until(d =>
            {
                var texts = d.FindElements(by)
                    .Where(e => e.Displayed)
                    .Select(e => e.Text)
                    .ToList();
                return texts.Count > 0 ? texts : null!;   // keep waiting until something is shown
            });
        }
        catch (WebDriverTimeoutException)
        {
            return Array.Empty<string>();   // nothing appeared: let the test assert with its own message
        }
    }

    private WebDriverWait CreateWait(TimeSpan? timeout)
    {
        var wait = new WebDriverWait(_driver, timeout ?? DefaultTimeout);
        wait.IgnoreExceptionTypes(
            typeof(NoSuchElementException),
            typeof(StaleElementReferenceException),
            typeof(ElementClickInterceptedException),    // overlay in the way: retry until it's gone
            typeof(ElementNotInteractableException));
        return wait;
    }

    public IWebElement? TryWaitUntilClickable(By by, TimeSpan timeout)
    {
        try
        {
            return WaitUntilClickable(by, timeout);
        }
        catch (WebDriverTimeoutException)
        {
            _logger.Info($"Element not found or not clickable within {timeout.TotalSeconds}s: {by}");
            return null;
        }
    }
            
    public void WaitForTransientToDisappear(By locator, TimeSpan appearTimeout)
    {
        try
        {
            WaitFor(d => d.FindElements(locator).Any(e => e.Displayed), appearTimeout);
        }
        catch (WebDriverTimeoutException)
        {
            // Never appeared (fast response): nothing to wait for.
        }

        WaitUntilInvisible(locator);
    }

    public byte[] TakeScreenshot()
    {
        if (_driver is not ITakesScreenshot screenshotDriver)
            throw new InvalidOperationException("Current driver does not support screenshots.");

        return screenshotDriver.GetScreenshot().AsByteArray;
    }
}
