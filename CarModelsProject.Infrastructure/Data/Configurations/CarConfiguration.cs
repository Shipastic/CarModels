using CarModelsProject.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarModelsProject.Infrastructure.Data.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.CarId);


            builder.Property(c => c.ModelName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(c => c.Brand)
                   .WithMany(b => b.Cars)
                   .HasForeignKey(c => c.BrandId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(c => c.BodyStyle)
                   .WithMany(bt => bt.Cars)
                   .HasForeignKey(c => c.BodyStyleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
               new Car { CarId = 1,  BrandId = 1, BrandName = "Audi",   BodyStyleId = 1, BodyStyleName = "Седан",       ModelName = "A4",             DealerUrl = "https://new-audi.ru",         SeatsCount = 4, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 2,  BrandId = 2, BrandName = "Ford",   BodyStyleId = 6, BodyStyleName = "Купе",        ModelName = "Mustang",        DealerUrl = "https://ford-russia.ru",      SeatsCount = 2, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 3,  BrandId = 4, BrandName = "Nissan", BodyStyleId = 5, BodyStyleName = "Внедорожник", ModelName = "Juke",           DealerUrl = "https://www.major-nissan.ru", SeatsCount = 5, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 4,  BrandId = 5, BrandName = "Toyota", BodyStyleId = 1, BodyStyleName = "Седан",       ModelName = "Camry",          DealerUrl = "https://www.toyota.ru",       SeatsCount = 4, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 5,  BrandId = 3, BrandName = "Jeep",   BodyStyleId = 5, BodyStyleName = "Внедорожник", ModelName = "Grand Cherokee", DealerUrl = "https://jeep-avilon.ru",      SeatsCount = 5, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 6,  BrandId = 2, BrandName = "Ford",   BodyStyleId = 2, BodyStyleName = "Хэтчбек",     ModelName = "Focus 2",        DealerUrl = "https://ford-russia.ru",      SeatsCount = 4, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 7,  BrandId = 1, BrandName = "Audi",   BodyStyleId = 5, BodyStyleName = "Внедорожник", ModelName = "Q6",             DealerUrl = "https://new-audi.ru",         SeatsCount = 5, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 8,  BrandId = 4, BrandName = "Nissan", BodyStyleId = 5, BodyStyleName = "Внедорожник", ModelName = "X-Trail",        DealerUrl = "https://www.major-nissan.ru", SeatsCount = 5, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 9,  BrandId = 5, BrandName = "Toyota", BodyStyleId = 1, BodyStyleName = "Седан",       ModelName = "COROLLA",        DealerUrl = "https://www.toyota.ru",       SeatsCount = 4, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 10, BrandId = 3, BrandName = "Jeep",   BodyStyleId = 5, BodyStyleName = "Внедорожник", ModelName = "Wrangler",       DealerUrl = "https://jeep-avilon.ru",      SeatsCount = 5, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 11, BrandId = 2, BrandName = "Ford",   BodyStyleId = 5, BodyStyleName = "Внедорожник", ModelName = "Bronco",         DealerUrl = "https://ford-russia.ru",      SeatsCount = 5, CreatedAt = DateTime.UtcNow },
               new Car { CarId = 12, BrandId = 4, BrandName = "Nissan", BodyStyleId = 5, BodyStyleName = "Внедорожник", ModelName = "Patrol",         DealerUrl = "https://www.major-nissan.ru", SeatsCount = 5, CreatedAt = DateTime.UtcNow });
        }
    }
}
