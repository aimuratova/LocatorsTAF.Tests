using LocatorsTAF.BusinessLayer.Pages;

namespace LocatorsTAF.Tests.Tests;

[Category("UI")]
public class CareerSearchTests : BaseTest
{
    [TestCase("Java", "Poland")]
    [TestCase("C#", "Poland")]
    [TestCase("Python", "Germany")]
    public void SearchJobs_ByTitleAndCountry_ResultMentionsTitle(string jobTitle, string country)
    {
        var mainPage = new MainPage(DriverWrapper);
        mainPage.AcceptCookiesIfDisplayed();

        var jobsPage = mainPage
            .NavigateToCareersPage()
            .NavigateToJobsPage();

        jobsPage.SelectCountry(country);
        jobsPage.EnterJobTitle(jobTitle);
        jobsPage.ClickRemote();
        jobsPage.Search();
        jobsPage.OpenLastSearchResult();

        Assert.That(jobsPage.GetResultDescription(), Does.Contain(jobTitle).IgnoreCase,
            $"Opened result should mention '{jobTitle}'");
    }
}
