using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Perfil")]
    public class Perfil : EntidadeAuditavel
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public bool Editavel { get; set; }

        [Display(Name = "Permissões")]
        public ICollection<Permissao> Permissoes { get; set; }
    }
}