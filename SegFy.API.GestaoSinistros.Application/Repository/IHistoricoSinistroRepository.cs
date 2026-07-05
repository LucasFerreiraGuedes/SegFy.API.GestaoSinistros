using SegFy.API.GestaoSinistros.Domain.Sinistros;

namespace SegFy.API.GestaoSinistros.Application.Repository
{
    public interface IHistoricoSinistroRepository
    {
        public Task<ICollection<HistoricoSinistros>> BuscaHistoricoSinistroAsync(int sinistroId);
    }
}
