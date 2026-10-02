using LocatorsTAF.BusinessLayer.Pages;
using Reqnroll;

namespace LocatorsTAF.ReqnrollTests.StepDefinitions;

[Binding]
public class ServicesSteps(DriverContext driverContext)
{
    private readonly MainPage _mainPage = new(driverContext.DriverWrapper);
    private ServicesPage _servicesPage = null!;

    [When("I select {string} from the Services menu")]
    public void WhenISelectFromTheServicesMenu(string serviceName) =>
        _servicesPage = _mainPage.NavigateToServices(serviceName);
        
    [Then("page title should contain {string}")]
    public void ThenPageTitleShouldContain(string expectedText) =>
    Assert.That(_servicesPage.GetPageTitle(), Does.Contain(expectedText).IgnoreCase,
        "Service page title should contain the selected service name");

    [Then("Our Related Expertise section is displayed")]
    public void ThenOurRelatedExpertiseSectionIsDisplayed() =>
        Assert.That(_servicesPage.IsRelatedExpertiseSectionDisplayed(), Is.True,
            "'Our Related Expertise' section is not displayed");
}