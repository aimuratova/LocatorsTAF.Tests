using LocatorsTAF.BusinessLayer.Pages;
using Reqnroll;
using System;

namespace LocatorsTAF.ReqnrollTests.StepDefinitions;

[Binding]
public class CareerSearchSteps
{
    private readonly MainPage _mainPage;
    private CareersPage _careersPage = null!;
    private JobsPage _jobsPage = null!;

    public CareerSearchSteps(DriverContext driverContext)
    {
        _mainPage = new MainPage(driverContext.DriverWrapper);
    }

    [When("I navigate to Careers")]
    public void WhenINavigateToCareers()
    {
        _mainPage.AcceptCookiesIfDisplayed();
        _careersPage = _mainPage.NavigateToCareersPage();
    }

    [When("I start a job search")]
    public void WhenIStartAJobSearch()
    {
        _jobsPage = _careersPage.NavigateToJobsPage();
        _jobsPage.AcceptCookiesIfDisplayed();   // different domain: separate consent banner
    }

    [When("I select {string} as the country")]
    public void WhenISelectAsTheCountry(string country) => _jobsPage.SelectCountry(country);

    [When("I enter {string} as the job title")]
    public void WhenIEnterAsTheJobTitle(string jobTitle) => _jobsPage.EnterJobTitle(jobTitle);

    [When("I filter by remote vacancies")]
    public void WhenIFilterByRemoteVacancies() => _jobsPage.ClickRemote();

    [When("I submit the search")]
    public void WhenISubmitTheSearch() => _jobsPage.Search();

    [When("I open the last search result")]
    public void WhenIOpenTheLastSearchResult() => _jobsPage.OpenLastSearchResult();

    [Then("the last search result should contain {string}")]
    public void ThenTheLastSearchResultShouldContain(string jobTitle)
    {
        Assert.That(_jobsPage.GetResultDescription(), Does.Contain(jobTitle).IgnoreCase,
            $"Opened result should mention '{jobTitle}'");
    }
}



