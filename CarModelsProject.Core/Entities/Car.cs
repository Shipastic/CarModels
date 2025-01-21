using System.ComponentModel.DataAnnotations;

namespace CarModelsProject.Core.Entities
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        public  string BrandName{ get; set; }

        [Required]
        public  string BodyStyleName { get; set; }
        public int BodyStyleId { get; set; }
        public int BrandId { get; set; }
        public  Brand Brand { get; set; } = null!;
        public  BodyStyle BodyStyle { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public  string ModelName { get; set; }

        [Required]
        public  DateTime CreatedAt { get; set; } 

        [Required]
        [Range(1, 12, ErrorMessage = "Количество мест должно быть от 1 до 12.")]
        public  int SeatsCount { get; set; }

        [MaxLength(1000)]
        [Url]
        [RegularExpression(@".*\.ru$", ErrorMessage = "Сайт должен быть в домене .ru")]
        public  string DealerUrl { get; set; }
    }
}
