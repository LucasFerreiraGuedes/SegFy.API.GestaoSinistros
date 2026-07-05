using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Application.DTO_s.Requests
{
    public class AtualizarStatusSinistroRequest
    {
        public StatusSinistro StatusSinsitro { get; set; }
        public string? Motivo { get; set; }
    }
}
