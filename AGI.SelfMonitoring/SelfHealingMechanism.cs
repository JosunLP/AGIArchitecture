
using System;
using System.Collections.Generic;

namespace AGI.SelfMonitoring
{
    public class SelfHealingMechanism
    {
        private Dictionary<string, Func<bool>> HealingActions;

        public SelfHealingMechanism()
        {
            HealingActions = [];
        }

        public void RegisterHealingAction(string issue, Func<bool> action)
        {
            HealingActions[issue] = action;
        }

        public void AttemptSelfHeal(string issue)
        {
            if (HealingActions.ContainsKey(issue))
            {
                bool success = HealingActions[issue].Invoke();
                if (success)
                {
                    Console.WriteLine($"Self-healing successful for issue: {issue}");
                }
                else
                {
                    Console.WriteLine($"Self-healing failed for issue: {issue}");
                }
            }
            else
            {
                Console.WriteLine($"No self-healing action registered for issue: {issue}");
            }
        }
    }
}
