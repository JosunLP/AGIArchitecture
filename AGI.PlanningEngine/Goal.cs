namespace AGI.PlanningEngine;

public class Goal
{
    public required string Description { get; set; }
    public int Priority { get; set; }
    public DateTime CreatedAt { get; set; }
}
