using System.ComponentModel.DataAnnotations;

namespace CarModelsProject.Core.Entities
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }        
        public string? Name { get; set; }
        public ICollection<Car>? Cars { get; set; }
    }
}
