using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace TricksInSeleniumWebDriver.Extension
{
    public static class SeleniumExtension
    {
        public static void Maximize(this IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
        }

        public static void ClickOnElement(this IWebDriver driver, By by)
        {
            driver.ExecuteJavaScript("arguments[0].setAttribute('style', arguments[1]);", driver.FindElement(by),
                "border: 2px solid red");
            driver.WaitForClickable(by);
            driver.FindElement(by).Click();
        }
    }
}