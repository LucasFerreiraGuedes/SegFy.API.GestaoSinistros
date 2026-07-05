using Microsoft.EntityFrameworkCore;
using SegFy.API.GestaoSinistros.Application.Repository;
using SegFy.API.GestaoSinistros.Domain.Sinistros;
using SegFy.API.GestaoSinistros.Infrastructure.Context;

namespace SegFy.API.GestaoSinistros.Infrastructure.Repository
{
    public class SinistroRepository : ISinistroRepository
    {
        private readonly AppDbContext _appDbContext;

        public SinistroRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IQueryable<Sinistro> Query()
        {
            return _appDbContext.Sinistros
                .AsNoTracking();
        }

        public async Task<Sinistro?> BuscaSinistroPorIdAsync(int id)
        {
            return await _appDbContext.Sinistros.Include(x => x.Apolice).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task CriarSinistroAsync(Sinistro sinistro)
        {
            await _appDbContext.Sinistros.AddAsync(sinistro);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task AtualizaSinistroAsync(Sinistro sinistro)
        {
             _appDbContext.Sinistros.Update(sinistro);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
