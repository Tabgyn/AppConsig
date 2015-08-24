using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Perfil")]
    public class Perfil : EntidadeAuditavel<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(256)]
        public string Nome { get; set; }

        [MaxLength(256)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [ScaffoldColumn(false)]
        public bool Editavel { get; set; }

        [Display(Name = "Permissões")]
        public ICollection<Permissao> Permissoes { get; set; }
    }
}