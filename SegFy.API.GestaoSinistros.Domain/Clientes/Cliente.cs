using SegFy.API.GestaoSinistros.Domain.Apolices;

namespace SegFy.API.GestaoSinistros.Domain.Clientes
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Rg { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<Apolice> Apolices { get; set; }
    }
}
