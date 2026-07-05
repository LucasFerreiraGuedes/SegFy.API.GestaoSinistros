using SegFy.API.GestaoSinistros.Domain.Sinistros;

namespace SegFy.API.GestaoSinistros.Application.Repository
{
    public interface ISinistroRepository
    {
         Task CriarSinistroAsync(Sinistro sinistro);
         Task<Sinistro?> BuscaSinistroPorIdAsync(int id);
         Task AtualizaSinistroAsync(Sinistro sinistro);
         IQueryable<Sinistro> Query();
    }
}
