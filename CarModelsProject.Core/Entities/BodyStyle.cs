using System.ComponentModel.DataAnnotations;

namespace CarModelsProject.Core.Entities
{
    public class BodyStyle
    {
        [Key]
        public int BodyStyleId { get; set; }      
        public string? Name { get; set; }
        public ICollection<Car>? Cars { get; set; }
    }
}
