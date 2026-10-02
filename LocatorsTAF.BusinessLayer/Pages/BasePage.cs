using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.BusinessLayer.Pages
{
    public abstract class BasePage
    {
        private static readonly By CookieAcceptButton = By.Id("onetrust-accept-btn-handler");

        protected IWebDriverWrapper Driver { get; }

        protected BasePage(IWebDriverWrapper driver)
        {
            Driver = driver;
        }

        public void AcceptCookiesIfDisplayed()
        {
            var acceptButton = Driver.TryWaitUntilClickable(CookieAcceptButton, TimeSpan.FromSeconds(3));
            if (acceptButton is null)
                return; // banner was not shown

            acceptButton.Click();
            Driver.WaitUntilInvisible(CookieAcceptButton);
        }
    }
}
