//using TestAutomationFramework.Core.Utilities;
using OpenQA.Selenium;
using CICD.Core.Configuration;
using CICD.Core.Utilities;

namespace CICD.Business.Pages
{
    public class BasePage
    {
        public readonly IWebDriver driver;
        private readonly By acceptCookiesLocator = By.Id("onetrust-accept-btn-handler");

        public BasePage(IWebDriver driver)
        { 
            this.driver = driver;
            ////Tried to overcome the Cloudflare issue based on DIAL's suggestion, but it didn't work
            //((IJavaScriptExecutor)driver).ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");
        }

        public void OpenHomePage()
        {
            LogFileCreator.LogOpenWebsiteInfo();
            driver.Navigate().GoToUrl(ConfigHelper.GetAppConfigValue("url"));
        }

        public void AcceptCookies()
        {
            var acceptCookiesButton = ElementWaits.WaitForExistingElement(driver, acceptCookiesLocator);
            if (acceptCookiesButton.Enabled)
            {
                ElementInteractions.MoveToElement(driver, acceptCookiesButton);
                ElementInteractions.JsClickOnElement(driver, acceptCookiesButton);
            }
        }
    }

}

