using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Interfaces
{
    public interface IWebDriverWrapper
    {
        void NavigateTo(string url);
        IWebElement FindElement(By by);
        IReadOnlyCollection<IWebElement> FindElements(By by);
        IWebDriver GetWebDriver();
        string GetUrl();
    }
}
