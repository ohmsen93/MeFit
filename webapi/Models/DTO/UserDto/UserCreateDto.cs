namespace webapi.Models.DTO.UserDTO
{
    public class UserCreateDto
    {        
        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;

        public bool FirstLogin { get; set; }

    }
}
