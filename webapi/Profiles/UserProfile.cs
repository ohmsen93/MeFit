using AutoMapper;
using webapi.Models.DTO.Set;
using webapi.Models;
using webapi.Models.DTO.UserDto;

namespace webapi.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReadDto>()
                .ForMember(dto => dto.UserProfiles, options =>
                options.MapFrom(userDomain => userDomain.UserProfiles.Select(userProfile => $"api/v1/userProfiles/{userProfile.Id}").ToList()));
            CreateMap<UserUpdateDto, User>();
        }
    }
}
