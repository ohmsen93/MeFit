using AutoMapper;
using webapi.Models.DTO.SetDTO;
using webapi.Models;
using webapi.Models.DTO.UserDTO;

namespace webapi.Profiles
{
    public class UserPF:Profile
    {
        public UserPF()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReadDto>()
                .ForMember(dto => dto.UserProfiles, options =>
                options.MapFrom(userDomain => userDomain.UserProfiles.Select(userProfile => $"api/v1/userProfiles/{userProfile.Id}").ToList()));
            CreateMap<UserUpdateDto, User>();
        }
    }
}
