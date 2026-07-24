using LocatorsTAF.BusinessLayer.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.Tests.Tests
{
    public class GlobalSearchTests : BaseTest
    {
        
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void GlobalSearch_ShouldReturnRelevantResults(string searchText)
        {
            Logger.Info($"GlobalSearchTests test with parameters: searchText {searchText}");

            var mainPage = new MainPage(DriverWrapper);
            mainPage.AcceptCookiesIfDisplayed();

            var searchResultsPage = mainPage.PerformGlobalSearch(searchText);

            var resultLinks = searchResultsPage.GetResultLinks();

            Assert.That(resultLinks, Is.Not.Empty, "No search results were found.");
            Assert.That(resultLinks.All(link => link.Text.ToLower().Contains(searchText.ToLower())),
                $"Not all search results contain the search text '{searchText}'.");
            Logger.Info($"GlobalSearchTests test with parameters: searchText {searchText} end");
        }
    }
}
