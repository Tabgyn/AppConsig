using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Permissao")]
    public class Permissao : Entidade<long>
    {
        [Required]
        [MaxLength(256)]
        public string Nome { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Descricao { get; set; }
        
        [MaxLength(256)]
        public string Url { get; set; }
        
        [MaxLength(256)]
        public string Action { get; set; }
        
        [MaxLength(256)]
        public string Controller { get; set; }
        
        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string Icone { get; set; }

        [ScaffoldColumn(false)]
        public long ParenteId { get; set; }

        [ScaffoldColumn(false)]
        public int Ordem { get; set; }

        [ScaffoldColumn(false)]
        public bool Padrao { get; set; }

        [ScaffoldColumn(false)]
        public bool MostrarNoMenu { get; set; }
        
        [ScaffoldColumn(false)]
        public bool Crud { get; set; }

        [ScaffoldColumn(false)]
        public string Atributos { get; set; }
    }
}