using AutoMapper;
using webapi.Models;
using webapi.Models.DTO.AddressDTO;

namespace webapi.Profiles
{
    public class AddressPF:Profile
    {
        public AddressPF()
        {
            CreateMap<AddressCreateDto, Address>();
            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressUpdateDto, Address>();
        }
    }
}
