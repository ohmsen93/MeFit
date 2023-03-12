<<<<<<<< HEAD:webapi/Models/DTO/UserProfileDTO/UserProfileUpdateDto.cs
﻿namespace webapi.Models.DTO.UserProfileDTO
========
﻿namespace webapi.Models.DTO.UserProfileDto
>>>>>>>> origin/API-03-User:webapi/Models/DTO/UserProfileDto/UserProfileUpdateDto.cs
{
    public class UserProfileUpdateDto
    {
        public int Id { get; set; }
<<<<<<<< HEAD:webapi/Models/DTO/UserProfileDTO/UserProfileUpdateDto.cs
========
        public int FkUserId { get; set; }
>>>>>>>> origin/API-03-User:webapi/Models/DTO/UserProfileDto/UserProfileUpdateDto.cs
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
