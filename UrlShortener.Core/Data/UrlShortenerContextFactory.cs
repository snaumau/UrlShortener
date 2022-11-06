using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace UrlShortener.Core.Data
{
    public class UrlShortenerContextFactory : IDesignTimeDbContextFactory<UrlShortenerContext>
    {
        public UrlShortenerContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var connectionString = builder.Build()
                .GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<UrlShortenerContext>();
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.Create(
                    new Version(10, 3),
                    ServerType.MariaDb),
                options => options.EnableRetryOnFailure());

            return new UrlShortenerContext(optionsBuilder.Options);
        }
    }
}
