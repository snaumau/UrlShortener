using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using UrlShortener.Models.Entities;

namespace UrlShortener.Core.Data
{
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options) { }

        public DbSet<Url> Urls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                @"Server=[SERVERNAME];Port=[PORT];Database=UrlShortener;Uid=[USERNAME];Pwd=[PASSWORD];",
                ServerVersion.Create(
                    new Version(10, 3, 36),
                    ServerType.MariaDb),
                options => options.EnableRetryOnFailure());
        }
    }
}
