using System.ComponentModel;

namespace AppConsig.Common.Enums
{
    public enum TipoServicoRelacao
    {
        Comissionado = 1,
        Efetivo = 2,
        [Description("Temporário")]
        Temporario = 3
    }
}