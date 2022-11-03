using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models.Entities
{
    public class Url
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter you url")]
        [DataType(DataType.Url)]
        [Display(Name = "Your url")]
        public string UrlLong { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter short url")]
        [DataType(DataType.Url)]
        [Display(Name = "Short url")]
        public string UrlShort { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter data")]
        [DataType(DataType.Time)]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Please enter count")]
        [Display(Name = "Please enter number of counter")]
        public int Counter { get; set; }
    }
}