using AutoMapper;
using webapi.Models.DTO.CategoryDTO;
using webapi.Models;

namespace webapi.Profiles
{
    public class CategoryPF:Profile
    {
        public CategoryPF()
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryReadDto>()
                .ForMember(dto => dto.Trainingprograms, options =>
                options.MapFrom(setDomain => setDomain.Trainingprograms.Select(tp => tp.Id).ToList()));
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
