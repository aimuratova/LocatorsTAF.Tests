using log4net;
using log4net.Config;
using System.Reflection;

namespace LocatorsTAF.CoreLayer.Utilities;

public static class LoggingConfigurator
{
    public static void Configure()
    {
        var repository = LogManager.GetRepository(
            Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
        XmlConfigurator.Configure(repository,
            new FileInfo(Path.Combine(AppContext.BaseDirectory, "log4net.config")));
    }
}
