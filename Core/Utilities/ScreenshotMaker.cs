using OpenQA.Selenium;

namespace CICD.Core.Utilities
{
    public static class ScreenshotMaker
    {
        public static string NewScreenshotDate
        {
            get { return $"_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff")}.png"; }
        }

        public static void TakeBrowserScreenshot(IWebDriver driver)
        {
            var screenshotPath = Path.Combine(Environment.CurrentDirectory, TestContext.CurrentContext.Test.MethodName + NewScreenshotDate);
            var image = (driver as ITakesScreenshot).GetScreenshot();
            image.SaveAsFile(screenshotPath);
        }
    }
}
