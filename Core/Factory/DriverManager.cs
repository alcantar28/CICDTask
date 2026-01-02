using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Reflection;
using CICD.Core.Utilities;
using CICD.Core.Configuration;

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
                        var chromeOptions = isHeadless ? DriverOptions.ChromeDriverHeadlessOptions() : new ChromeOptions();
                        driver.Value = new ChromeDriver(chromeOptions);
                        break;
                    case "EDGE":
                        var edgeOptions = isHeadless ? DriverOptions.EdgeDriverHeadlessOptions() : new EdgeOptions();
                        driver.Value = new EdgeDriver(edgeOptions);
                        break;
                    case "FIREFOX":
                        var firefoxOptions = isHeadless ? DriverOptions.FirefoxHeadlessDriverOptions() : DriverOptions.FirefoxDriverOptions();
                        driver.Value = new FirefoxDriver(firefoxOptions);
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