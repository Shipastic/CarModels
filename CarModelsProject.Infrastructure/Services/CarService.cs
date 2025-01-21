using AutoMapper;

using CarModelsProject.Application.DTOs;
using CarModelsProject.Application.Services;
using CarModelsProject.Core.Entities;
using CarModelsProject.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace CarModelsProject.Infrastructure.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly CacheService _cacheService;
        public CarService(ApplicationDbContext context, 
                          IMapper mapper, 
                          CacheService cacheService)
        {
            _context = context;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        private async Task<IEnumerable<Car>> GetPagedCarsAsync(int pageNumber, int pageSize)
        {
            return await _context.Cars
                                    .AsNoTracking()
                                    .Include(c => c.Brand)
                                    .Include(c => c.BodyStyle)
                                    .OrderBy(c => c.CarId)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
        }
        public async Task<PagedResponse<CarDto>> GetCarsAsync(PaginationQuery paginationQuery)
        {
            var cacheKey = $"{Constants.CarsCacheKey}_{paginationQuery.PageNumber}_{paginationQuery.PageSize}";

            if (_cacheService.TryGetValue(cacheKey, out PagedResponse<CarDto> cachedCars))
            { 
                return cachedCars;
            }

            var totalItems = await _context.Cars.CountAsync();

            var cars = await GetPagedCarsAsync(paginationQuery.PageNumber, paginationQuery.PageSize);

            var carDtos = _mapper.Map<IEnumerable<CarDto>>(cars);

            var pagedResponse = new PagedResponse<CarDto>(carDtos, 
                                                          totalItems, 
                                                          paginationQuery.PageNumber, 
                                                          paginationQuery.PageSize);
            _cacheService.Set(cacheKey, pagedResponse);

            return pagedResponse;

        }
    }
}
