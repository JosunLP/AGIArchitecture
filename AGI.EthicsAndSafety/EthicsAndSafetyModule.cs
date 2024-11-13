
using System;
using System.Collections.Generic;

namespace AGI.EthicsAndSafety
{
    public class EthicsAndSafetyModule
    {
        private readonly List<string> _ethicsRules;
        private readonly List<string> _safetyRules;

        public EthicsAndSafetyModule()
        {
            _ethicsRules = new List<string>();
            _safetyRules = new List<string>();
        }

        // Add an ethics rule
        public void AddEthicsRule(string rule)
        {
            _ethicsRules.Add(rule);
        }

        // Add a safety rule
        public void AddSafetyRule(string rule)
        {
            _safetyRules.Add(rule);
        }

        // Check if an action complies with ethics rules
        public bool CheckEthicsCompliance(string action)
        {
            // Simple compliance check for demonstration purposes
            foreach (var rule in _ethicsRules)
            {
                if (action.Contains(rule))
                {
                    Console.WriteLine($"Ethics Violation Detected: {rule}");
                    return false;
                }
            }
            return true;
        }

        // Check if an action complies with safety rules
        public bool CheckSafetyCompliance(string action)
        {
            // Simple compliance check for demonstration purposes
            foreach (var rule in _safetyRules)
            {
                if (action.Contains(rule))
                {
                    Console.WriteLine($"Safety Violation Detected: {rule}");
                    return false;
                }
            }
            return true;
        }
    }
}
