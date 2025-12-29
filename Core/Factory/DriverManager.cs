using CICD.Core.Configuration;
using CICD.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Selenium.WebDriver.UndetectedChromeDriver;
using System.Reflection;

namespace CICD.Core.Factory
{
    public sealed class DriverManager
    {
        private static readonly string browser = ConfigHelper.GetAppConfigValue("browser");
        private static readonly bool isHeadless = Convert.ToBoolean(ConfigHelper.GetAppConfigValue("headless"));

        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver GetInstance()
        {
            const int Seconds = 10;
            if (driver.Value == null)
            {
                switch (browser.ToUpper())
                {
                    case "CHROME":
                        //var chromeOptions = isHeadless ? DriverOptions.ChromeDriverHeadlessOptions() : new ChromeOptions();
                        //driver.Value = isHeadless ? DriverOptions.ChromeDriverHeadlessOptions() : new ChromeDriver(new ChromeOptions());//new ChromeDriver(chromeOptions);
                        driver.Value = isHeadless ? DriverOptions.CreateUndetectedChromeDriverHeadless() : DriverOptions.CreateUndetectedChromeDriver();
                        break;
                    case "EDGE":
                        driver.Value = isHeadless ? DriverOptions.EdgeDriverHeadlessOptions() : new EdgeDriver(new EdgeOptions());
                        break;
                    case "FIREFOX":
                        driver.Value = isHeadless ? DriverOptions.FirefoxHeadlessDriverOptions() : DriverOptions.FirefoxDriverOptions();
                        break;
                    default:
                        string methodName = MethodBase.GetCurrentMethod().Name;
                        LogFileCreator.LogBrowserError(methodName, browser);
                        throw new Exception($"Unable to set browser type {browser}");
                }
                driver.Value.Manage().Timeouts().ImplicitWait.Add(TimeSpan.FromSeconds(Seconds));
                // Window.Maximize() does not work in headless mode, window size must be set via arguments
                if (!isHeadless)
                {
                    driver.Value.Manage().Window.Maximize();
                }
            }
            return driver.Value;
        }

        public static void QuitDriver()
        {
            GetInstance().Dispose();
            GetInstance().Quit();
            driver.Value = null;
        }
    }
}