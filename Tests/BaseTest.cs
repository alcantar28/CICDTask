using log4net.Config;
using NUnit.Framework.Interfaces;
using CICD.Business.Pages;
using CICD.Core.Configuration;
using CICD.Core.Factory;
using CICD.Core.Utilities;

namespace CICD.Tests
{
    public class BaseTest
    {
        public BaseTest()
        { }

        [SetUp]
        public void Setup()
        {
            ConfigureLogging();
            var basePage = new BasePage(DriverManager.GetInstance());
            basePage.OpenHomePage();
            basePage.AcceptCookies();
        }

        [TearDown]
        public void Cleanup()
        {
            LogFileCreator.LogCloseWebsiteInfo();
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            if (testStatus.Equals(TestStatus.Failed) || testStatus.Equals(TestStatus.Inconclusive))
            {
                ScreenshotMaker.TakeScreenshot(DriverManager.GetInstance());
            }
            DriverManager.QuitDriver();
        }

        private void ConfigureLogging()
        {
            const string configurationPath = "\\Core\\Configuration\\";

            var logConfigFile = ConfigHelper.GetAppConfigValue("logConfigFile");
            var path = ConfigHelper.GetProjectDirectory();
            XmlConfigurator.Configure(new FileInfo(path + configurationPath + logConfigFile));
        }
    }
}
