using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Perfil")]
    public class Perfil : EntidadeAuditavel<long>
    {
        [Required]
        [MaxLength(256)]
        public string Nome { get; set; }
        [MaxLength(256)]
        public string Descricao { get; set; }

        public ICollection<Permissao> Permissoes { get; set; }
    }
}