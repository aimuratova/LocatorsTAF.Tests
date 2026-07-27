using LocatorsTAF.CoreLayer.Driver;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.ReqnrollTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly DriverManager _driverManager;
        private readonly DriverContext _driverContext;

        public Hooks(DriverContext driverContext)
        {
            _driverContext = driverContext;
            _driverManager = new DriverManager();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driverManager.StartBrowser();

            _driverContext.DriverWrapper = new WebdriverWrapper(_driverManager);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driverManager.QuitBrowser();
        }
    }
}
