namespace webapi.Models.DTO.GoalDTO
{
    public class GoalUpdateDto
    {
        public int Id { get; set; }

        public int FkUserProfileId { get; set; }

        public DateTime EndDate { get; set; }

        public bool Achived { get; set; }

        public int? FkTrainingprogramId { get; set; }

        public int FkStatusId { get; set; }        
    }
}
