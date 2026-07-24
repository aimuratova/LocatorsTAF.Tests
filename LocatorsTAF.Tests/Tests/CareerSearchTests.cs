using LocatorsTAF.BusinessLayer.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.Tests.Tests
{
    public class CareerSearchTests : BaseTest
    {
        [TestCase("Java", "Poland")]
        [TestCase("C#", "Poland")]
        [TestCase("Python", "Germany")]
        public void SearchWithPage(string jobTitle, string country)
        {
            Logger.Info($"CareerSearchTests test with parameters: jobTitle {jobTitle} and country {country}");
            var mainPage = new MainPage(DriverWrapper);
            mainPage.AcceptCookiesIfDisplayed();

            var careersPage = mainPage.NavigateToCareersPage();

            var jobsPage = careersPage.NavigateToJobsPage();
            jobsPage.AcceptCookiesIfDisplayed();

            jobsPage.SelectCountry(country);
            jobsPage.WaitForLoader();
            jobsPage.EnterJobTitle(jobTitle);
            jobsPage.ClickRemote();
            jobsPage.WaitForLoader();

            jobsPage.ClickSearchButton();

            jobsPage.WaitForLoader();

            jobsPage.LastSearchResultClick();

            var resultDescription = jobsPage.GetResultDescription();

            Assert.That(resultDescription.ToLower(), Does.Contain(jobTitle.ToLower()));
            Logger.Info($"CareerSearchTests test end");
        }
               
    }
}
