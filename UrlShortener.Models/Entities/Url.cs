namespace UrlShortener.Models.Entities
{
    public class Url
    {
        public int Id { get; set; }
        public string UrlLong { get; set; } = string.Empty;
        public string UrlShort { get; set; } = string.Empty;
        public int Counter { get; set; }
    }
}