namespace UrlShortener.Client.DTOs
{
    public class CreateUrlDto
    {
        public string UrlLong { get; set; } = string.Empty;
        public string UrlShort { get; set; } = string.Empty;
    }
}