
using AGI.ReasoningEngine;
using AGI.PlanningEngine;
using AGI.EthicsAndSafety;
using AGI.SelfMonitoring;

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
Console.WriteLine();

// Test Ethics and Safety Module
var ethicsSafetyModule = new EthicsAndSafetyModule();
ethicsSafetyModule.AddEthicsRule("no harm");
ethicsSafetyModule.AddSafetyRule("no data leakage");

Console.WriteLine("Ethics and Safety Module Test:");
string action1 = "Data analysis with no harm";
string action2 = "Send data with possible data leakage";
Console.WriteLine($"Action: {action1} complies with ethics? {ethicsSafetyModule.CheckEthicsCompliance(action1)}");
Console.WriteLine($"Action: {action1} complies with safety? {ethicsSafetyModule.CheckSafetyCompliance(action1)}");
Console.WriteLine($"Action: {action2} complies with ethics? {ethicsSafetyModule.CheckEthicsCompliance(action2)}");
Console.WriteLine($"Action: {action2} complies with safety? {ethicsSafetyModule.CheckSafetyCompliance(action2)}");
Console.WriteLine();

// Test Self-Monitoring Module
var selfMonitoringModule = new SelfMonitoringModule();
selfMonitoringModule.Log("System started");
selfMonitoringModule.Log("Data analysis completed without issues");
selfMonitoringModule.Log("An attempt was made to send data with potential data leakage");

Console.WriteLine("Self-Monitoring Module Test:");
Console.WriteLine("Logs:");
foreach (var log in selfMonitoringModule.GetLogs())
{
    Console.WriteLine(log);
}
