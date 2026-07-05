using SegFy.API.GestaoSinistros.Domain.Apolices;

namespace SegFy.API.GestaoSinistros.Application.Repository
{
    public interface IApoliceRepository
    {
        public Task<Apolice?> BuscarApoliceNumero(string numero);
    }
}
