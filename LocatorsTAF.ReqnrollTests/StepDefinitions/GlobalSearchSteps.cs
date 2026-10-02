using LocatorsTAF.BusinessLayer.Pages;
using OpenQA.Selenium;
using Reqnroll;
using System;

namespace LocatorsTAF.ReqnrollTests.StepDefinitions;

[Binding]
public class GlobalSearchSteps(DriverContext driverContext)
{
    private readonly MainPage _mainPage = new(driverContext.DriverWrapper);
    private SearchResultPage _searchResultPage = null!;
    private IReadOnlyList<string>? _resultTexts;

    // Fetched once on first use, so the Then steps work in any order.
    private IReadOnlyList<string> ResultTexts =>
        _resultTexts ??= _searchResultPage.GetResultTexts();

    [Given("I am on the EPAM home page")]
    public void GivenIAmOnTheEPAMHomePage() => _mainPage.AcceptCookiesIfDisplayed();

    [When("I search for {string}")]
    public void WhenISearchFor(string searchText) =>
        _searchResultPage = _mainPage.PerformGlobalSearch(searchText);

    [Then("the search results should not be empty")]
    public void ThenTheSearchResultsShouldNotBeEmpty() =>
        Assert.That(ResultTexts, Is.Not.Empty, "No search results were found.");

    [Then("every search result should contain {string}")]
    public void ThenEverySearchResultShouldContain(string searchText)
    {
        var irrelevant = ResultTexts
            .Where(text => !text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Assert.That(irrelevant, Is.Empty, $"These results do not contain '{searchText}'");
    }
}