
using System;
using System.Collections.Generic;
using AGI.ReasoningEngine; // Added using directive to include ReasoningEngine namespace

namespace AGI.PlanningEngine
{
    public class PlanningEngine
    {
        private readonly Queue<Goal> _goals;

        public PlanningEngine()
        {
            _goals = new Queue<Goal>();
        }

        // Add a new goal to the planning queue with additional parameters for dynamic prioritization
        public void AddGoal(string description, int priority = 1, DateTime? deadline = null, List<Goal>? dependencies = null, List<string>? requiredResources = null)
        {
            var goal = new Goal
            {
                Description = description,
                Priority = CalculateDynamicPriority(priority, deadline, dependencies),
                Deadline = deadline,
                Dependencies = dependencies ?? new List<Goal>(),
                RequiredResources = requiredResources ?? new List<string>(),
                CreatedAt = DateTime.Now
            };
            _goals.Enqueue(goal);
        }

        // Calculate a dynamic priority based on various factors like deadlines and dependencies
        private int CalculateDynamicPriority(int basePriority, DateTime? deadline, List<Goal>? dependencies)
        {
            int dynamicPriority = basePriority;

            if (deadline.HasValue)
            {
                var daysToDeadline = (deadline.Value - DateTime.Now).TotalDays;
                dynamicPriority += daysToDeadline < 7 ? 5 : 0; // Increase priority if the deadline is approaching
            }

            if (dependencies != null && dependencies.Count > 0)
            {
                dynamicPriority -= dependencies.Count; // Decrease priority if there are dependencies that need to be handled first
            }

            return dynamicPriority;
        }

        // Get the next goal in the queue considering dependencies
        public Goal? GetNextGoal()
        {
            foreach (var goal in _goals)
            {
                if (goal.Dependencies.TrueForAll(d => d.IsCompleted))
                {
                    return goal;
                }
            }
            return null; // No available goal without unfulfilled dependencies
        }

        // Evaluate and provide feedback after goal completion
        public void EvaluateGoalCompletion(Goal completedGoal)
        {
            // Logic for feedback and adjustments based on completed goals
            Console.WriteLine($"Evaluating completion of goal: {completedGoal.Description}");
            // Placeholder: Integrate with ReasoningEngine for analysis
            try
            {
                ReasoningEngine.ReasoningEngine reasoningEngine = new ReasoningEngine.ReasoningEngine();
                var newPriority = reasoningEngine.AnalyzeGoalOutcome(completedGoal.Description);
                completedGoal.Priority = newPriority;
            }
            catch (Exception)
            {
                Console.WriteLine("Warning: ReasoningEngine integration failed. Using default feedback logic.");
                completedGoal.Priority -= 1; // Fallback logic for priority adjustment
            }
        }
    }
}
