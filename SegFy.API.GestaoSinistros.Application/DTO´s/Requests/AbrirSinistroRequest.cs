namespace SegFy.API.GestaoSinistros.Application.DTO_s.Requests
{
    public class AbrirSinistroRequest
    {
        public string NumeroApolice { get; set; }
        public string Descricao { get; set; }
        public decimal ValorEstimado { get; set; }
        public DateTime DataAbertura { get; set; }    }
}
