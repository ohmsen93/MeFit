using NuGet.Protocol.Core.Types;

namespace webapi.Models.DTO.UserDto
{
    public class UserReadDto
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;
        public List<string> UserProfiles { get; set; }
    }
}
