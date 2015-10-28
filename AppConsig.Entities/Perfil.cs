using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Perfil : AuditEntity<long>
    {
        public Perfil()
        {
            Permissoes = new List<Permissao>();
        }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Editável")]
        public bool EhEditavel { get; set; }

        [Display(Name = "Permissões")]
        public ICollection<Permissao> Permissoes { get; set; }
    }
}