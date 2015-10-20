using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Auditoria : Entity<long>
    {
        [Required]
        public string SessaoId { get; set; }

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string EnderecoIP { get; set; }

        [Required]
        public string Acao { get; set; }

        [Required]
        public string Controle { get; set; }

        [Required]
        public DateTime DataEvento { get; set; }

        public string Dados { get; set; }
    }
}