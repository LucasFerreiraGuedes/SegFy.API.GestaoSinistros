using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SegFy.API.GestaoSinistros.Application.Interfaces;
using SegFy.API.GestaoSinistros.Application.Repository;
using SegFy.API.GestaoSinistros.Application.Services;
using SegFy.API.GestaoSinistros.Infrastructure.Context;
using SegFy.API.GestaoSinistros.Infrastructure.Repository;

namespace SegFy.API.GestaoSinistros.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DbConnectionString")));

            // Services
            services.AddScoped<IHistoricoService, HistoricoService>();
            services.AddScoped<ISinistroService, SinistroService>();

            // Repositories
            services.AddScoped<IApoliceRepository, ApoliceRepository>();
            services.AddScoped<IHistoricoSinistroRepository, HistoricoSinistroRepository>();
            services.AddScoped<ISinistroRepository, SinistroRepository>();

            return services;
        }
    }
}