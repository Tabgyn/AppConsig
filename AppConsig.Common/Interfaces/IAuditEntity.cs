using System;

namespace AppConsig.Common.Interfaces
{
    public interface IAuditEntity
    {
        string CriadoPor { get; set; }
        DateTime CriadoEm { get; set; }
        string AtualizadoPor { get; set; }
        DateTime AtualizadoEm { get; set; }
        bool Excluido { get; set; }
    }
}