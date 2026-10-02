using LocatorsTAF.BusinessLayer.Pages;

namespace LocatorsTAF.Tests.Tests;

[Category("UI")]
public class CarouselArticleTest : BaseTest
{
    [Test]
    public void CarouselArticleShouldMatchOpenedArticle()
    {
        var mainPage = new MainPage(DriverWrapper);
        mainPage.AcceptCookiesIfDisplayed();

        var insightsPage = mainPage.NavigateToInsightsPage();
        insightsPage.ClickRightArrow();
        insightsPage.ClickRightArrow();  

        var previewTitle = insightsPage.GetArticleTitle();

        insightsPage.ClickArticle();

        var articleTitle = insightsPage.GetReadMoreArticleTitle();

        Assert.Multiple(() =>
        {
            Assert.That(articleTitle, Is.Not.Empty, "Opened article has no title");
            Assert.That(previewTitle, Does.Contain(articleTitle).IgnoreCase,
                "Carousel title should contain the opened article's title");
        });
    }
}
