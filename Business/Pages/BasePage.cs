//using TestAutomationFramework.Core.Utilities;
using CICD.Core.Configuration;
using CICD.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Reflection;

namespace CICD.Business.Pages
{
    public class BasePage
    {
        public readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly By acceptCookiesLocator = By.Id("onetrust-accept-btn-handler");
        private readonly By bodyLocator = By.TagName("body");

        public BasePage(IWebDriver driver)
        { 
            this.driver = driver;
            this.wait = InitializeWait();
            ////Tried to overcome the Cloudflare issue based on DIAL's suggestion, but it didn't work
            //((IJavaScriptExecutor)driver).ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");
        }

        public void OpenHomePage()
        {
            LogFileCreator.LogOpenWebsiteInfo();
            driver.Navigate().GoToUrl(ConfigHelper.GetAppConfigValue("url"));
            WaitForPageLoad();
        }

        private void WaitForPageLoad()
        {
            try
            {
                wait.Until(driver => driver.FindElement(bodyLocator));
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();

            }
        }
        public void AcceptCookies()
        {
            try
            {
                var acceptCookiesButton = wait.Until(driver => driver.FindElement(acceptCookiesLocator)); //ElementWaits.WaitForClickableElement(driver, acceptCookiesLocator);

                if (acceptCookiesButton.Enabled)
                {
                    ElementInteractions.MoveToElement(driver, acceptCookiesButton);
                    ElementInteractions.JsClickOnElement(driver, acceptCookiesButton);
                }
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        public WebDriverWait InitializeWait()
        {
            const double pollInterval = 0.5;
            const int waitInterval = 8;

            return new WebDriverWait(driver, TimeSpan.FromSeconds(waitInterval))
            {
                PollingInterval = TimeSpan.FromSeconds(pollInterval),
                Message = $"Element has not been found after {waitInterval} seconds."
            };
        }
    }
}

