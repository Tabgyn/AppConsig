using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Department : AuditEntity<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Código departamento")]
        public long DepartmentCode { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Sistema de folha")]
        public long HumanResourceSystemId { get; set; }

        public HumanResourceSystem HumanResourceSystem { get; set; }
    }
}