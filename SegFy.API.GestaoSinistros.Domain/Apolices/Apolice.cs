using SegFy.API.GestaoSinistros.Domain.Apolices.Enums;
using SegFy.API.GestaoSinistros.Domain.Clientes;
using SegFy.API.GestaoSinistros.Domain.Ramos;
using SegFy.API.GestaoSinistros.Domain.Sinistros;
using System.Net.Http.Headers;

namespace SegFy.API.GestaoSinistros.Domain.Apolices
{
    public class Apolice
    {
        public int Id { get; set; }
        public int RamoId { get; set; }
        public Ramo Ramo { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public string Numero { get; set; }
        public decimal ValorSegurado { get; set; }
        public StatusApolice Status { get; set; }
        public DateTime VigenciaInicio { get; set; }
        public DateTime VigenciaFim { get; set; }
        public ICollection<Sinistro> Sinistros { get; set; }

        public bool ValidaAberturaSinistro()
        {
            if (Status != StatusApolice.Ativa)
                return false;

            return true;        
        }
    }
}
