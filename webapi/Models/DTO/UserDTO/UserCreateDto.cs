namespace webapi.Models.DTO.UserDTO
{
    public class UserCreateDto
    {
        public string Id { get; set; }

        public string Username { get; set; } = null!;

        public bool FirstLogin { get; set; }

    }
}
