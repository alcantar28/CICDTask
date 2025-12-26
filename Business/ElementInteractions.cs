using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Reflection;
using CICD.Core.Utilities;

namespace CICD.Business
{
    public static class ElementInteractions
    {
        public static IWebElement GetElement(IWebDriver driver, By locator)
        {
            try
            {
                return driver.FindElement(locator);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }

        public static ReadOnlyCollection<IWebElement> GetElements(IWebDriver driver, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {
                return wait.Until(driver => driver.FindElements(locator));
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        public static void MoveToElement(IWebDriver driver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)driver;

            try
            {
                js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        public static void JsClickOnElement(IWebDriver driver, IWebElement element)
        {
            var js = (IJavaScriptExecutor)driver;

            try
            {
                js.ExecuteScript("arguments[0].click();", element);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
    }
}