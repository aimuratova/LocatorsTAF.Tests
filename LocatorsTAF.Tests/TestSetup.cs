using LocatorsTAF.CoreLayer.Utilities;
using log4net;
using log4net.Config;
using System.Reflection;

namespace LocatorsTAF.Tests;

[SetUpFixture]
public class TestSetup
{
    public static TestSettings Settings { get; private set; } = null!;

    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        var repository = LogManager.GetRepository(
            Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
        XmlConfigurator.Configure(repository,
            new FileInfo(Path.Combine(AppContext.BaseDirectory, "log4net.config")));

        LoggingConfigurator.Configure();
        Settings = SettingsLoader.Load();
    }
}
