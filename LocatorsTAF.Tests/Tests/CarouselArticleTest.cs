using LocatorsTAF.BusinessLayer.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.Tests.Tests
{
    public class CarouselArticleTest : BaseTest
    {
        [Test]
        public void CarouselArticleShouldMatchOpenedArticle()
        {
            Logger.Info($"CarouselArticleTest test CarouselArticleShouldMatchOpenedArticle");
            var mainPage = new MainPage(DriverWrapper);
            mainPage.AcceptCookiesIfDisplayed();

            var insightPage = mainPage.NavigateToInsightsPage();
            insightPage.ClickRightArrow();
            insightPage.Wait();
            insightPage.ClickRightArrow();

            string expected = insightPage.GetArticleTitle().Trim();

            insightPage.ClickArticle();

            string actual = insightPage.GetReadMoreArticleTitle().Trim();

            Assert.That(expected.ToLower().Contains(actual.ToLower()), Is.True);
            Logger.Info($"CarouselArticleTest test CarouselArticleShouldMatchOpenedArticle end");
        }
    }
}
