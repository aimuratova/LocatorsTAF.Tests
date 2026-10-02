using LocatorsTAF.BusinessLayer.Pages;

namespace LocatorsTAF.Tests.Tests;

[Category("UI")]
public class GlobalSearchTests : BaseTest
{
    [TestCase("BLOCKCHAIN")]
    [TestCase("AI")]
    [TestCase("Automation")]
    public void GlobalSearch_ShouldReturnRelevantResults(string searchText)
    {
        var mainPage = new MainPage(DriverWrapper);
        mainPage.AcceptCookiesIfDisplayed();

        var resultTexts = mainPage
            .PerformGlobalSearch(searchText)
            .GetResultTexts();

        Assert.That(resultTexts, Is.Not.Empty, "No search results were found.");

        var irrelevant = resultTexts
            .Where(text => !text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Assert.That(irrelevant, Is.Empty,
            $"These results do not contain '{searchText}'");
    }
}