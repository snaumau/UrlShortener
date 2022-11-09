using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using UrlShortener.Core.Data;
using UrlShortener.Core.Repositories;
using UrlShortener.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UrlShortenerContext>(options =>
    options.UseMySql(
        connection,
        ServerVersion.Create(
            new Version(10, 3),
            ServerType.MariaDb),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
        ));

builder.Services.AddScoped<IUrlRepository, UrlRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var sevices = scope.ServiceProvider;
    var context = sevices.GetRequiredService<UrlShortenerContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();