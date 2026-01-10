using OpenQA.Selenium;

namespace CICD.Core.Utilities
{
    public static class ScreenshotMaker
    {
        public static void TakeScreenshot(IWebDriver driver)
        {
            var screenshotFile = Path.Combine(Environment.CurrentDirectory, TestContext.CurrentContext.Test.MethodName + ".png");

            //For full page screenshot
            ITakesScreenshot ScreenshotDriver = (ITakesScreenshot)driver;
            ScreenshotDriver.GetScreenshot().SaveAsFile(screenshotFile);

            ////For specific element screenshot
            //var element = driver.FindElement(By.TagName("h1"));
            //var elementScreenshot = ((ITakesScreenshot)element).GetScreenshot();
            //elementScreenshot.SaveAsFile(screenshotFile);
        }
    }
}
