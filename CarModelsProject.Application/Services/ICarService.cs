using CarModelsProject.Application.DTOs;

namespace CarModelsProject.Application.Services
{
    public interface ICarService
    {
        Task<PagedResponse<CarDto>> GetCarsAsync(PaginationQuery paginationQuery);
    }
}
