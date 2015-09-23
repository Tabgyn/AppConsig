using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Auditoria")]
    public class Auditoria : Entidade<long>
    {
        public string Usuario { get; set; }

        [Required]
        public DateTime DataEvento { get; set; }

        [Required]
        public TipoEvento TipoEvento { get; set; }

        [Required]
        public string NomeEntidade { get; set; }

        [Required]
        public string RegistroId { get; set; }

        public virtual ICollection<DetalheAuditoria> DetalhesAuditoria { get; set; } = new List<DetalheAuditoria>();
    }
}