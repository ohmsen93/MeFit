namespace webapi.Models.DTO.UserDto
{
    public class UserCreateDTO
    {        
        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;
    }
}
