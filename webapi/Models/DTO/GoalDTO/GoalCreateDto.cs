namespace webapi.Models.DTO.GoalDTO
{
    public class GoalCreateDto
    {                
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? FkTrainingprogramId { get; set; }
        public List<int> Workouts { get; set; }
    }
}
