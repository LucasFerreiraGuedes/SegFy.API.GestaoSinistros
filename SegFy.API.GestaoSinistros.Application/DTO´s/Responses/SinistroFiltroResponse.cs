namespace SegFy.API.GestaoSinistros.Application.DTO_s.Responses
{
    public class SinistroFiltroResponse<T>
    {
        public List<T> Dados { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int NumeroPagina { get; set; }
        public int TamanhoPagina { get; set; }
    }
}
