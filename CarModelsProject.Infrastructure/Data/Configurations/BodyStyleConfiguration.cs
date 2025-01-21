using CarModelsProject.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarModelsProject.Infrastructure.Data.Configurations
{
    internal class BodyStyleConfiguration : IEntityTypeConfiguration<BodyStyle>
    {
        public void Configure(EntityTypeBuilder<BodyStyle> builder)
        {
            builder.HasData(
                new BodyStyle { BodyStyleId = 1, Name = "Седан" },
                new BodyStyle { BodyStyleId = 2, Name = "Хэтчбек" },
                new BodyStyle { BodyStyleId = 3, Name = "Универсал" },
                new BodyStyle { BodyStyleId = 4, Name = "Минивэн" },
                new BodyStyle { BodyStyleId = 5, Name = "Внедорожник" },
                new BodyStyle { BodyStyleId = 6, Name = "Купе" }
            );
        }
    }
}
