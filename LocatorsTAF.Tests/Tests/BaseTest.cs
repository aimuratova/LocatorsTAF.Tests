using LocatorsTAF.CoreLayer.Driver;
using LocatorsTAF.CoreLayer.Interfaces;
using LocatorsTAF.CoreLayer.Utilities;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.Tests.Tests
{
    public class BaseTest
    {
        protected ILoggingService Logger = null!;

        protected DriverManager DriverManager { get; private set; }
        protected IWebDriverWrapper DriverWrapper { get; private set; }
        
        [SetUp]
        public void OneTimeSetUp()
        {
            DriverManager = new DriverManager();
            DriverManager.StartBrowser();

            DriverWrapper = new WebdriverWrapper(DriverManager);

            Logger = new LoggerService();

            Logger.Info("========== Test started ==========");
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            DriverManager.QuitBrowser();
            Logger.Info("========== Test finished ==========");
        }
    }
}
