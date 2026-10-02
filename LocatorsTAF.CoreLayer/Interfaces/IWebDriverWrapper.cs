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
        string Url { get; }

        void NavigateTo(string url);
        IWebElement FindElement(By by, TimeSpan? timeout = null);
        IReadOnlyCollection<IWebElement> FindElements(By by);        
        IWebElement? TryWaitUntilClickable(By by, TimeSpan timeout);
        void WaitUntilInvisible(By by, TimeSpan? timeout = null);
        IWebElement WaitUntilClickable(By by, TimeSpan? timeout = null);
        T WaitFor<T>(Func<IWebDriver, T> condition, TimeSpan? timeout = null);
        void WaitForTransientToDisappear(By locator, TimeSpan appearTimeout);
        void MoveToElement(IWebElement element);
        void ExecuteScript(string v, IWebElement e);
        void WaitUntilUrlChanges(string oldUrl);
        IReadOnlyList<string> GetDisplayedTexts(By by, TimeSpan? timeout = null);
        byte[] TakeScreenshot();
    }
}
