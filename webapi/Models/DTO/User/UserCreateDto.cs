namespace webapi.Models.DTO.User
{
    public class UserCreateDto
    {        
        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;
    }
}
