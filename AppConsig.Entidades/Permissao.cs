using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Permissao")]
    public class Permissao : EntidadeAuditavel<long>
    {
        [Required]
        [MaxLength(256)]
        public string Nome { get; set; }
        [MaxLength(256)]
        public string Descricao { get; set; }
        [MaxLength(256)]
        public string Action { get; set; }
        [MaxLength(256)]
        public string Controller { get; set; }
        public ICollection<Perfil> Perfis { get; set; }
    }
}