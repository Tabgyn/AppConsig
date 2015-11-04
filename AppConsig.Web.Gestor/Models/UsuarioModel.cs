using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Web.Gestor.Resources;

namespace AppConsig.Web.Gestor.Models
{
    public class UsuarioContaModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Nome { get; set; }

        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Sobrenome { get; set; }

        [Display(Name = @"Nome completo")]
        public string NomeCompleto => $"{Nome} {Sobrenome}";

        public string Foto { get; set; }

        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Facebook { get; set; }

        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Twitter { get; set; }

        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Telefone { get; set; }

        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Celular { get; set; }

        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = @"Endereço")]
        public string Endereco { get; set; }

        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        [Display(Name = @"Complemento")]
        public string EnderecoComplemento { get; set; }
    }

    public class UsuarioEditaModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Nome { get; set; }

        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Sobrenome { get; set; }

        [Display(Name = @"Nome completo")]
        public string NomeCompleto => $"{Nome} {Sobrenome}";

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        public string CriadoPor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCriacao { get; set; }
    }
}