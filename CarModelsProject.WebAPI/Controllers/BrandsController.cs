using AutoMapper;

using CarModelsProject.Application.DTOs;
using CarModelsProject.Application.Services;
using CarModelsProject.Infrastructure.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarModelsProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BrandsController> _logger;
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;
        public BrandsController(ApplicationDbContext context, 
                                ILogger<BrandsController> logger, 
                                CacheService cacheService, 
                                IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BrandDto>> GetBrands()
        {
            var cacheKey = Constants.BrandsCacheKey;

            if (_cacheService.TryGetValue(cacheKey, out IEnumerable<BrandDto> cachedBrands))
            {
                return cachedBrands;
            }
            var response = await _context.Brands.ToListAsync();
            var brandDtos = _mapper.Map<IEnumerable<BrandDto>>(response);
            _cacheService.Set(cacheKey, brandDtos);

            return brandDtos;
        }
    }
}
