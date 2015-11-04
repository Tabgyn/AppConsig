using System.ComponentModel.DataAnnotations;
using AppConsig.Web.Gestor.Resources;

namespace AppConsig.Web.Gestor.Models
{
    public class AvisoListModel
    {
        public long Id { get; set; }
        public string Texto { get; set; }
        [Display(Name = @"Criado por")]
        public string CriadoPor { get; set; }
        [Display(Name = @"Criado em")]
        public string CriadoEm { get; set; }
    }

    public class AvisoEditModel
    {
        public long Id { get; set; }
        [Required(ErrorMessageResourceType = typeof (Annotations), ErrorMessageResourceName = "IsRequired")]
        [MaxLength(256, ErrorMessageResourceType = typeof (Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Texto { get; set; }
    }
}