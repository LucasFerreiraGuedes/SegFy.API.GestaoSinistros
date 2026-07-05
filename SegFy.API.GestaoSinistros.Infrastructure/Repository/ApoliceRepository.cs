using Microsoft.EntityFrameworkCore;
using SegFy.API.GestaoSinistros.Application.Repository;
using SegFy.API.GestaoSinistros.Domain.Apolices;
using SegFy.API.GestaoSinistros.Infrastructure.Context;

namespace SegFy.API.GestaoSinistros.Infrastructure.Repository
{
    public class ApoliceRepository : IApoliceRepository
    {
        private readonly AppDbContext _appDbContext;

        public ApoliceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Apolice?> BuscarApoliceNumero(string numero)
        {
            var consultaNumero = numero.Trim().ToUpperInvariant();

            return await _appDbContext.Apolices.SingleOrDefaultAsync(x => x.Numero == consultaNumero);
        }
    }
}
