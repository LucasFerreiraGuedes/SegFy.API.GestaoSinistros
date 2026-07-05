using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Domain.Sinistros
{
    public class HistoricoSinistros
    {
        public int Id { get; set; }
        public int SinistroId { get; set; }
        public Sinistro Sinistro { get; set; }
        public StatusSinistro? StatusAnterior { get; set; }
        public StatusSinistro StatusAtual { get; set; }
        public DateTime DataAtualizacao { get; set; }

    }
}
