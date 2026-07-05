using SegFy.API.GestaoSinistros.Application.DTO_s.Requests;
using SegFy.API.GestaoSinistros.Application.DTO_s.Responses;

namespace SegFy.API.GestaoSinistros.Application.Interfaces
{
    public interface ISinistroService
    {
        Task AbrirSinistroAsync(AbrirSinistroRequest request);
        Task<SinistroFiltroResponse<SinistroResponse>> BuscaSinistrosPorfiltroAsync(SinistroFiltroRequest filtro);
        Task<SinistroResponse?> BuscarSinistroPorIdAsync(int id);
        Task AtualizarStatusSinistroAsync(int id,AtualizarStatusSinistroRequest request);
    }
}
