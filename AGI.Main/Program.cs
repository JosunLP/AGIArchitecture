
using System;
using AGI.ReasoningEngine;
using AGI.PlanningEngine;

// Test Reasoning Engine
var reasoningEngine = new ReasoningEngine();
reasoningEngine.AddRelation("AI", "is a field of", "Computer Science");
reasoningEngine.AddRelation("Machine Learning", "is a subset of", "AI");

Console.WriteLine("Reasoning Engine Test:");
Console.WriteLine($"AI is a field of Computer Science? {reasoningEngine.InferRelation("AI", "is a field of", "Computer Science")}");
Console.WriteLine($"Machine Learning is a subset of AI? {reasoningEngine.InferRelation("Machine Learning", "is a subset of", "AI")}");
Console.WriteLine($"Machine Learning is a field of Computer Science? {reasoningEngine.InferRelation("Machine Learning", "is a field of", "Computer Science")}");
Console.WriteLine();

// Test Planning Engine
var planningEngine = new PlanningEngine();
planningEngine.AddGoal("Learn basics of AI", priority: 1);
planningEngine.AddGoal("Implement a machine learning model", priority: 2);
planningEngine.AddGoal("Understand reasoning engines", priority: 3);

Console.WriteLine("Planning Engine Test:");
Console.WriteLine("Current Goals:");
foreach (var goal in planningEngine.ListGoals())
{
    Console.WriteLine($"- {goal.Description} (Priority: {goal.Priority}, Created: {goal.CreatedAt})");
}

Console.WriteLine("\nFetching Next Goal:");
var nextGoal = planningEngine.GetNextGoal();
if (nextGoal != null)
{
    Console.WriteLine($"Next Goal: {nextGoal.Description} with priority {nextGoal.Priority}");

}
