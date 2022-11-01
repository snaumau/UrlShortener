using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace UrlShortener.Core.Data
{
    public class UrlShortenerContextFactory : IDesignTimeDbContextFactory<UrlShortenerContext>
    {
        public UrlShortenerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UrlShortenerContext>();
            optionsBuilder.UseMySql(
                @"Server=localhost;Port=3307;Database=UrlShortener;Uid=root;Pwd=04081202;",
                ServerVersion.Create(
                    new Version(10, 3, 36),
                    ServerType.MariaDb),
                options => options.EnableRetryOnFailure());

            return new UrlShortenerContext(optionsBuilder.Options);
        }
    }
}
