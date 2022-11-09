namespace UrlShortener.API.DTOs
{
    public class EditUrlDto
    {
        public string UrlLong { get; set; } = string.Empty;
        public string UrlShort { get; set; } = string.Empty;
        public int Counter { get; set; }
    }
}
