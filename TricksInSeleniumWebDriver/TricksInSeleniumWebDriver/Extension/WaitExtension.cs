using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TricksInSeleniumWebDriver.Extension
{
    public static class WaitExtension
    {
        private static WebDriverWait wait;

        private static WebDriverWait Wait(this IWebDriver driver)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            return wait;
        }

        public static void WaitForClickable(this IWebDriver driver, By by)
        {
            driver.Wait().Until(ExpectedConditions.ElementToBeClickable(by));
        }
    }
}