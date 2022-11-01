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
                @"Server=localhost;Port=3307;Database=UrlShortener;Uid=root;Pwd=04081202;",
                ServerVersion.Create(
                    new Version(10, 3, 36),
                    ServerType.MariaDb),
                options => options.EnableRetryOnFailure());
        }
    }
}
