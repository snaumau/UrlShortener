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
                @"Server=[SERVERNAME];Port=[PORT];Database=UrlShortener;Uid=[USERNAME];Pwd=[PASSWORD];",
                ServerVersion.Create(
                    new Version(10, 3, 36),
                    ServerType.MariaDb),
                options => options.EnableRetryOnFailure());

            return new UrlShortenerContext(optionsBuilder.Options);
        }
    }
}
