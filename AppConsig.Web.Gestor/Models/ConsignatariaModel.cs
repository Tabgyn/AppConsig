using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common.Enums;
using AppConsig.Web.Gestor.Resources;

namespace AppConsig.Web.Gestor.Models
{
    public class ConsignatariaListModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }

        [DisplayFormat(DataFormatString = @"{0:00\.000\.000\/0000\-00}", ApplyFormatInEditMode = true)]
        public string CNPJ { get; set; }

        [Display(Name = @"Código")]
        public string Codigo { get; set; }

        [Display(Name = @"Criado por")]
        public string CriadoPor { get; set; }

        [Display(Name = @"Criado em")]
        public DateTime CriadoEm { get; set; }
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
        [DisplayFormat(DataFormatString = @"{0:00\.000\.000\/0000\-00}", ApplyFormatInEditMode = true)]
        public string CNPJ { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [Display(Name = @"Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [Display(Name = @"E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [Display(Name = @"Tipo representante")]
        public TipoRepresentante TipoRepresentante { get; set; }
    }
}