using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class User : AuditEntity<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Surname { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} é inválido")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Password { get; set; }

        public string Picture { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Facebook { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Twitter { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string PhoneNumber { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string MobileNumber { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Complemento")]
        public string ComplementAddress { get; set; }

        public bool IsAdmin { get; set; }

        public long ProfileId { get; set; }

        public Profile Profile { get; set; }
    }
}