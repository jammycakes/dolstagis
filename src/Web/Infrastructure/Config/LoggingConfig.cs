using System;
using System.IO;

using NLog;
using NLog.Config;
using NLog.Targets;

namespace Dolstagis.Web.Infrastructure.Config
{
    public static class LoggingConfig
    {
        public static void InitLogging()
        {
            string loggingFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\logs");
            loggingFolder = new DirectoryInfo(loggingFolder).FullName;
            string logFileName = Path.Combine(loggingFolder, @"web\app\web.log");
            string defaultConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logging.config");
            string localConfig = Path.Combine(loggingFolder, @"\web\logging.config");

            var config = new LoggingConfiguration();
            var target = new FileTarget() {
                FileName = logFileName,
                ArchiveEvery = FileArchivePeriod.Day,
                CreateDirs = true
            };
            config.AddTarget("logfile", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, target));
            LogManager.Configuration = config;
        }
    }
}
