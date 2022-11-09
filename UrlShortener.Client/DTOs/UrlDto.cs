﻿using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Client.DTOs
{
    public class UrlDto
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
        [Display(Name = "Date")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Please enter count")]
        [Range(1, int.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Please enter number of counter")]
        public int Counter { get; set; }
    }
}