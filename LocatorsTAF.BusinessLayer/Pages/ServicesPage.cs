using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;

namespace LocatorsTAF.BusinessLayer.Pages;

public class ServicesPage : BasePage
{
    private static readonly By RelatedExpertiseHeading =
        By.XPath("//h2[contains(normalize-space(.),'Our Related Expertise')]");

    private readonly IWebElementWrapper _pageHeading;

    public ServicesPage(IWebDriverWrapper driver) : base(driver)
    {
        _pageHeading = new WebElementWrapper(driver, By.CssSelector("#main h1"));   // verify in DevTools
    }

    public string GetPageTitle() => _pageHeading.GetText();

    public bool IsRelatedExpertiseSectionDisplayed() =>
        Driver.GetDisplayedTexts(RelatedExpertiseHeading, TimeSpan.FromSeconds(5)).Count > 0;
}