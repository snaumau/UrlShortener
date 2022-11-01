using UrlShortener.Models.Entities;

namespace UrlShortener.Client.ViewModels
{
    public class HomeViewModel
    {
        public IQueryable<Url>? Urls { get; set; }
    }
}
