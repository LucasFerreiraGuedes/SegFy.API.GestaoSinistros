using SegFy.API.GestaoSinistros.Domain.Sinistros.Enums;

namespace SegFy.API.GestaoSinistros.Application.DTO_s.Requests
{
    public class SinistroFiltroRequest
    {
        public StatusSinistro? Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

         public int NumeroPagina = 1;
         public int TamanhoPagina = 10;
     }
}
