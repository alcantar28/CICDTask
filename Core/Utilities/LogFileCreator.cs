using log4net;

namespace CICD.Core.Utilities
{
    public static class LogFileCreator
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(LogFileCreator));

        public static void LogOpenWebsiteInfo()
        {
            log.Info($"[{TestContext.CurrentContext.Test.Name}] - OPEN WEBSITE");
        }

        public static void LogCloseWebsiteInfo()
        {
            log.Info($"[{TestContext.CurrentContext.Test.Name}] - CLOSE WEBSITE");
        }

        public static void LogBrowserError(string callingMethod, string browser)
        {
            log.Error($"[{TestContext.CurrentContext.Test.Name}] - {callingMethod} -  Browser {browser} not supported.");
        }

        public static void LogGeneralError(string callingMethod, Exception exception)
        {
            log.Error($"[{TestContext.CurrentContext.Test.Name}] \n METHOD: {callingMethod} \n MESSAGE: {exception.Message} \n STACK TRACE: {exception.StackTrace} \n EXCEPTION: {exception}");
        }
        public static void LogStartApiTestInfo()
        {
            log.Info($"[{TestContext.CurrentContext.Test.Name}] -STARTING API TEST");
        }

        public static void LogEndApiTestInfo()
        {
            log.Info($"[{TestContext.CurrentContext.Test.Name}] - ENDING API TEST");
        }
    }
}
