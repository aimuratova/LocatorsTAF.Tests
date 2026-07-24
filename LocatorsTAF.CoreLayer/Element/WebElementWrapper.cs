using LocatorsTAF.CoreLayer.Driver;
using LocatorsTAF.CoreLayer.Interfaces;
using LocatorsTAF.CoreLayer.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Element
{
    public class WebElementWrapper : IWebElementWrapper
    {
        private readonly IWebDriverWrapper _driver;
        private readonly By _locator;
        private readonly TimeSpan _timeout;

        public WebElementWrapper(IWebDriverWrapper driver, By locator)
        {
            _driver = driver;
            _locator = locator;
            _timeout = TimeSpan.FromSeconds(ConfigurationService.ImplicitWaitTime);
        }

        public void Click()
        {
            var element = WaitForElementToBePresent();
            new Actions(_driver.GetWebDriver()).MoveToElement(element).Click().Perform();
        }

        public void EnterText(string text)
        {
            var element = WaitForElementToBePresent();
            element.Clear();
            element.SendKeys(text);
        }

        public void ClearText()
        {
            var element = WaitForElementToBePresent();

            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        public string GetText()
        {
            var element = WaitForElementToBePresent();
            return element.Text;
        }

        public IWebElement FindElement()
        {
            var elementPresent = WaitForElementToBePresent();
            return elementPresent;
        }
                
        public IWebElement WaitForElementToBePresent()
        {            
            var wait = new WebDriverWait(_driver.GetWebDriver(), _timeout);

            return wait.Until(drv =>
            {
                try
                {
                    var el = drv.FindElement(_locator);
                    return el.Displayed ? el : null;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("WaitForElementToBePresent method: 'NoSuchElementException' is found.");
                }
                return null;
            });
        }

        public IWebElement FindChildBy(By by)
        {
            var parent = WaitForElementToBePresent();

            var wait = new WebDriverWait(_driver.GetWebDriver(), _timeout);

            return wait.Until(_ =>
            {
                try
                {
                    var child = parent.FindElement(by);
                    return child.Displayed ? child : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    // Reacquire the parent if it became stale
                    parent = WaitForElementToBePresent();
                    return null;
                }
            });
        }
    }
}
