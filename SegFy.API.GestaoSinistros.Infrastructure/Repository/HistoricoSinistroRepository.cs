using Microsoft.EntityFrameworkCore;
using SegFy.API.GestaoSinistros.Application.Repository;
using SegFy.API.GestaoSinistros.Domain.Sinistros;
using SegFy.API.GestaoSinistros.Infrastructure.Context;

namespace SegFy.API.GestaoSinistros.Infrastructure.Repository
{
    public class HistoricoSinistroRepository : IHistoricoSinistroRepository
    {
        private readonly AppDbContext _appDbContext;

        public HistoricoSinistroRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ICollection<HistoricoSinistros>> BuscaHistoricoSinistroAsync(int sinistroId)
        {
            return await _appDbContext.HistoricoSinistros.Where(x => x.SinistroId == sinistroId).OrderBy(x => x.DataAtualizacao).ToListAsync();
        }
    }
}
