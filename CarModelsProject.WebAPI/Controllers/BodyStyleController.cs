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
    public class BodyStyleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BrandsController> _logger;
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;

        public BodyStyleController(ApplicationDbContext context, 
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
        public async Task<IEnumerable<BodyStyleDto>> GetBodyStyles()
        {
            var cacheKey = Constants.BodyStyleCacheKey;

            if (_cacheService.TryGetValue(cacheKey, out IEnumerable<BodyStyleDto> cachedBodyStyles))
            {
                return cachedBodyStyles;
            }
            var response = await _context.BodyStyles.ToListAsync();
            var bodyStylesDtos = _mapper.Map<IEnumerable<BodyStyleDto>>(response);
            _cacheService.Set(cacheKey, bodyStylesDtos);

            return bodyStylesDtos;
        }
    }
}
