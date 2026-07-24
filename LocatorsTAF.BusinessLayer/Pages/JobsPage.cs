using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using LocatorsTAF.CoreLayer.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.BusinessLayer.Pages
{
    public class JobsPage : BasePage
    {
        private readonly IWebElementWrapper countryInput;
        private readonly IWebElementWrapper countryDropdown;
        private readonly IWebElementWrapper jobTitleInput;
        private readonly IWebElementWrapper remoteCheckbox;
        private readonly IWebElementWrapper searchButton;
        private readonly IWebElementWrapper lastSearchResult;
        private readonly WebDriverWait wait;


        public JobsPage(IWebDriverWrapper driver) : base(driver)
        {
            countryInput = new WebElementWrapper(driver, By.XPath("//input[@aria-label='Choose your country']"));
            countryDropdown = new WebElementWrapper(driver, By.XPath($"//div[contains(@class, 'dropdown__menu')]"));
            jobTitleInput = new WebElementWrapper(driver, By.Name("search"));
            remoteCheckbox = new WebElementWrapper(driver, By.XPath("//span[normalize-space()='Remote']"));
            searchButton = new WebElementWrapper(driver, By.CssSelector("button[type='submit']"));
            lastSearchResult = new WebElementWrapper(driver, By.XPath("(//div[@data-testid='accordion-section-container'])[last()]"));

            wait = new WebDriverWait(driver.GetWebDriver(), TimeSpan.FromSeconds(ConfigurationService.ImplicitWaitTime));
        }

        public void ClickRemote()
        {
            var element = remoteCheckbox.FindElement();
            if (!element.Selected)
            {
                element.Click();
            }
        }

        public void ClickSearchButton()
        {
            searchButton.Click();
        }

        public void EnterJobTitle(string jobTitle)
        {
            jobTitleInput.Click();
            jobTitleInput.ClearText();
            jobTitleInput.EnterText(jobTitle);
        }

        public string GetResultDescription()
        {
            return lastSearchResult.GetText();
        }

        public void LastSearchResultClick()
        {
            lastSearchResult.Click();

            wait.Until(d =>
            {
                var applyButton = d.FindElement(By.XPath("(//*[@id='cta_job_apply_unauthorized'])[last()]"));

                return applyButton.Displayed &&
                       applyButton.Location.Y > 0 &&
                       applyButton.Size.Height > 0;
            });
        }

        public void SelectCountry(string country)
        {
            countryInput.Click();
            countryInput.ClearText();
            countryInput.EnterText(country);

            countryDropdown.FindElement();
            var option = countryDropdown.FindChildBy(By.XPath($"//div[@data-testid='dropdown-option'][contains(., '{country}')]"));
            option.Click();
        }

        public void WaitForLoader()
        {
            wait.Until(d =>
            {
                var loaders = d.FindElements(
                    By.CssSelector("div.Preloader_fullSize__jIIky"));

                return loaders.Count == 0 || !loaders.Any(x => x.Displayed);
            });
        }
    }
}
