using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Selenium.WebDriver.UndetectedChromeDriver;

namespace CICD.Core.Factory
{
    public static class DriverOptions
    {
        private static readonly string downloadsPath = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads\";
        public static Selenium.Extensions.SlDriver ChromeDriverHeadlessOptions()//ChromeOptions ChromeDriverHeadlessOptions()
        {
            var chromeOptions = new ChromeOptions();
            //These arguments are needed for running the code in CI/CD pipeline
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadsPath);
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            chromeOptions.AddArgument("--window-size=1920, 1080");
            return UndetectedChromeDriver.Instance("profile_name", chromeOptions);
        }

        public static EdgeDriver EdgeDriverHeadlessOptions()
        {
            var edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument("--headless");
            edgeOptions.AddUserProfilePreference("download.default_directory", downloadsPath);
            edgeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            edgeOptions.AddArgument("--window-size=1920, 1080");
            return new EdgeDriver(edgeOptions);
        }

        public static FirefoxDriver FirefoxHeadlessDriverOptions()
        {
            var firefoxOptions = new FirefoxOptions();

            firefoxOptions.AddArgument("--headless");
            // Disable the built-in PDF viewer for the file download
            firefoxOptions.SetPreference("pdfjs.enabledCache.state", false);
            firefoxOptions.SetPreference("pdfjs.disabled", true);
            firefoxOptions.SetPreference("browser.download.dir", downloadsPath);
            firefoxOptions.SetPreference("browser.download.folderList", 2);
            firefoxOptions.AddArgument("--window-size=1920, 1080");
            return new FirefoxDriver(firefoxOptions);
        }

        public static FirefoxDriver FirefoxDriverOptions()
        {
            var firefoxOptions = new FirefoxOptions();

            // Disable the built-in PDF viewer for the file download
            firefoxOptions.SetPreference("pdfjs.enabledCache.state", false);
            firefoxOptions.SetPreference("pdfjs.disabled", true);
            // Use this to disable Acrobat plugin for previewing PDFs in Firefox (if you have Adobe reader installed on your computer)
            firefoxOptions.SetPreference("plugin.scan.plid.all", false);
            firefoxOptions.SetPreference("plugin.disable_full_page_plugin_for_types", "application/pdf, application/octet-stream, application/x-pdf");
            firefoxOptions.SetPreference("browser.helperApps.alwaysAsk.force", false);
            firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf, application/octet-stream, application/x-pdf");
            return new FirefoxDriver(firefoxOptions);
        }
        public static IWebDriver CreateUndetectedChromeDriverHeadless()
        {
            var chromeOptions = new ChromeOptions();

            // Essential arguments for CI/CD pipeline
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--headless=new"); // Use new headless mode

            // Cloudflare bypass optimizations
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalOption("useAutomationExtension", false);
            chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");

            // Window and performance settings
            chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--disable-web-security");
            chromeOptions.AddArgument("--allow-running-insecure-content");
            chromeOptions.AddArgument("--disable-features=TranslateUI");
            chromeOptions.AddArgument("--disable-iframes-sandbox-restrictions");

            // Download preferences
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadsPath);
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);

            // Anti-detection preferences
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
            chromeOptions.AddUserProfilePreference("profile.managed_default_content_settings.images", 2);
            return UndetectedChromeDriver.Instance("profile_name", chromeOptions);
            //return UndetectedChromeDriver.Create(chromeOptions, driverExecutablePath: null, browserExecutablePath: null);
        }

        public static IWebDriver CreateUndetectedChromeDriver()
        {
            var chromeOptions = new ChromeOptions();

            // Basic arguments for non-headless mode
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--disable-gpu");

            // Cloudflare bypass optimizations
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalOption("useAutomationExtension", false);
            chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");

            // Window settings
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--disable-web-security");
            chromeOptions.AddArgument("--allow-running-insecure-content");

            // Download preferences
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadsPath);
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
            return UndetectedChromeDriver.Instance("profile_name", chromeOptions);
        }

        
    }
}

