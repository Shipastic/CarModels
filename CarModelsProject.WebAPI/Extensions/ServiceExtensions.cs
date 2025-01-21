using CarModelsProject.Application.Mappings;
using CarModelsProject.Application.Services;
using CarModelsProject.Infrastructure.Data;
using CarModelsProject.Infrastructure.Helpers;
using CarModelsProject.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;

namespace CarModelsProject.WebAPI.Extensions
{
    public static class ServiceExtensions 
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CarModelsDbConnection")));

            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IMappingHelper, MappingHelper>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSingleton(typeof(CacheService));

            services.AddMemoryCache();
        }
    }
}
