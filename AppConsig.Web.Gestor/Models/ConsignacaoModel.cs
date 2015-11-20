using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Web.Gestor.Resources;

namespace AppConsig.Web.Gestor.Models
{
    public class ConsignacaoListModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        [Display(Name = @"Descrição")]
        public string Descricao { get; set; }
        
        [Display(Name = @"Código")]
        public string Codigo { get; set; }

        [Display(Name = @"Criado por")]
        public string CriadoPor { get; set; }

        [Display(Name = @"Criado em")]
        public DateTime CriadoEm { get; set; }

        public string Consignataria { get; set; }
        public string Servico { get; set; }
    }

    public class ConsignacaoEditModel
    {
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Nome { get; set; }

        [Display(Name = @"Descrição")]
        [MaxLength(256, ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "MaxLength")]
        public string Descricao { get; set; }

        [Display(Name = @"Código")]
        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public string Codigo { get; set; }

        [Display(Name = @"Máx. parcelas")]
        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public int MaximoParcela { get; set; }

        [Display(Name = @"Valor mínimo")]
        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public decimal ValorMinimo { get; set; }

        [Display(Name = @"Inicio da vigência")]
        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public DateTime InicioDaVigenciaEm { get; set; }

        [Display(Name = @"Fim da vigência")]
        public DateTime FimDaVigenciaEm { get; set; }

        [Display(Name = @"Permite desconto parcial")]
        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public bool PermiteDescontoParcial { get; set; }

        [Display(Name = @"Permite lançamento manual")]
        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public bool PermiteLancamentoManual { get; set; }

        [Display(Name = @"Permite outras ocorrências")]
        [Required(ErrorMessageResourceType = typeof(Annotations), ErrorMessageResourceName = "IsRequired")]
        public bool PermiteOutrasOcorrencias { get; set; }

        [Display(Name = @"Consignatária")]
        public long ConsignatariaId { get; set; }
        
        [Display(Name = @"Tipo de serviço")]
        public long ServicoId { get; set; }
    }
}