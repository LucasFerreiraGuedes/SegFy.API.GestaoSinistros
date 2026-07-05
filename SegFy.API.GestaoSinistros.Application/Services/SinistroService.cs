using Microsoft.EntityFrameworkCore;
using SegFy.API.GestaoSinistros.Application.DTO_s.Requests;
using SegFy.API.GestaoSinistros.Application.DTO_s.Responses;
using SegFy.API.GestaoSinistros.Application.Exceptions;
using SegFy.API.GestaoSinistros.Application.Interfaces;
using SegFy.API.GestaoSinistros.Application.Repository;
using SegFy.API.GestaoSinistros.Domain.Sinistros;
using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Application.Services
{
    public class SinistroService : ISinistroService
    {
        private readonly IApoliceRepository _apoliceRepository;
        private readonly ISinistroRepository _sinistroRepository;

        public SinistroService(IApoliceRepository apoliceRepository, ISinistroRepository sinistroRepository)
        {
            _apoliceRepository = apoliceRepository;
            _sinistroRepository = sinistroRepository;
        }
        public async Task AbrirSinistroAsync(AbrirSinistroRequest request)
        {
            string numeroApolice = request.NumeroApolice.Trim().ToUpperInvariant();
            var apolice = await _apoliceRepository.BuscarApoliceNumero(numeroApolice);

            if(apolice == null)
                throw new NotFoundException("Apólice não encontrada.");

            if (!apolice.ValidaAberturaSinistro())
                throw new BusinessException("Não é possível abrir um sinistro para uma apólice que não está ativa.");

            var sinistro = new Sinistro(
                apolice.Id,
                request.Descricao,
                request.ValorEstimado,
                StatusSinistro.Aberto,
                DateTime.UtcNow);

            await _sinistroRepository.CriarSinistroAsync(sinistro);

        }

        public async Task AtualizarStatusSinistroAsync(int id,AtualizarStatusSinistroRequest request)
        {
            var sinistroDb = await _sinistroRepository.BuscaSinistroPorIdAsync(id);

            if(sinistroDb == null)
                throw new NotFoundException("Sinistro não encontrado.");

            if (!sinistroDb.AtualizaStatus(request.StatusSinsitro))
                throw new BusinessException("Status incorreto para o sinistro");

            if(request.StatusSinsitro == StatusSinistro.Negado && String.IsNullOrEmpty(request.Motivo))
                throw new BusinessException("Para atualizar o status para negado, é obrigatório informar o motivo");

            sinistroDb.Motivo = request.Motivo;

            await _sinistroRepository.AtualizaSinistroAsync(sinistroDb);
        }

        public async Task<SinistroResponse?> BuscarSinistroPorIdAsync(int id)
        {
            var sinistro = await _sinistroRepository.BuscaSinistroPorIdAsync(id);

            if (sinistro == null)
                return null;

            var apoliceResumo = new ApoliceResumoResponse(
                sinistro.ApoliceId,
                sinistro.Apolice.Numero,
                sinistro.Apolice.Status
                );

            var sinistroResponse = new SinistroResponse(
                sinistro.Id,
                apoliceResumo,
                sinistro.Descricao,
                sinistro.ValorEstimado,
                sinistro.ValorAprovado,
                sinistro.Motivo,
                sinistro.Status,
                sinistro.DataAbertura,
                sinistro.DataEncerramento
                );

            return sinistroResponse;
        }

        public async Task<SinistroFiltroResponse<SinistroResponse>> BuscaSinistrosPorfiltroAsync(SinistroFiltroRequest filtro)
        {
            var query = _sinistroRepository.Query();

            if (filtro.Status.HasValue)
                query = query.Where(x => x.Status == filtro.Status.Value);

            if (filtro.DataInicio.HasValue)
                query = query.Where(x => x.DataAbertura >= filtro.DataInicio.Value);

            if (filtro.DataFim.HasValue)
                query = query.Where(x => x.DataAbertura <= filtro.DataFim.Value);

            var total = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.DataAbertura)
                .Skip((filtro.NumeroPagina - 1) * filtro.TamanhoPagina)
                .Take(filtro.TamanhoPagina)
                .Select(x => new SinistroResponse
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                    ValorEstimado = x.ValorEstimado,
                    ValorAprovado = x.ValorAprovado,
                    Motivo = x.Motivo,
                    Status = x.Status,
                    DataAbertura = x.DataAbertura,
                    DataEncerramento = x.DataEncerramento,
                    Apolice = new ApoliceResumoResponse(
                        x.Apolice.Id,
                        x.Apolice.Numero,
                        x.Apolice.Status
                    )
                })
                .ToListAsync();

            return new SinistroFiltroResponse<SinistroResponse>
            {
                Dados = data,
                TotalRegistros = total,
                NumeroPagina = filtro.NumeroPagina,
                TamanhoPagina = filtro.TamanhoPagina
            };
        }
    }
}
