using webapi.Models.DTO.ExerciseDTO;

namespace webapi.Models.DTO.ContributionrequestDTO
{
    public class ContributionrequestReadDto
    {
        public int Id { get; set; }

        public int FkUserProfileId { get; set; }

        public virtual UserProfile FkUserProfile { get; set; } = null!;
    }
}
