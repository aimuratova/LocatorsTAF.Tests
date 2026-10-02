using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LocatorsTAF.BusinessLayer.Pages;

public class InsightsPage : BasePage
{
    private const string ActiveSlide = "//div[contains(@class,'owl-item active')]";

    private readonly IWebElementWrapper _rightArrow;
    private readonly IWebElementWrapper _articleTitle;
    private readonly IWebElementWrapper _readMoreButton;
    private readonly IWebElementWrapper _readMoreArticleTitle;

    public InsightsPage(IWebDriverWrapper driver) : base(driver)
    {
        _rightArrow = new WebElementWrapper(driver, By.CssSelector("button.slider__right-arrow"));
        _articleTitle = new WebElementWrapper(driver, By.XPath(
            ActiveSlide + "//div[contains(@class,'text-image-slide-ui__parsys--2')]//span[contains(@class,'font-size-44')]"));
        _readMoreButton = new WebElementWrapper(driver, By.XPath(
            ActiveSlide + "//a[contains(@class,'slider-cta-link')]"));
        _readMoreArticleTitle = new WebElementWrapper(driver, By.TagName("h1"));
    }

    public void ClickRightArrow()
    {
        var previousTitle = _articleTitle.GetText();
        _rightArrow.Click();
        _articleTitle.WaitUntilTextIsNot(previousTitle);   // replaces Wait()
    }

    public string GetArticleTitle() => _articleTitle.GetText();

    public void ClickArticle()
    {
        var oldUrl = Driver.Url;
        _readMoreButton.Click();
        Driver.WaitUntilUrlChanges(oldUrl);
    }

    public string GetReadMoreArticleTitle() => _readMoreArticleTitle.GetText();
}
