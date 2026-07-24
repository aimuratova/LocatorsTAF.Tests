using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.Tests
{
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var repository = LogManager.GetRepository(Assembly.GetExecutingAssembly());

            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }
    }
}
