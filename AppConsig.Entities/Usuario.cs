using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Usuario : AuditEntity<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Sobrenome { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} é inválido")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Usuário")]
        public string NomeDeUsuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Senha { get; set; }

        public string Foto { get; set; }

        public DateTime? UltimoAcesso { get; set; }

        public bool Bloqueado { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Facebook { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Twitter { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Telefone { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Celular { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Complemento")]
        public string EnderecoComplemento { get; set; }

        public bool EhAdministrador { get; set; }

        public long PerfilId { get; set; }

        public Perfil Perfil { get; set; }
    }
}