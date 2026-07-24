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
    public class SearchResultPage : BasePage
    {
        private readonly IWebElementWrapper resultsContainer;

        public SearchResultPage(IWebDriverWrapper driver) : base(driver)
        {
            resultsContainer = new WebElementWrapper(driver, By.XPath("//div[contains(@class,'search-results__items')]"));
        }

        public IWebElement[] GetResultLinks()
        {
            return resultsContainer.FindElement().FindElements(By.XPath(".//article[contains(@class,'search-results__item')]")).ToArray();
        }
    }
}
