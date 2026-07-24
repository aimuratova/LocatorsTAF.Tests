using LocatorsTAF.CoreLayer.Enums;
using LocatorsTAF.CoreLayer.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Driver
{
    public class DriverManager
    {
        private readonly AsyncLocal<IWebDriver?> _driver = new();

        public DriverManager()
        {
            
        }

        public IWebDriver Current => _driver.Value ?? throw new InvalidOperationException("Browser is not started.");

        public void StartBrowser()
        {
            ConfigurationService.LoadConfiguration();
                       
            _driver.Value = DriverFactory.Create(ConfigurationService.BrowserType);
            _driver.Value.Navigate().GoToUrl(ConfigurationService.AppUrl);
        }

        public void QuitBrowser()
        {
            _driver.Value?.Quit();
            _driver.Value?.Dispose();
            _driver.Value = null;
        }
    }
}
