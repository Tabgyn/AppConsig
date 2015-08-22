using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Usuario")]
    public class Usuario : EntidadeAuditavel<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        public string Nome { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        public string Sobrenome { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} é inválido")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Senha { get; set; }

        public string Foto { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        public string Facebook { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        public string Twitter { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        public string Telefone { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        public string Celular { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        [Display(Name = "Complemento")]
        public string EnderecoComplemento { get; set; }

        public long PerfilId { get; set; }

        public Perfil Perfil { get; set; }
    }
}