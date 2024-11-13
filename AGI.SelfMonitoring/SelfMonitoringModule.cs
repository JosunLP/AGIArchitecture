
using System;
using System.Collections.Generic;

namespace AGI.SelfMonitoring
{
    public class SelfMonitoringModule
    {
        private readonly List<string> _logs;

        public SelfMonitoringModule()
        {
            _logs = new List<string>();
        }

        // Log performance or error information
        public void Log(string message)
        {
            var logEntry = $"{DateTime.Now}: {message}";
            _logs.Add(logEntry);
            Console.WriteLine(logEntry); // For real-time monitoring in console
        }

        // Retrieve logs for review
        public List<string> GetLogs()
        {
            return new List<string>(_logs);
        }
    }
}
