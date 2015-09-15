using System.ComponentModel.DataAnnotations;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    public class Orgao : EntidadeAuditavel<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Código")]
        public long Codigo { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Sistema de folha")]
        public long SistemaFolhaId { get; set; }
        public SistemaFolha SistemaFolha { get; set; }
    }
}