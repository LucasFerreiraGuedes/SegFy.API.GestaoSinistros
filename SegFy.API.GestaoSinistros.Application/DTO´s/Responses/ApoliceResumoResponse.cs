using SegFy.API.GestaoSinistros.Domain.Apolices.Enums;

namespace SegFy.API.GestaoSinistros.Application.DTO_s.Responses
{
    public class ApoliceResumoResponse
    {
        public ApoliceResumoResponse(int id, string numero, StatusApolice status)
        {
            Id = id;
            Numero = numero;
            Status = status;
        }

        public int Id { get; set; }
        public string Numero { get; set; }
        public StatusApolice Status { get; set; }


    }
}
