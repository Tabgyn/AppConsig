using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Comum.Interfaces;

namespace AppConsig.Comum
{
    public abstract class EntidadeAuditavel<T> : Entidade<T>, IEntidadeAuditavel
    {
        [ScaffoldColumn(false)]
        public string CriadoPor { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DataCriacao { get; set; }
        [ScaffoldColumn(false)]
        public string AtualizadoPor { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DataAtualizacao { get; set; }
        [ScaffoldColumn(false)]
        public bool Excluido { get; set; }
    }
}