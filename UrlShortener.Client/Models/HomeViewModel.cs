using UrlShortener.Models.Entities;

namespace UrlShortener.Client.Models
{
    public class HomeViewModel
    {
        public IQueryable<Url>? Urls { get; set; }
    }
}
