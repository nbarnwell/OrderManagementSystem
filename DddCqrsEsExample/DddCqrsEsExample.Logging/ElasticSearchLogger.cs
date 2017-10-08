using System;
using Inforigami.Regalo.Core;
using Nest;
using Newtonsoft.Json;

namespace DddCqrsEsExample.Logging
{
    public class ElasticSearchLogger : ILogger
    {
        private readonly IElasticClient _elasticsearch;

        private readonly ILogger _consoleLogger = new ConsoleLogger();

        public ElasticSearchLogger(IElasticClient elasticsearch)
        {
            if (elasticsearch == null) throw new ArgumentNullException(nameof(elasticsearch));
            _elasticsearch = elasticsearch;
        }

        public void Debug(object sender, string format, params object[] args)
        {
            var entry = 
                new LogEntry(
                    System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    LogSeverity.Debug, 
                    string.Format(format, args),
                    null,
                    Environment.MachineName,
                    "SweetShop",
                    DateTime.Now);

            Log(entry);

            _consoleLogger.Debug(sender, format, args);
        }

        public void Info(object sender, string format, params object[] args)
        {
            var entry = 
                new LogEntry(
                    System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    LogSeverity.Info, 
                    string.Format(format, args),
                    null,
                    Environment.MachineName,
                    "SweetShop",
                    DateTime.Now);

            Log(entry);

            _consoleLogger.Info(sender, format, args);
        }

        public void Warn(object sender, string format, params object[] args)
        {
            var entry = 
                new LogEntry(
                    System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    LogSeverity.Warning, 
                    string.Format(format, args),
                    null,
                    Environment.MachineName,
                    "SweetShop",
                    DateTime.Now);

            Log(entry);

            _consoleLogger.Warn(sender, format, args);
        }

        public void Error(object sender, string format, params object[] args)
        {
            var entry = 
                new LogEntry(
                    System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    LogSeverity.Error, 
                    string.Format(format, args),
                    null,
                    Environment.MachineName,
                    "SweetShop",
                    DateTime.Now);

            Log(entry);

            _consoleLogger.Error(sender, format, args);
        }

        public void Error(object sender, Exception exception, string format, params object[] args)
        {
            var entry = 
                new LogEntry(
                    System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    LogSeverity.Exception,
                    $"Exception: {exception.Message}. Message: " + string.Format(format, args),
                    exception.StackTrace,
                    Environment.MachineName,
                    "SweetShop",
                    DateTime.Now);

            Log(entry);

            _consoleLogger.Error(sender, exception, format, args);
        }

        private void Log(LogEntry entry)
        {
            var response = _elasticsearch.Index(entry);
        }
    }
}