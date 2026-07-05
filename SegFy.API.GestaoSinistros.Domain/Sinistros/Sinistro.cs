using SegFy.API.GestaoSinistros.Domain.Apolices;
using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Domain.Sinistros
{
    public class Sinistro
    {
        public Sinistro(int apoliceId, string descricao, decimal valorEstimado, StatusSinistro status, DateTime dataAbertura)
        {
            ApoliceId = apoliceId;
            Descricao = descricao;
            ValorEstimado = valorEstimado;
            Status = status;
            DataAbertura = dataAbertura;

            HistoricoSinistros.Add(new HistoricoSinistros
            {
                StatusAnterior = null,
                StatusAtual = status,
                DataAtualizacao = DateTime.UtcNow
            });
        }
        public int Id { get; set; }
        public int ApoliceId { get; set; }
        public Apolice Apolice { get; set; }
        public string Descricao { get; set; }
        public decimal ValorEstimado { get; set; }
        public decimal? ValorAprovado { get; set; }
        public string? Motivo { get; set; }
        public StatusSinistro Status { get; set; }
        public DateTime DataAbertura { get; set; }

        public DateTime? DataEncerramento { get; set; }
        public ICollection<HistoricoSinistros> HistoricoSinistros { get; private set; } = [];

        public bool AtualizaStatus(StatusSinistro novoStatus)
        {
            if (Status == StatusSinistro.Aberto && novoStatus != StatusSinistro.EmAnalise)
                return false;

            if (Status == StatusSinistro.EmAnalise && novoStatus != StatusSinistro.Aprovado && novoStatus != StatusSinistro.Negado)
                return false;

            if (Status == StatusSinistro.Aprovado && novoStatus != StatusSinistro.Encerrado)
                return false;

            if (Status == StatusSinistro.Negado || Status == StatusSinistro.Encerrado)
                return false;

            HistoricoSinistros.Add(new HistoricoSinistros
            {
                StatusAnterior = Status,
                StatusAtual = novoStatus,
                DataAtualizacao = DateTime.UtcNow
            });

            Status = novoStatus;

            return true;
        }


    }
}
