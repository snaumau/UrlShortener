using UrlShortener.Client.DTOs;

namespace UrlShortener.Client.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<UrlDto>? Urls { get; set; }
    }
}
