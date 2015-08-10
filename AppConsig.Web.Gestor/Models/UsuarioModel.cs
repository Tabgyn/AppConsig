using System.ComponentModel.DataAnnotations;

namespace AppConsig.Web.Gestor.Models
{
    public class UsuarioModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        public string Nome { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máx. 256 caracteres")]
        public string Sobrenome { get; set; }

        [Display(Name = "Nome completo")]
        public string NomeCompleto => $"{Nome} {Sobrenome}";

        public byte[] Foto { get; set; }

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
    }
}