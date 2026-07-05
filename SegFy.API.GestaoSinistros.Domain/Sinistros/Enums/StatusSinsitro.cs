using System.ComponentModel;

namespace SegFy.API.GestaoSinistros.Domain.Sinistros.Enums
{
    public enum StatusSinistro
    {
        [Description("Aberto")]
        Aberto = 1,

        [Description("Em análise")]
        EmAnalise = 2,

        [Description("Aprovado")]
        Aprovado = 3,

        [Description("Negado")]
        Negado = 4,

        [Description("Encerrado")]
        Encerrado = 5
    }
}
