﻿namespace UrlShortener.API.DTOs
{
    public class GetUrlDetailsDto
    {
        public int Id { get; set; }
        public string UrlShort { get; set; } = string.Empty;
    }
}
