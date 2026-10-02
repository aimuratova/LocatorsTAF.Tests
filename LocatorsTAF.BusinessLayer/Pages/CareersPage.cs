using LocatorsTAF.CoreLayer.Element;
using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;

namespace LocatorsTAF.BusinessLayer.Pages;

public class CareersPage : BasePage
{
    private readonly IWebElementWrapper _jobsLink;

    public CareersPage(IWebDriverWrapper driver) : base(driver)
    {
        _jobsLink = new WebElementWrapper(driver, By.XPath("//a[contains(@href,'careers.epam.com/en/jobs')]"));
    }

    public JobsPage NavigateToJobsPage()
    {
        _jobsLink.Click();
        return new JobsPage(Driver);
    }
}
