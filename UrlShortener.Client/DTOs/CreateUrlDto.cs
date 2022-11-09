namespace UrlShortener.Client.DTOs
{
    public class CreateUrlDto
    {
        public string UrlLong { get; set; } = string.Empty;
        public string UrlShort { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Counter { get; set; } = default;
    }
}