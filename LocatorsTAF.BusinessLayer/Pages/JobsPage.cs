using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;

namespace LocatorsTAF.BusinessLayer.Pages;

public class JobsPage : BasePage
{
    private static readonly By Loader = By.CssSelector("div[class*='Preloader_fullSize']");
    private static readonly By ApplyButton = By.XPath("(//*[@id='cta_job_apply_unauthorized'])[last()]");
    private static readonly By CountryInput =
        By.CssSelector("[data-testid='country-dropdown'] input[role='combobox']");
    private static readonly By CountryMenu =
        By.CssSelector("[data-testid='country-dropdown'] [role='listbox']");
    private static readonly By SelectedCountry =
        By.CssSelector("[data-testid='country-dropdown'] [data-testid='dropdown-value']");
    private static readonly By SearchSubmitButton =
        By.CssSelector("button[name='submit_search_box_button']");

    private readonly IWebElementWrapper _countryInput;
    private readonly IWebElementWrapper _jobTitleInput;
    private readonly IWebElementWrapper _remoteCheckbox;
    private readonly IWebElementWrapper _searchButton;
    private readonly IWebElementWrapper _lastSearchResult;
    private readonly IWebElementWrapper _selectedCountry;
    private readonly IWebElementWrapper _countryMenu;


    public JobsPage(IWebDriverWrapper driver) : base(driver)
    {
        _countryInput = new WebElementWrapper(driver, CountryInput);
       
        _countryMenu = new WebElementWrapper(driver, CountryMenu);
        _jobTitleInput = new WebElementWrapper(driver, By.Name("search"));
        _remoteCheckbox = new WebElementWrapper(driver, By.XPath("//span[normalize-space()='Remote']"));
        _searchButton = new WebElementWrapper(driver, SearchSubmitButton);
        _lastSearchResult = new WebElementWrapper(driver, By.XPath("(//div[@data-testid='accordion-section-container'])[last()]"));
        _selectedCountry = new WebElementWrapper(driver, SelectedCountry);
    }

    public void SelectCountry(string country)
    {        
        _countryInput.ClearText();
        _countryInput.EnterText(country);

        //Driver.WaitUntilClickable(CountryMenu);

        _countryMenu
            .Child(By.XPath($".//div[@data-testid='dropdown-option'][normalize-space(.)='{country}']"))
            .Click();

        WaitForResultsToLoad();
    }

    public void EnterJobTitle(string jobTitle)
    {
        _jobTitleInput.ClearText();
        _jobTitleInput.EnterText(jobTitle);
    }

    public void ClickRemote()
    {        
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
        Driver.WaitUntilVisible(ApplyButton);
    }

    public string GetResultDescription() => _lastSearchResult.GetText();

    // JobsPage
    private void WaitForResultsToLoad() => Driver.WaitForTransientToDisappear(Loader, TimeSpan.FromSeconds(2));
}
