using System.ComponentModel;

namespace AppConsig.Common.Enums
{
    public enum TipoServicoInerente
    {
        [Description("Cartão")]
        Cartao = 1,
        [Description("Empréstimo")]
        Emprestimo = 2,
        Seguro = 3
    }
}