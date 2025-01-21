using CarModelsProject.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarModelsProject.Infrastructure.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(
                new Brand { BrandId = 1, Name = "Audi" },
                new Brand { BrandId = 2, Name = "Ford" },
                new Brand { BrandId = 3, Name = "Jeep" },
                new Brand { BrandId = 4, Name = "Nissan" },
                new Brand { BrandId = 5, Name = "Toyota" }
            );
        }
    }
}
