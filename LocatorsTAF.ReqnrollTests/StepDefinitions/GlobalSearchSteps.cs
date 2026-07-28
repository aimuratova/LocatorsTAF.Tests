using LocatorsTAF.BusinessLayer.Pages;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace LocatorsTAF.ReqnrollTests.StepDefinitions
{
    [Binding]
    public class GlobalSearchSteps(DriverContext driverContext)
    {
        private readonly MainPage _mainPage = new MainPage(driverContext.DriverWrapper);
        private SearchResultPage _searchResultPage;
        private IWebElement[] _resultLinks;

        [Given("I am on the EPAM home page")]
        public void GivenIAmOnTheEPAMHomePage()
        {
            _mainPage.AcceptCookiesIfDisplayed();
        }

        [When("I search for {string}")]
        public void WhenISearchFor(string searchText)
        {
            _searchResultPage = _mainPage.PerformGlobalSearch(searchText);
        }

        [Then("every search result should contain {string}")]
        public void ThenEverySearchResultShouldContain(string searchText)
        {
            _resultLinks = _searchResultPage.GetResultLinks();
            Assert.That(_resultLinks.All(link => link.Text.ToLower().Contains(searchText.ToLower())),
                $"Not all search results contain the search text '{searchText}'.");

        }

        [Then("the search results should not be empty")]
        public void ThenTheSearchResultsShouldNotBeEmpty()
        {
            Assert.That(_resultLinks, Is.Not.Empty, "No search results were found.");
        }

    }
}
