using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    public class Orgao : EntidadeAuditavel<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Código")]
        public long Codigo { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Sistema de folha")]
        public long SistemaFolhaId { get; set; }

        [ForeignKey("SistemaFolhaId")]
        public SistemaFolha SistemaFolha { get; set; }
    }
}