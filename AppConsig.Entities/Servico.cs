using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

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

    public enum TipoServicoInerente
    {
        [Description("Cartão")]
        Cartao = 1,
        [Description("Empréstimo")]
        Emprestimo = 2,
        [Description("Seguro")]
        Seguro = 3
    }

    public enum TipoServicoRelacao
    {
        [Description("Comissionado")]
        Comissionado = 1,
        [Description("Efetivo")]
        Efetivo = 2,
        [Description("Temporário")]
        Temporario = 3
    }
}