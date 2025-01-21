using AutoMapper;

using CarModelsProject.Application.DTOs;
using CarModelsProject.Core.Entities;

namespace CarModelsProject.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<BodyStyle, BodyStyleDto>().ReverseMap();
        }
    }
}
