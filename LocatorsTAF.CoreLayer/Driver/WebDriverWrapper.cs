using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Enums;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Driver
{
    public class WebdriverWrapper : IWebDriverWrapper
    {
        private readonly DriverManager _driverManager;

        public WebdriverWrapper(DriverManager driverManager)
        {
            _driverManager = driverManager;
        }

        public void NavigateTo(string url)
        {
            _driverManager.Current.Navigate().GoToUrl(url);
        }
        
        public IWebElement FindElement(By by)
        {
            return _driverManager.Current.FindElement(by);
        }

        public IReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _driverManager.Current.FindElements(by);
        }

        public IWebDriver GetWebDriver()
        {
            return _driverManager.Current;
        }

        public string GetUrl()
        {
            return _driverManager.Current.Url;
        }
    }
}
