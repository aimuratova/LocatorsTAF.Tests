using LocatorsTAF.CoreLayer.Enums;

namespace LocatorsTAF.CoreLayer.Utilities;

public class TestSettings
{
    public BrowserType BrowserType { get; init; }
    public string AppUrl { get; init; } = "";
    public string ApiBaseUrl { get; init; } = "";
    public bool Headless { get; init; }
    public int ExplicitWaitSeconds { get; init; } = 10;
}
