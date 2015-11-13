using System.ComponentModel;

namespace AppConsig.Common.Enums
{
    public enum TipoRepresentante
    {
        Matriz = 1,
        [Description("Escritório")]
        Escritorio = 2,
        [Description("Representante legal")]
        RepresentanteLegal = 3,
        [Description("Agência")]
        Agencia = 4,
        Filial = 5,
        Sucursal = 6
    }
}