using AutoMapper;
using webapi.Models;
using webapi.Models.DTO.Set;

namespace webapi.Profiles
{
    public class SetProfile:Profile
    {
        public SetProfile()
        {
            CreateMap<SetCreateDto, Set>();
        }
    }
}
