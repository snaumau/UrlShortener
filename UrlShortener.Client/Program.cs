using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using UrlShortener.Core.Data;
using UrlShortener.Core.Repositories;
using UrlShortener.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UrlShortenerContext>(options =>
    options.UseMySql(
        connection,
        ServerVersion.Create(
            new Version(10, 3, 36),
            ServerType.MariaDb),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
        ));

builder.Services.AddScoped<IUrlRepository, UrlRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var sevices = scope.ServiceProvider;
    var context = sevices.GetRequiredService<UrlShortenerContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
