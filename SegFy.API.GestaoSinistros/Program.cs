using Microsoft.EntityFrameworkCore;
using SegFy.API.GestaoSinistros.Application.Interfaces;
using SegFy.API.GestaoSinistros.Application.Repository;
using SegFy.API.GestaoSinistros.Application.Services;
using SegFy.API.GestaoSinistros.Infrastructure.Context;
using SegFy.API.GestaoSinistros.Infrastructure.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(
                         new JsonStringEnumConverter());
                 });;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));
builder.Services.AddScoped<IHistoricoService, HistoricoService>();
builder.Services.AddScoped<ISinistroService, SinistroService>();
builder.Services.AddScoped<IApoliceRepository, ApoliceRepository>();
builder.Services.AddScoped<IHistoricoSinistroRepository, HistoricoSinistroRepository>();
builder.Services.AddScoped<ISinistroRepository, SinistroRepository>();

var app = builder.Build();

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
