using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common;
using AppConsig.Common.Enums;

namespace AppConsig.Entities
{
    public class Servico : AuditEntity<long>
    {
        [Required]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Relação de trabalho")]
        [Required]
        public TipoServicoRelacao TipoServicoRelacao { get; set; }
        [Display(Name = "Inerente a")]
        [Required]
        public TipoServicoInerente TipoServicoInerente { get; set; }
        [Required]
        [Display(Name = "Ordem de desconto")]
        public int Ordem { get; set; }
    }
}