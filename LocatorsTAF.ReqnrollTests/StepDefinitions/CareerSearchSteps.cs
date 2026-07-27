using LocatorsTAF.BusinessLayer.Pages;
using Reqnroll;
using System;

namespace LocatorsTAF.ReqnrollTests.StepDefinitions
{
    [Binding]
    public class CareerSearchSteps(DriverContext driverContext)
    {
        private readonly MainPage _mainPage = new MainPage(driverContext.DriverWrapper);
        private CareersPage _careersPage;
        private JobsPage _jobsPage;

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
            _jobsPage.AcceptCookiesIfDisplayed();
        }

        [When("I select {string} as the country")]
        public void WhenISelectAsTheCountry(string country)
        {
            _jobsPage.SelectCountry(country);
            _jobsPage.WaitForLoader();
        }

        [When("I enter {string} as the job title")]
        public void WhenIEnterAsTheJobTitle(string jobTitle)
        {
            _jobsPage.EnterJobTitle(jobTitle);
        }

        [When("I filter by remote vacancies")]
        public void WhenIFilterByRemoteVacancies()
        {
            _jobsPage.ClickRemote();
            _jobsPage.WaitForLoader();
        }

        [When("I submit the search")]
        public void WhenISubmitTheSearch()
        {
            _jobsPage.ClickSearchButton();
            _jobsPage.WaitForLoader();
        }

        [Then("the last search result should contain {string}")]
        public void ThenTheLastSearchResultShouldContain(string jobTitle)
        {
            _jobsPage.LastSearchResultClick();

            var resultDescription = _jobsPage.GetResultDescription();
            Assert.That(resultDescription.ToLower(), Does.Contain(jobTitle.ToLower()));
        }

    }
}
