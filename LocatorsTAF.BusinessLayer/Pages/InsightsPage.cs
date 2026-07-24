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
    public class InsightsPage : BasePage
    {
        private readonly IWebElementWrapper rightArrowSlider;
        private readonly IWebElementWrapper articleTitle;
        private readonly IWebElementWrapper readMoreButton;
        private readonly IWebElementWrapper readMoreArticleTitle;
        private readonly WebDriverWait wait;

        public InsightsPage(IWebDriverWrapper driver) : base(driver)
        {
            rightArrowSlider = new WebElementWrapper(driver, By.CssSelector("button.slider__right-arrow"));
            articleTitle = new WebElementWrapper(driver, By.XPath("//div[contains(@class,'owl-item active')]//div[contains(@class,'text-image-slide-ui__parsys--2')]//span[contains(@class,'font-size-44')]"));
            readMoreButton = new WebElementWrapper(driver, By.XPath("//div[contains(@class,'owl-item active')]//a[contains(@class,'slider-cta-link')]"));
            readMoreArticleTitle = new WebElementWrapper(driver, By.TagName("h1"));

            wait = new WebDriverWait(driver.GetWebDriver(), TimeSpan.FromSeconds(ConfigurationService.ImplicitWaitTime));
        }

        public void ClickRightArrow()
        {
            rightArrowSlider.Click();
        }

        public void Wait()
        {
            wait.Until(d => true);
        }

        public string GetArticleTitle()
        {
            return articleTitle.GetText();
        }

        public void ClickArticle()
        {
            string oldUrl = driver.GetUrl();

            readMoreButton.Click();
            //wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'owl-item active')]//a[contains(@class,'slider-cta-link')]"))).Click();

            //var readMore = wait.Until(d =>
            //{
            //    var el = d.FindElement(By.XPath("//div[contains(@class,'owl-item')]//a[contains(@class,'slider-cta-link')]"));
            //    var classes = el.FindElement(By.XPath("./ancestor::div[contains(@class,'owl-item')]"))
            //                    .GetAttribute("class");

            //    return classes.Contains("active") &&
            //           !classes.Contains("cloned") &&
            //           !classes.Contains("owl-animated")
            //        ? el
            //        : null;
            //});

            //readMore.Click();

            wait.Until(d => d.Url != oldUrl);
        }

        public string GetReadMoreArticleTitle()
        {
            return readMoreArticleTitle.GetText();

            //return wait.Until(d => d.FindElement(By.TagName("h1"))).Text;
        }
    }
}
