using LocatorsTAF.BusinessLayer.Pages;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.ReqnrollTests.StepDefinitions
{
    [Binding]
    public class ServicesSteps
    {
        private readonly MainPage _mainPage;
        private ServicesPage _servicesPage;

        public ServicesSteps(DriverContext driverContext)
        {
            _mainPage = new MainPage(driverContext.DriverWrapper);
        }

        [Given("I open EPAM home page")]
        public void GivenIOpenEPAMHomePage()
        {
            _mainPage.AcceptCookiesIfDisplayed();
        }

        [When("I select {string}")]
        public void WhenISelect(string p0)
        {
            _servicesPage = _mainPage.NavigateToServices(p0);
        }

        [Then("page title should contain {string}")]
        public void ThenPageTitleShouldContain(string p0)
        {
            var pageTitle = _servicesPage.GetPageTitleInService();
            Assert.That(pageTitle, Contains.Substring(p0));
        }

        [Then("Our Related Expertise section is displayed")]
        public void ThenOurRelatedExpertiseSectionIsDisplayed()
        {
            var isTextFound = _servicesPage.IsServicesSearchStringIsFound();
            Assert.That(isTextFound, Is.True);
        }
    }
}
