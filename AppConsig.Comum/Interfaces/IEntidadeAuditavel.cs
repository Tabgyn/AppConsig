using System;

namespace AppConsig.Comum.Interfaces
{
    public interface IEntidadeAuditavel
    {
        string CriadoPor { get; set; }
        DateTime DataCriacao { get; set; }
        string AtualizadoPor { get; set; }
        DateTime DataAtualizacao { get; set; }
        bool Excluido { get; set; }
    }
}