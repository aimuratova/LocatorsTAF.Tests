using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.Interfaces
{
    public interface IWebElementWrapper
    {
        void Click();
        void EnterText(string text);
        void ClearText();
        string GetText();
        void ScrollIntoView();
        void Hover();
        void WaitUntilTextIsNot(string previousTitle);
        IWebElementWrapper Child(By by);
    }
}
