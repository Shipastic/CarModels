using CarModelsProject.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace CarModelsProject.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<BodyStyle> BodyStyles { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
