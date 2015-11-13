using System.ComponentModel.DataAnnotations;
using AppConsig.Common.Enums;

namespace AppConsig.Web.Gestor.Models
{
    public class ServicoListModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        [Display(Name = @"Criado por")]
        public string CriadoPor { get; set; }
        [Display(Name = @"Criado em")]
        public string CriadoEm { get; set; }
    }

    public class ServicoEditModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        [Display(Name = @"Descrição")]
        public string Descricao { get; set; }

        public int Ordem { get; set; }

        [Display(Name = @"Tipo relação")]
        public TipoServicoRelacao TipoServicoRelacao { get; set; }

        [Display(Name = @"Tipo inerente")]
        public TipoServicoInerente TipoServicoInerente { get; set; }
    }
}