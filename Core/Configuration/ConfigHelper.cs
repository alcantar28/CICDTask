using log4net;
using log4net.Config;
using log4net.Core;
using Microsoft.Extensions.Configuration;

namespace CICD.Core.Configuration
{
    public class ConfigHelper
    {
        const string ConfigDirectory = "Core\\Configuration";
        const string ConfigFileName = "appconfig";
        const string ConfigFileExtension = ".json";
        const string logConfigFile = "log4net.config";

        public static string GetProjectDirectory()
        {
            var path = AppContext.BaseDirectory;
            return path.Substring(0, path.LastIndexOf("bin"));
        }

        private static string GetSettingsFile()
        {
            var path = GetProjectDirectory();
            var filesInConfigDir = Directory.GetFiles(path + ConfigDirectory);
            var settingsFile = filesInConfigDir.FirstOrDefault(x => x.Contains($"{ConfigFileName}{ConfigFileExtension}"));

            if (settingsFile == null)
                throw new FileNotFoundException($"Settings file '{ConfigFileName}{ConfigFileExtension}' not found in {path}");

            return settingsFile;
        }

        public static string GetAppConfigValue(string key)
        {
            var settingsFile = GetSettingsFile();
            var builder = new ConfigurationBuilder().AddJsonFile(settingsFile, optional: true, reloadOnChange: true);
            var root = builder.Build();
            return root[key];
        }

        private static Level ParseLogLevel(string logLevel)
        {
            return logLevel?.ToUpper() switch
            {
                "ALL" => Level.All,
                "DEBUG" => Level.Debug,
                "INFO" => Level.Info,
                "WARN" => Level.Warn,
                "ERROR" => Level.Error,
                "FATAL" => Level.Fatal,
                "OFF" => Level.Off,
                _ => Level.Info
            };
        }

        public static void ConfigureLogging()
        {
            //Configure log4net using log4net.config file
            var path = GetProjectDirectory();
            var logConfigPath = $"{path}{ConfigDirectory}\\{logConfigFile}";
            XmlConfigurator.Configure(new FileInfo(logConfigPath));

            //Obtain minimum log level from appconfig.json
            var logLevelString = GetAppConfigValue("Logging:MinimumLevel") ?? "Info";
            var loggingLevel = ParseLogLevel(logLevelString);
            LogManager.GetRepository().Threshold = loggingLevel;

            // Set absolute path for log4net file appender, so it doesn't go to bin/Debug/net8.0 or bin/Release/net8.0
            var logRepository = LogManager.GetRepository();
            var appenders = logRepository.GetAppenders();
            foreach (var appender in appenders)
            {
                if (appender is log4net.Appender.RollingFileAppender fileAppender)
                {
                    var logDir = Path.Combine(path, "Logs");
                    Directory.CreateDirectory(logDir); // Create directory if it doesn't exist
                    fileAppender.File = Path.Combine(logDir, "APITestingTask_");
                    fileAppender.ActivateOptions();
                }
            }
        }
    }
}
