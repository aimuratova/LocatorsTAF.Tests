using LocatorsTAF.CoreLayer.Interfaces;
using OpenQA.Selenium;

namespace LocatorsTAF.CoreLayer.Element;

public class WebElementWrapper : IWebElementWrapper
{
    private readonly IWebDriverWrapper _driver;
    private readonly By _locator;
    private readonly WebElementWrapper? _parent;

    public WebElementWrapper(IWebDriverWrapper driver, By locator)
    {
        _driver = driver;
        _locator = locator;
    }
    
    private WebElementWrapper(WebElementWrapper parent, By locator)
        : this(parent._driver, locator)
    {
        _parent = parent;
    }

    public void Click() => Perform(e => e.Click());

    public void ClearText() => Perform(e =>
    {
        e.Click();
        e.SendKeys(Keys.Control + "a");
        e.SendKeys(Keys.Delete);
    });

    public void EnterText(string text) => Perform(e => e.SendKeys(text));

    public string GetText() => Read(e => e.Text);

    public string GetAttribute(string name) => Read(e => e.GetAttribute(name) ?? "");
        
    public IWebElementWrapper Child(By by) => new WebElementWrapper(this, by);

    public void ScrollIntoView() => Perform(e =>
    _driver.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", e));

    public void Hover() => Perform(e => _driver.MoveToElement(e));
        
    private void Perform(Action<IWebElement> action) =>
        _driver.WaitFor(d =>
        {
            var element = d.FindElement(_locator);
            if (!element.Displayed) return false;
            action(element);
            return true;
        });

    private string Read(Func<IWebElement, string> read) =>
        _driver.WaitFor(d =>
        {
            var element = d.FindElement(_locator);
            return element.Displayed ? read(element) : null!;
        });

    public void WaitUntilTextIsNot(string oldText) =>
        _driver.WaitFor(d =>
        {
            var element = d.FindElement(_locator);
            return element.Displayed && element.Text != oldText;
        });
}
