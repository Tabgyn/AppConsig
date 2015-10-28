using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common.Interfaces;

namespace AppConsig.Common
{
    public abstract class AuditEntity<T> : Entity<T>, IAuditEntity
    {
        [MaxLength(256)]
        public string CriadoPor { get; set; }

        public DateTime CriadoEm { get; set; }

        [MaxLength(256)]
        public string AtualizadoPor { get; set; }

        public DateTime AtualizadoEm { get; set; }

        public bool Excluido { get; set; }
    }
}