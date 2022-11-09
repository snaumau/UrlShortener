using Microsoft.EntityFrameworkCore;
using UrlShortener.Core.Data;
using UrlShortener.Models.Entities;
using UrlShortener.Models.Interfaces;

namespace UrlShortener.Core.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlShortenerContext _context;

        public UrlRepository(UrlShortenerContext context)
        {
            _context = context;
        }

        public IQueryable<Url> GetAllUrl => _context.Urls;

        public Url GetUrl(int? id)
        {
            return _context.Urls.Find(id);
        }

        public Url GetUrl(string id)
        {
            var urlShort = _context.Urls.Where(url => url.UrlShort == $"{id}");
            Url url = urlShort.FirstOrDefault();
            return _context.Urls.Find(url.Id);
        }

        public int PostUrl(Url url)
        {
            _context.Urls.Add(url);
            return _context.SaveChanges();
        }

        public int PutUrl(Url url)
        {
            _context.Entry(url).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public int DeleteUrl(Url url)
        {
            _context.Entry(new Url { Id = url.Id }).State = EntityState.Deleted;
            return _context.SaveChanges();
        }
    }
}
