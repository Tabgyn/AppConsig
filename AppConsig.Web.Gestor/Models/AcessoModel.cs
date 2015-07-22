using System.ComponentModel.DataAnnotations;

namespace AppConsig.Web.Gestor.Models
{
    public class AcessoModel
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Senha { get; set; }
    }

    public class NovaSenhaModel
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}