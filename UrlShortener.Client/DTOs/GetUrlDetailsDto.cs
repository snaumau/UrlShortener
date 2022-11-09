namespace UrlShortener.Client.DTOs
{
    public class GetUrlDetailsDto
    {
        public int Id { get; set; }
        public string UrlShort { get; set; } = string.Empty;
    }
}
