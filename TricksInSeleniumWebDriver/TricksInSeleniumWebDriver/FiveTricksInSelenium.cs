using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Applitools.Selenium;
using Mailosaur;
using Mailosaur.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using TricksInSeleniumWebDriver.Extension;

namespace TricksInSeleniumWebDriver
{
    [TestFixture(BrowserType.Headless)]
    [TestFixture(BrowserType.EmulateMobile)]
    [TestFixture(BrowserType.Default)]
    public class FiveTricksInSelenium
    {
        WebDriverFactory webDriverFactory;
        private IWebDriver driver;
        private readonly BrowserType browserType;
        private Eyes eyes;

        public FiveTricksInSelenium(BrowserType browserType)
        {
            this.browserType = browserType;
        }

        [SetUp]
        public void Setup()
        {
            webDriverFactory = new WebDriverFactory();
            eyes = new Eyes {ApiKey = "kbaKyuBEoh107j8oVqCPN9h7RX351Lzr0Xe9cco7gVxA4110"};
            driver = webDriverFactory.GetWebDriver(browserType);
        }

        [Test]
        public void LostPassword_EnterLoginSubmitItAndTakeForgotPasswordMailPutNewPassword_PasswordSuccessfullyChanged()
        {
//            driver.Maximize();
            eyes.Open(driver, "Go To Lost Password Form Rest Password And Change Password To New",
                "Enter To Lost Password Form");
            driver.Navigate().GoToUrl("https://wordpress.com/wp-login.php?action=lostpassword");
            driver.ClickOnElement(By.CssSelector("#user_login"));
            driver.FindElement(By.CssSelector("#user_login")).SendKeys("testcommunicate");
            eyes.CheckWindow("Filled name of user");
            driver.ClickOnElement(By.CssSelector("#wp-submit"));
            eyes.CheckWindow("Send form with the name of user");
            //there you need to put your mailosaur api key
            var client = new MailosaurClient("SFzsuS7cp5qHbom", "https://mailosaur.com");

            //there put search criteria which emails we want to take
            var criteria = new SearchCriteria {SentTo = "wordpresstest.wr7z7h7k@mailosaur.io"};

            var message = client.Messages.Get("wr7z7h7k", criteria);
            var takeUrlFromMessage = Regex.Matches(message.Text.Body,
                @"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
            var value = takeUrlFromMessage[0].Value;
            driver.Navigate().GoToUrl(value);
            driver.ClickOnElement(By.CssSelector("#pass1"));
            driver.FindElement(By.CssSelector("#pass1")).SendKeys("_new_password_");
            driver.ClickOnElement(By.CssSelector("#wp-submit"));
            driver.WaitForClickable(By.CssSelector(".reset-pass"));
            var text = driver.FindElement(By.CssSelector(".reset-pass")).Text;
            Assert.AreEqual("Your password has been reset. Log in", text);
           
        }

        [TearDown]
        public void TearDown()
        {
            eyes.Close();
            driver.Close();
            driver.Quit();
        }
    }
}