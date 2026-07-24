using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.BusinessLayer.Pages
{
    public class MainPage : BasePage
    {
        private readonly IWebElementWrapper careersLink;
        private readonly IWebElementWrapper searchMagnifier;
        private readonly IWebElementWrapper searchInput;
        private readonly IWebElementWrapper findButton;
        private readonly IWebElementWrapper pdfDownloadLink;
        private readonly IWebElementWrapper insightLink;

        public MainPage(IWebDriverWrapper driver) : base(driver)
        {
            careersLink = new WebElementWrapper(driver, By.PartialLinkText("Care"));
            searchMagnifier = new WebElementWrapper(driver, By.CssSelector("button[class*='search']"));
            searchInput = new WebElementWrapper(driver, By.XPath("//input[@type='search']"));
            findButton = new WebElementWrapper(driver, By.XPath("//button[.//span[normalize-space()='Find']]"));
            pdfDownloadLink = new WebElementWrapper(driver, By.XPath("//a[contains(@href,'Code-Of-Conduct_01_26.pdf')]"));
            insightLink = new WebElementWrapper(driver, By.CssSelector("a.top-navigation__item-link[href='/insights']"));
        }
        
        public InsightsPage NavigateToInsightsPage()
        {
            insightLink.Click();
            return new InsightsPage(driver);
        }

        public CareersPage NavigateToCareersPage()
        {
            careersLink.Click();
            return new CareersPage(driver);
        }

        public SearchResultPage PerformGlobalSearch(string searchText)
        {
            searchMagnifier.Click();

            searchInput.Click();
            searchInput.ClearText();
            searchInput.EnterText(searchText);

            findButton.Click();

            return new SearchResultPage(driver);
        }

        public void ClickToDownloadFile()
        {
            var pdf = pdfDownloadLink.FindElement();
            ((IJavaScriptExecutor)driver.GetWebDriver()).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", pdf);
            pdf.Click();
        }
    }
}
