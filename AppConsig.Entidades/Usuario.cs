using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Usuario")]
    public class Usuario : EntidadeAuditavel<long>
    {
        [Required]
        [MaxLength(256)]
        public string Login { get; set; }
        [Required]
        [MaxLength(256)]
        public string Senha { get; set; }
        [Required]
        [MaxLength(256)]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public long PerfilId { get; set; }
        public Perfil Perfil { get; set; }
    }
}