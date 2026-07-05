using SegFy.API.GestaoSinistros.Application.DTO_s.Responses;
using SegFy.API.GestaoSinistros.Application.Exceptions;
using SegFy.API.GestaoSinistros.Application.Interfaces;
using SegFy.API.GestaoSinistros.Application.Repository;

namespace SegFy.API.GestaoSinistros.Application.Services
{
    public class HistoricoService : IHistoricoService
    {
        private readonly IHistoricoSinistroRepository _historicoSinistrorepository;

        public HistoricoService(IHistoricoSinistroRepository historicoSinistroRepository)
        {
            _historicoSinistrorepository = historicoSinistroRepository;
        }
        public async Task<ICollection<HistoricoSinistroResponse>?> ConsultarHistoricoAsync(int sinistroId)
        {
            var historico = await _historicoSinistrorepository.BuscaHistoricoSinistroAsync(sinistroId);

            if (historico == null)
                return null;

            List<HistoricoSinistroResponse> response = historico
                    .Select(x => new HistoricoSinistroResponse
                    {
                        Id = x.Id,
                        StatusAnterior = x.StatusAnterior,
                        StatusAtual = x.StatusAtual,
                        DataAtualizacao = x.DataAtualizacao
                    })
                    .ToList();

            return response;
        }

    }         
}
