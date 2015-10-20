using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Departamento : AuditEntity<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Código do departamento")]
        public long CodigoDepartamento { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Sistema de folha")]
        public long SistemaFolhaId { get; set; }

        public SistemaFolha SistemaFolha { get; set; }
    }
}