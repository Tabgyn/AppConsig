using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Aviso")]
    public class Aviso : EntidadeAuditavel<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Texto { get; set; }
    }
}