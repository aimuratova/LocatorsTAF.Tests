using LocatorsTAF.BusinessLayer.Pages;
using LocatorsTAF.CoreLayer.Enums;
using LocatorsTAF.CoreLayer.Helpers;
using OpenQA.Selenium.Support.UI;

namespace LocatorsTAF.Tests.Tests;

[Category("UI")]
public class PdfDownloadTest : BaseTest
{
    [Test]
    public void CodeOfEthicalConductPdf_IsDownloaded()
    {
        Assume.That(TestSetup.Settings.BrowserType, Is.EqualTo(BrowserType.Chrome),
            "Download preferences are configured for Chrome only.");

        var mainPage = new MainPage(DriverWrapper);
        mainPage.AcceptCookiesIfDisplayed();

        mainPage.ClickToDownloadFile();

        var downloadFolder = Path.Combine(Path.GetTempPath(), "Downloads");
        Directory.CreateDirectory(downloadFolder);

        var path = FileDownloadHelper.WaitForFile(
            downloadFolder, MainPage.CodeOfConductFileName, TimeSpan.FromSeconds(30));

        Assert.That(new FileInfo(path).Length, Is.GreaterThan(0), "Downloaded file is empty.");
    }
}
