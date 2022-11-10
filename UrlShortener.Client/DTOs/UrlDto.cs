using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Client.DTOs
{
    public class UrlDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter you url")]
        [DataType(DataType.Url)]
        public string UrlLong { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter short url")]
        public string UrlShort { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Time)]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Please enter count")]
        [Range(1, int.MaxValue)]
        [DataType(DataType.Currency)]
        public int Counter { get; set; }
    }
}
