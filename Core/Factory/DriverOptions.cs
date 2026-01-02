using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace CICD.Core.Factory
{
    public static class DriverOptions
    {
        private static readonly string downloadsPath = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads\";
        public static ChromeOptions ChromeDriverHeadlessOptions()
        {
            var chromeOptions = new ChromeOptions();
            //These arguments are needed for running the code in CI/CD pipeline
            //chromeOptions.AddArgument("--no-sandbox");
            //chromeOptions.AddArgument("--disable-dev-shm-usage");
            
            //chromeOptions.AddArgument("--headless");
            //chromeOptions.AddUserProfilePreference("download.default_directory", downloadsPath);
            //chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            //chromeOptions.AddArgument("--window-size=1920, 1080");
            chromeOptions.AddArgument("--headless=new"); // Use new headless mode for better masking
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("disable-infobars");
            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalOption("useAutomationExtension", false);
            chromeOptions.AddArgument("window-size=1920,1080");
            chromeOptions.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            
            return chromeOptions;
        }

        public static EdgeOptions EdgeDriverHeadlessOptions()
        {
            var edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument("--headless");
            edgeOptions.AddUserProfilePreference("download.default_directory", downloadsPath);
            edgeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            edgeOptions.AddArgument("--window-size=1920, 1080");
            return edgeOptions;
        }

        public static FirefoxOptions FirefoxHeadlessDriverOptions()
        {
            var firefoxOptions = new FirefoxOptions();

            firefoxOptions.AddArgument("--headless");
            // Disable the built-in PDF viewer for the file download
            firefoxOptions.SetPreference("pdfjs.enabledCache.state", false);
            firefoxOptions.SetPreference("pdfjs.disabled", true);
            firefoxOptions.SetPreference("browser.download.dir", downloadsPath);
            firefoxOptions.SetPreference("browser.download.folderList", 2);
            firefoxOptions.AddArgument("--window-size=1920, 1080");

            return firefoxOptions;
        }

        public static FirefoxOptions FirefoxDriverOptions()
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

            return firefoxOptions;
        }
    }
}


