using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;

namespace LocatorsTAF.BusinessLayer.Pages;

public class SearchResultPage : BasePage
{
    private static readonly By ResultItems =
        By.XPath("//article[contains(@class,'search-results__item')]");

    public SearchResultPage(IWebDriverWrapper driver) : base(driver)
    {
    }

    public IReadOnlyList<string> GetResultTexts() => Driver.GetDisplayedTexts(ResultItems);
}