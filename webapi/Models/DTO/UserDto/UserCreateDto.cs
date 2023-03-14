namespace webapi.Models.DTO.UserDTO
{
    public class UserCreateDto
    {        

        public string Username { get; set; } = null!;

        public bool FirstLogin { get; set; }

    }
}
