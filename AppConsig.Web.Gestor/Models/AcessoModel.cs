using System.ComponentModel.DataAnnotations;

namespace AppConsig.Web.Gestor.Models
{
    public class AcessoModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Endereço de e-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Senha { get; set; }
    }

    public class CriarNovaSenhaModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Endereço de e-mail inválido")]
        public string Email { get; set; }
    }
}