using OpenQA.Selenium;

namespace CICD.Core.Utilities
{
    public static class ScreenshotMaker
    {
        public static void TakeScreenshot(IWebDriver driver)
        {
            var screenshotFile = Path.Combine(Environment.CurrentDirectory, TestContext.CurrentContext.Test.MethodName + ".png");
            ITakesScreenshot ScreenshotDriver = (ITakesScreenshot)driver;
            ScreenshotDriver.GetScreenshot().SaveAsFile(screenshotFile);
        }
    }
}
