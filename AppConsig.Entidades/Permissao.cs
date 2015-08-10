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
        public string Icone { get; set; }

        public long ParenteId { get; set; }
        
        public int Ordem { get; set; }

        public bool Visivel { get; set; }

        public bool IsCrud { get; set; }

        public string Atributos { get; set; }
    }
}