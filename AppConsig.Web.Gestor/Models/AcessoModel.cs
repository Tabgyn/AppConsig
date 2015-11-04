using System.ComponentModel.DataAnnotations;
using AppConsig.Web.Gestor.Resources;

namespace AppConsig.Web.Gestor.Models
{
    public class AcessoModel
    {
        [Display(Name = @"E-mail")]
        [Required(ErrorMessageResourceType = typeof (Annotations), ErrorMessageResourceName = "IsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof (Annotations), ErrorMessageResourceName = "InvalidEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof (Annotations), ErrorMessageResourceName = "IsRequired")]
        public string Senha { get; set; }
    }

    public class CriarNovaSenhaModel
    {
        [Display(Name = @"E-mail")]
        [Required(ErrorMessageResourceType = typeof (Annotations), ErrorMessageResourceName = "IsRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof (Annotations), ErrorMessageResourceName = "InvalidEmail")]
        public string Email { get; set; }
    }
}