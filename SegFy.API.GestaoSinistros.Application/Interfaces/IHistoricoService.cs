using SegFy.API.GestaoSinistros.Application.DTO_s.Responses;

namespace SegFy.API.GestaoSinistros.Application.Interfaces
{
    public interface IHistoricoService
    {
        public Task<ICollection<HistoricoSinistroResponse>?> ConsultarHistoricoAsync(int sinistroId);
    }
}
