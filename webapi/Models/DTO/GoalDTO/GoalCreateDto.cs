namespace webapi.Models.DTO.GoalDTO
{
    public class GoalCreateDto
    {        
        public int FkUserProfileId { get; set; }

        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }

        public int? FkTrainingprogramId { get; set; }

        public int FkStatusId { get; set; }
    }
}
