namespace webapi.Models.DTO.GoalWorkoutDTO
{
    public class GoalWorkoutCreateDto
    {
        public int FkGoalId { get; set; }
        public int FkWorkoutId { get; set; }
        public int FkStatusId { get; set; }
    }
}
