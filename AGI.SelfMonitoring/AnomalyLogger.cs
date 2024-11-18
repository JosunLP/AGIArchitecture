
using System;
using System.Collections.Generic;

namespace AGI.SelfMonitoring
{
    public class AnomalyLogger
    {
        private List<string> LogEntries;

        public AnomalyLogger()
        {
            LogEntries = new List<string>();
        }

        public void LogAnomaly(string anomalyDescription)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] Anomaly Detected: {anomalyDescription}";
            LogEntries.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public void ShowLogs()
        {
            foreach (var log in LogEntries)
            {
                Console.WriteLine(log);
            }
        }
    }
}
