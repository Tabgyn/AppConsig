using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Profile : AuditEntity<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Editável")]
        public bool IsEditable { get; set; }

        [Display(Name = "Permissões")]
        public ICollection<Permission> Permissions { get; set; }
    }
}