using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Reflection;
using CICD.Core.Utilities;

namespace CICD.Business
{
    public static class ElementWaits
    {
        public static IWebElement WaitForExistingElement(IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        public static IWebElement WaitForClickableElement(IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
    }
}