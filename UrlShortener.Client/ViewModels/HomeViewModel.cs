using UrlShortener.Client.DTOs;

namespace UrlShortener.Client.ViewModels
{
    public class HomeViewModel
    {
        public IList<GetUrlsDto>? Urls { get; set; }
    }
}
