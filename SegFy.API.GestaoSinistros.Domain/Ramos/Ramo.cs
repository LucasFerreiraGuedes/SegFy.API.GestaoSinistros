using SegFy.API.GestaoSinistros.Domain.Apolices;

namespace SegFy.API.GestaoSinistros.Domain.Ramos
{
    public class Ramo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Apolice> Apolices { get; set; }
    }
}
