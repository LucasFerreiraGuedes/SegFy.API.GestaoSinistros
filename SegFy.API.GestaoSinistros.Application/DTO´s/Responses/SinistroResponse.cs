using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Application.DTO_s.Responses
{
    public class SinistroResponse
    {
        public SinistroResponse()
        {

        }

        public SinistroResponse(int id, ApoliceResumoResponse apolice, string descricao, decimal valorEstimado, decimal? valorAprovado, string? motivoNegacao, StatusSinistro status,
        DateTime dataAbertura, DateTime? dataEncerramento)
        {
            Id = id;
            Apolice = apolice;
            Descricao = descricao;
            ValorEstimado = valorEstimado;
            ValorAprovado = valorAprovado;
            Motivo = motivoNegacao;
            Status = status;
            DataAbertura = dataAbertura;
            DataEncerramento = dataEncerramento;
        }
        public ApoliceResumoResponse Apolice { get; set; }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal ValorEstimado { get; set; }
        public decimal? ValorAprovado { get; set; }
        public string? Motivo { get; set; }
        public StatusSinistro Status { get; set; }
        public DateTime DataAbertura { get; set; }

        public DateTime? DataEncerramento { get; set; }
    }
}
