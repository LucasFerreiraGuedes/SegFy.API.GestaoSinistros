using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Application.DTO_s.Responses
{
    public class HistoricoSinistroResponse
    {
        public int Id { get; set; }
        public StatusSinistro? StatusAnterior { get; set; }
        public StatusSinistro StatusAtual { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
