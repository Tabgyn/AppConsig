using System.ComponentModel.DataAnnotations;

namespace AppConsig.Web.Gestor.Models
{
    public class ServicoModel
    {
        public string Nome { get; set; }

        [Display(Name = @"Descrição")]
        public string Descricao { get; set; }

        public int Ordem { get; set; }

        [Display(Name = @"Tipo relação")]
        public int TipoServicoRelacao { get; set; }

        [Display(Name = @"Tipo inerente")]
        public int TipoServicoInerente { get; set; }
    }
}