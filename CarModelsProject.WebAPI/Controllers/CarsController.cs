using AutoMapper;

using CarModelsProject.Application.DTOs;
using CarModelsProject.Application.Mappings;
using CarModelsProject.Application.Services;
using CarModelsProject.Core.Entities;
using CarModelsProject.Infrastructure.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarModelsProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarsController> _logger;
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;
        private readonly IMappingHelper _helper;
        public CarsController(ICarService carService, 
                              ILogger<CarsController> logger, 
                              ApplicationDbContext context, 
                              CacheService cacheService, 
                              IMapper mapper,
                              IMappingHelper helper)
        {
            _carService = carService;
            _logger = logger;
            _context = context;
            _cacheService = cacheService;
            _mapper = mapper;
            _helper = helper;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetCars([FromQuery] PaginationQuery paginationQuery)
        {
            var response = await _carService.GetCarsAsync(paginationQuery);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> PostCar([FromBody] CarDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Car car = await _helper.MappingAttributesAsync(carDto);

            _context.Cars.Add(car);

            await _context.SaveChangesAsync();

            _cacheService.InvalidateCache(Constants.CarsCacheKey);

            return Ok(car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCar(int id, [FromBody] CarDto carDto)
        {
            if (id == 0)
            {
                return NotFound();
            }           

            if (!ModelState.IsValid || id != carDto.CarId)
            {
                return BadRequest(ModelState);
            }

            Car car = await _helper.MappingAttributesAsync(carDto);

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                _context.Cars.Update(car);
                _context.Entry(car).Property(x => x.CreatedAt).IsModified = false;
                await _context.SaveChangesAsync();
                _cacheService.InvalidateCache(Constants.CarsCacheKey);
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!_context.Cars.Any(c => c.CarId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            _cacheService.InvalidateCache(Constants.CarsCacheKey);

            return NoContent();
        }
    }
}
