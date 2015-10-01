using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Comum.Interfaces;

namespace AppConsig.Comum
{
    public abstract class EntidadeAuditavel : Entidade, IEntidadeAuditavel
    {
        [MaxLength(256)]
        public string CriadoPor { get; set; }

        public DateTime DataCriacao { get; set; }

        [MaxLength(256)]
        public string AtualizadoPor { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public bool Excluido { get; set; }
    }
}