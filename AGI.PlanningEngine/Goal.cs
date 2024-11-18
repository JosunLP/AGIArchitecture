
namespace AGI.PlanningEngine;

public class Goal
{
    public required string Description { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? Deadline { get; set; } // New attribute for deadlines
    public List<Goal> Dependencies { get; set; } = new(); // New attribute for goal dependencies
    public List<string> RequiredResources { get; set; } = new(); // New attribute for resource management
    public bool IsCompleted { get; set; } = false; // Track goal completion status
}
