using CarModelsProject.Application.DTOs;
using CarModelsProject.Core.Entities;

namespace CarModelsProject.Application.Mappings
{
    public interface IMappingHelper
    {
        Task<Car> MappingAttributesAsync(CarDto carDto);
    }
}
