using Microsoft.EntityFrameworkCore;
using SegFy.API.GestaoSinistros.Domain.Apolices;
using SegFy.API.GestaoSinistros.Domain.Clientes;
using SegFy.API.GestaoSinistros.Domain.Ramos;
using SegFy.API.GestaoSinistros.Domain.Sinistros;

namespace SegFy.API.GestaoSinistros.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Apolice> Apolices { get; set; }
        public DbSet<Ramo> Ramos { get; set; }
        public DbSet<Sinistro> Sinistros { get; set; }
        public DbSet<HistoricoSinistros> HistoricoSinistros { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
