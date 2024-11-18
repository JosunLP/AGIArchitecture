
using System;
using System.Collections.Generic;

namespace AGI.EthicsAndSafety
{
    public class EthicalAuditLog
    {
        private readonly List<string> AuditEntries;

        public EthicalAuditLog()
        {
            AuditEntries = [];
        }

        // Method to add an entry to the audit log
        public void LogDecision(string action, bool isPermissible, string reason)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string entry = $"[{timestamp}] Action: '{action}', Permissible: {isPermissible}, Reason: '{reason}'";
            AuditEntries.Add(entry);
            Console.WriteLine(entry);
        }

        // Method to display all audit log entries
        public void ShowAuditLog()
        {
            Console.WriteLine("Ethical Audit Log:");
            foreach (var entry in AuditEntries)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
