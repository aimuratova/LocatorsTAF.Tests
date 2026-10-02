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
        private readonly TestSettings _settings;
        private IWebDriver? _driver;

        public string DownloadDirectory { get; } = Path.Combine(Path.GetTempPath(), "TafDownloads", Guid.NewGuid().ToString("N"));


        public DriverManager(TestSettings settings)
        {
            _settings = settings;
        }

        public IWebDriver Current =>
            _driver ?? throw new InvalidOperationException("Browser is not started.");

        public void StartBrowser()
        {
            if (_driver is not null)
                throw new InvalidOperationException("Browser is already started.");

            if (_driver is not null)
                throw new InvalidOperationException("Browser is already started.");

            Directory.CreateDirectory(DownloadDirectory);
            _driver = DriverFactory.Create(_settings, DownloadDirectory);
        }

        public void QuitBrowser()
        {
            if (_driver is null) return;
            try { _driver.Quit(); }
            finally
            {
                _driver.Dispose();
                _driver = null;
                try { Directory.Delete(DownloadDirectory, recursive: true); } catch { /* best effort */ }
            }
        }
    }
}
