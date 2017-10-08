using System;

namespace DddCqrsEsExample.Logging
{
    public enum LogSeverity
    {
        Debug,
        Info,
        Warning,
        Error,
        Exception
    }

    public class LogEntry
    {
        public string Username { get; private set; }

        public string Severity { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }
        
        public string MachineName { get; private set; }
        public string ApplicationName { get; private set; }

        public DateTime DateTime { get; private set; }

        public LogEntry(
            string username,
            LogSeverity severity,
            string message,
            string stackTrace,
            string machineName,
            string applicationName,
            DateTime dateTime)
        {
            Username = username;
            Severity = severity.ToString();
            Message = message;
            StackTrace = stackTrace;
            MachineName = machineName;
            ApplicationName = applicationName;
            DateTime = dateTime;
        }
    }
}
