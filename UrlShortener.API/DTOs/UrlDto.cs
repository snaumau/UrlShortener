namespace UrlShortener.API.DTOs
{
    public class UrlDto
    {
        public int Id { get; set; }
        public string UrlLong { get; set; } = string.Empty;
        public string UrlShort { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int Counter { get; set; }
    }
}
