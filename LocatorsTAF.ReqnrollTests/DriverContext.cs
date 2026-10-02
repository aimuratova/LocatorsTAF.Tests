using LocatorsTAF.CoreLayer.Interfaces;

namespace LocatorsTAF.ReqnrollTests;

public class DriverContext
{
    public IWebDriverWrapper DriverWrapper { get; set; } = null!;
    public ILoggingService Logger { get; set; } = null!;
    public string DownloadDirectory { get; set; } = "";
}
