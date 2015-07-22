using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Aviso")]
    public class Aviso : EntidadeAuditavel<long>
    {
        [Required]
        [MaxLength(256)]
        public string Texto { get; set; }
    }
}