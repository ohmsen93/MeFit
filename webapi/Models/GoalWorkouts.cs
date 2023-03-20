namespace webapi.Models
{
    public class GoalWorkouts
    {
        public int Id { get; set; }
        public int FkGoalId { get; set; }
        public Goal FkGoal { get; set; }
        public int FkWorkoutId { get; set; }
        public Workout FkWorkout { get; set; }
        public int FkStatusId { get; set; }
        public Status FkStatus { get; set; }
    }
}
