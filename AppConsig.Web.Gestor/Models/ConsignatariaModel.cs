using System.ComponentModel.DataAnnotations;
using AppConsig.Web.Gestor.Resources;

namespace AppConsig.Web.Gestor.Models
{
    public class ConsignatariaListModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string CNPJ { get; set; }

        [Display(Name = @"Código")]
        public string Codigo { get; set; }

        [Display(Name = @"Criado por")]
        public string CriadoPor { get; set; }

        [Display(Name = @"Criado em")]
        public string CriadoEm { get; set; }
    }

    public class ConsignatariaEditModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public string Sigla { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public string CNPJ { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [Display(Name = @"Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [Display(Name = @"E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [Display(Name = @"Tipo representante")]
        public int TipoRepresentante { get; set; }
    }
}