namespace UrlShortener.Client.DTOs
{
    public class GetUrlsDto
    {
        public string UrlLong { get; set; } = string.Empty;
        public string UrlShort { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int Counter { get; set; }
    }
}
