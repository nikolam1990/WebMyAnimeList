using Microsoft.EntityFrameworkCore;
using System;
using WebMyAnimeList.Data;
using WebMyAnimeList.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AnimeStudioService>();
builder.Services.AddScoped<AnimeService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("swagger/openapi/v1.json");
    app.UseSwaggerUI(options => options.SwaggerEndpoint("openapi/v1.json", "Anime API"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
