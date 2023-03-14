namespace webapi.Models.DTO.GoalDTO
{
    public class GoalReadDto
    {
        public int Id { get; set; }

        public int FkUserProfileId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? FkTrainingprogramId { get; set; }

        public int FkStatusId { get; set; }       

        public List<string> Workouts { get; set; }
    }
}
