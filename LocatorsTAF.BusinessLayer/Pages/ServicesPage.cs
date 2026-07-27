using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.BusinessLayer.Pages
{
    public class ServicesPage : BasePage
    {
        private readonly IWebElementWrapper pageContent;

        public ServicesPage(IWebDriverWrapper webDriverWrapper) : base(webDriverWrapper)
        {
            pageContent = new WebElementWrapper(webDriverWrapper, By.Id("main"));
        }

        public string GetPageTitleInService()
        {
            return pageContent.GetText();
        }

        public bool IsServicesSearchStringIsFound()
        {
            return pageContent.GetText().Contains("Our Related Expertise", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
