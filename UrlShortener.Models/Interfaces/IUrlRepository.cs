using UrlShortener.Models.Entities;

namespace UrlShortener.Models.Interfaces
{
    public interface IUrlRepository
    {
        IQueryable<Url> GetAllUrl { get; }
        Url GetUrl(int? id);
        Url GetUrl(string? id);
        int PostUrl(Url url);
        int PutUrl(Url url);
        int DeleteUrl(Url url);
    }
}
