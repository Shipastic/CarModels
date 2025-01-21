using AutoMapper;

using CarModelsProject.Application.DTOs;
using CarModelsProject.Application.Mappings;
using CarModelsProject.Core.Entities;
using CarModelsProject.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace CarModelsProject.Infrastructure.Helpers
{
    public class MappingHelper : IMappingHelper
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public MappingHelper(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Car> MappingAttributesAsync(CarDto carDto)
        {
            var carDb = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == carDto.CarId);

            Car car = new Car();

            if (carDb != null)
            {
                car = _mapper.Map(carDto, carDb);
            }
            else
            {
                car = _mapper.Map<Car>(carDto);

                car.CreatedAt = DateTime.UtcNow;

                car.BodyStyleId = await _context.BodyStyles
                                                    .Where(bsn => bsn.Name.Equals(carDto.bodyStyleName))
                                                    .Select(bsn => bsn.BodyStyleId)
                                                    .FirstOrDefaultAsync();

                if (car.BodyStyleId == 0)
                {
                    throw new Exception("BodyStyle not found");
                }

                car.BrandId = await _context.Brands
                                                .Where(bn => bn.Name.Equals(carDto.brandName))
                                                .Select(bn => bn.BrandId)
                                                .FirstOrDefaultAsync();

                if (car.BrandId == 0)
                {
                    throw new Exception("Brand not found");
                }
            }
            
            return car;
        }
    }
}
