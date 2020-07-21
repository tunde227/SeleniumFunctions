using System.Reflection;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;

namespace Core.Utility
{
    public static class Log
    {
        public static void SetUp(Assembly assembly, string fileName)
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date{yyyy-MM-dd hh:mm:ss tt} [%thread] %-5level %logger - %message%newline"
            };
            patternLayout.ActivateOptions();

            var consoleAppender = new ConsoleAppender
            {
                Threshold = Level.All,
                Name = "ConsoleAppender",
                Layout = patternLayout
            };
            consoleAppender.ActivateOptions();

            var debugAppender = new DebugAppender
            {
                Threshold = Level.All,
                Name = "DebugAppender",
                Layout = patternLayout
            };
            debugAppender.ActivateOptions();

            var roller = new RollingFileAppender
            {
                AppendToFile = true,
                File = $"{fileName}/Automation",
                Layout = patternLayout,
                DatePattern = ".yyyy-MM-dd.lo\\g",
                MaximumFileSize = "10MB",
                RollingStyle = RollingFileAppender.RollingMode.Date,
                StaticLogFileName = false
            };
            roller.ActivateOptions();

            BasicConfigurator.Configure(LoggerManager.GetRepository(assembly), consoleAppender, debugAppender, roller);
        }
    }
}