using System.ComponentModel.DataAnnotations;

namespace CarModelsProject.Application.DTOs
{
    public class CarDto
    {
        public int CarId { get; set; }
        public string? brandName { get; set; }
        public string? bodyStyleName { get; set; }
        
        [MaxLength(1000)]
        public string modelName { get; set; } = null!;

        [Range(1, 12, ErrorMessage = "Количество мест должно быть от 1 до 12.")]
        public int seatsCount { get; set; }

        [MaxLength(1000)]
        [Url]
        [RegularExpression(@".*\.ru$", ErrorMessage = "Сайт должен быть в домене .ru")]
        public string? dealerUrl { get; set; }
    }
}
