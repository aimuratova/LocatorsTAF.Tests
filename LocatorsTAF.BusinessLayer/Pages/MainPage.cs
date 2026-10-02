using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;

namespace LocatorsTAF.BusinessLayer.Pages;

public class MainPage : BasePage
{
    private static readonly By ServicesMenuItem =
        By.XPath("//li[contains(@class,'top-navigation__item')][.//a[@href='/services']]");

    private readonly IWebElementWrapper _careersLink;
    private readonly IWebElementWrapper _searchMagnifier;
    private readonly IWebElementWrapper _searchInput;
    private readonly IWebElementWrapper _findButton;
    private readonly IWebElementWrapper _pdfDownloadLink;
    private readonly IWebElementWrapper _insightLink;
    private readonly IWebElementWrapper _servicesMenu;

    public const string CodeOfConductFileName = "Code_of_Ethical_Conduct.pdf";

    public MainPage(IWebDriverWrapper driver) : base(driver)
    {
        _careersLink = new WebElementWrapper(driver, By.CssSelector("a.top-navigation__item-link[href='/careers']")); // verify href
        _searchMagnifier = new WebElementWrapper(driver, By.CssSelector("button[class*='search']"));
        _searchInput = new WebElementWrapper(driver, By.XPath("//input[@type='search']"));
        _findButton = new WebElementWrapper(driver, By.XPath("//button[.//span[normalize-space()='Find']]"));
        _pdfDownloadLink = new WebElementWrapper(driver, By.XPath($"//a[contains(@href, '{CodeOfConductFileName}')]"));
        _insightLink = new WebElementWrapper(driver, By.CssSelector("a.top-navigation__item-link[href='/insights']"));
        _servicesMenu = new WebElementWrapper(driver, ServicesMenuItem);
    }

    public InsightsPage NavigateToInsightsPage()
    {
        _insightLink.Click();
        return new InsightsPage(Driver);
    }

    public CareersPage NavigateToCareersPage()
    {
        _careersLink.Click();
        return new CareersPage(Driver);
    }

    public SearchResultPage PerformGlobalSearch(string searchText)
    {
        _searchMagnifier.Click();
        _searchInput.ClearText();
        _searchInput.EnterText(searchText);
        _findButton.Click();
        return new SearchResultPage(Driver);
    }

    public void ClickToDownloadFile()
    {
        _pdfDownloadLink.ScrollIntoView();
        _pdfDownloadLink.Click();
    }

    public ServicesPage NavigateToServices(string serviceName)
    {
        _servicesMenu.Hover();

        // Built here because the locator depends on the parameter.
        new WebElementWrapper(Driver,
                By.XPath($"//div[contains(@class,'top-navigation__flyout')]//a[normalize-space()='{serviceName}']"))
            .Click();

        return new ServicesPage(Driver);
    }
}
