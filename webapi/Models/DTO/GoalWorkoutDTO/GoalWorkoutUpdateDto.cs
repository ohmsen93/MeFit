namespace webapi.Models.DTO.GoalWorkoutDTO
{
    public class GoalWorkoutUpdateDto
    {
        public int Id { get; set; }
        public int FkGoalId { get; set; }
        public int FkWorkoutId { get; set; }
        public int FkStatusId { get; set; }
    }
}
