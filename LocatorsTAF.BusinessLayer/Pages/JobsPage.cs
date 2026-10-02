using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;

namespace LocatorsTAF.BusinessLayer.Pages;

public class JobsPage : BasePage
{
    private static readonly By Loader = By.CssSelector("div[class*='Preloader_fullSize']");
    private static readonly By ApplyButton = By.XPath("(//*[@id='cta_job_apply_unauthorized'])[last()]");

    private readonly IWebElementWrapper _countryInput;
    private readonly IWebElementWrapper _countryDropdown;
    private readonly IWebElementWrapper _jobTitleInput;
    private readonly IWebElementWrapper _remoteCheckbox;
    private readonly IWebElementWrapper _searchButton;
    private readonly IWebElementWrapper _lastSearchResult;

    public JobsPage(IWebDriverWrapper driver) : base(driver)
    {
        _countryInput = new WebElementWrapper(driver, By.XPath("//input[@aria-label='Choose your country']"));
        _countryDropdown = new WebElementWrapper(driver, By.XPath("//div[contains(@class, 'dropdown__menu')]"));
        _jobTitleInput = new WebElementWrapper(driver, By.Name("search"));
        _remoteCheckbox = new WebElementWrapper(driver, By.XPath("//span[normalize-space()='Remote']"));
        _searchButton = new WebElementWrapper(driver, By.CssSelector("button[type='submit']"));
        _lastSearchResult = new WebElementWrapper(driver, By.XPath("(//div[@data-testid='accordion-section-container'])[last()]"));
    }

    public void SelectCountry(string country)
    {
        _countryInput.ClearText();
        _countryInput.EnterText(country);

        _countryDropdown.Child(By.XPath($".//div[@data-testid='dropdown-option'][normalize-space(.)='{country}']")).Click();

        WaitForResultsToLoad();
    }

    public void EnterJobTitle(string jobTitle)
    {
        _jobTitleInput.ClearText();
        _jobTitleInput.EnterText(jobTitle);
    }

    public void ClickRemote()
    {
        // Verify the real checked state in DevTools and replace this check accordingly.
        // A <span> never reports Selected = true.
        _remoteCheckbox.Click();
        WaitForResultsToLoad();
    }

    public void Search()
    {
        _searchButton.Click();
        Driver.WaitUntilInvisible(Loader);
        WaitForResultsToLoad();
    }

    public void OpenLastSearchResult()
    {
        _lastSearchResult.Click();
        Driver.WaitUntilClickable(ApplyButton);
    }

    public string GetResultDescription() => _lastSearchResult.GetText();

    // JobsPage
    private void WaitForResultsToLoad() => Driver.WaitForTransientToDisappear(Loader, TimeSpan.FromSeconds(2));
}
