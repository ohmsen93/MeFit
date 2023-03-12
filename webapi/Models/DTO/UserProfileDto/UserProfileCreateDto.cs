namespace webapi.Models.DTO.UserProfileDTO
 {
    public class UserProfileCreateDto
    {        
        public int FkUserId { get; set; }

        public int FkAddressId { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public string? MedicalCondition { get; set; }

        public string? Disabilities { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public int Phone { get; set; }

        public string? Picture { get; set; }

        public string Email { get; set; } = null!;
    }
}
