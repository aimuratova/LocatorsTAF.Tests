using LocatorsTAF.BusinessLayer.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.Tests.Tests
{
    public class PdfDownloadTest : BaseTest
    {
        [TestCase("Code-Of-Conduct_01_26.pdf")]
        public void PdfShouldBeDownloaded(string fileName)
        {
            Logger.Info($"PdfDownloadTest test with parameters: fileName {fileName}");

            var mainPage = new MainPage(DriverWrapper);
            mainPage.AcceptCookiesIfDisplayed();

            mainPage.ClickToDownloadFile();

            var downloadFolder = Path.Combine(Path.GetTempPath(), "Downloads");
            Directory.CreateDirectory(downloadFolder);
            var isFileDownloaded = WaitForDownload(downloadFolder, fileName);

            Logger.Info($"PdfDownloadTest test with parameters: fileName {fileName} is downloaded {isFileDownloaded.ToString()}");

            Assert.That(isFileDownloaded, Is.True);
            Logger.Info($"PdfDownloadTest test with parameters: fileName {fileName} end");
        }

        private bool WaitForDownload(string folder, string fileName, int timeoutSeconds = 30)
        {
            var wait = new WebDriverWait(new SystemClock(), DriverWrapper.GetWebDriver(),
                TimeSpan.FromSeconds(timeoutSeconds),
                TimeSpan.FromMilliseconds(500));

            return wait.Until(_ =>
            {
                var path = Path.Combine(folder, fileName);
                return File.Exists(path) && !File.Exists(path + ".crdownload");
            });
        }
    }
}
