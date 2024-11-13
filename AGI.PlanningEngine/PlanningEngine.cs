
namespace AGI.PlanningEngine
{
    public class PlanningEngine
    {
        private readonly Queue<Goal> _goals;

        public PlanningEngine()
        {
            _goals = new Queue<Goal>();
        }

        // Add a new goal to the planning queue
        public void AddGoal(string description, int priority = 1)
        {
            var goal = new Goal { Description = description, Priority = priority, CreatedAt = DateTime.Now };
            _goals.Enqueue(goal);
        }

        // Get the next goal in the queue
        public Goal? GetNextGoal()
        {
            return _goals.Count > 0 ? _goals.Dequeue() : null;
        }

        // List all current goals
        public List<Goal> ListGoals()
        {
            return new List<Goal>(_goals);
        }
    }

    public class Goal
    {
        public required string Description { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
