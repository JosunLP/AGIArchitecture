
using System;
using System.Collections.Generic;

namespace AGI.SelfMonitoring
{
    public class SelfHealingSystem
    {
        private Dictionary<string, Func<bool>> SelfHealingStrategies;

        public SelfHealingSystem()
        {
            SelfHealingStrategies = new Dictionary<string, Func<bool>>();
            RegisterDefaultStrategies();
        }

        private void RegisterDefaultStrategies()
        {
            // Example strategy: Restart a specific component
            SelfHealingStrategies.Add("RestartComponent", () =>
            {
                Console.WriteLine("Attempting to restart component...");
                // Simulate restarting process and assume it is successful
                bool success = true;
                return success;
            });

            // Example strategy: Clear Cache
            SelfHealingStrategies.Add("ClearCache", () =>
            {
                Console.WriteLine("Attempting to clear cache...");
                // Simulate cache clearing and assume it is successful
                bool success = true;
                return success;
            });
        }

        public void TriggerSelfHealing(string issue)
        {
            if (SelfHealingStrategies.ContainsKey(issue))
            {
                bool result = SelfHealingStrategies[issue].Invoke();
                Console.WriteLine(result ? "Self-Healing successful." : "Self-Healing failed.");
            }
            else
            {
                Console.WriteLine($"No self-healing strategy found for issue: {issue}");
            }
        }

        public void AddCustomSelfHealingStrategy(string name, Func<bool> strategy)
        {
            if (!SelfHealingStrategies.ContainsKey(name))
            {
                SelfHealingStrategies.Add(name, strategy);
                Console.WriteLine($"Custom self-healing strategy '{name}' added successfully.");
            }
            else
            {
                Console.WriteLine($"A strategy with the name '{name}' already exists.");
            }
        }
    }
}
