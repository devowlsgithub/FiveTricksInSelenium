using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TricksInSeleniumWebDriver.Extension
{
    public class WebDriverFactory
    {
        public IWebDriver GetWebDriver(BrowserType browserType)
        {
            var options = new ChromeOptions();
            switch (browserType)
            {
                case BrowserType.Headless:
                    options.AddArgument("--headless");
                    return new ChromeDriver(options);
                case BrowserType.Default:
                    return new ChromeDriver();
                case BrowserType.EmulateMobile:
                    options.EnableMobileEmulation("iPhone 6");
                    options.AddArgument("--window-size=750,1334");
                    return new ChromeDriver(options);
                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
            }
        }
    }
}