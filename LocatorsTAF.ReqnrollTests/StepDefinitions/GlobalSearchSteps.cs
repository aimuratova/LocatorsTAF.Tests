using System;
using Reqnroll;

namespace LocatorsTAF.ReqnrollTests.StepDefinitions
{
    [Binding]
    public class GlobalSearchSteps
    {
        [Given("I am on the EPAM home page")]
        public void GivenIAmOnTheEPAMHomePage()
        {
            throw new PendingStepException();
        }

        [When("I search for {string}")]
        public void WhenISearchFor(string bLOCKCHAIN)
        {
            throw new PendingStepException();
        }

        [Then("every search result should contain {string}")]
        public void ThenEverySearchResultShouldContain(string bLOCKCHAIN)
        {
            throw new PendingStepException();
        }

        [Then("the search results should not be empty")]
        public void ThenTheSearchResultsShouldNotBeEmpty()
        {
            throw new PendingStepException();
        }

    }
}
