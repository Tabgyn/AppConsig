using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("DetalheAuditoria")]
    public class DetalheAuditoria : Entidade<long>
    {
        [Required]
        public string Propriedade { get; set; }

        public string ValorOriginal { get; set; }

        public string ValorNovo { get; set; }
    }
}