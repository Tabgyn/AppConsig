using System.Collections.Generic;
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
        public string UrlImagem { get; set; }

        public long ParenteId { get; set; }
        
        public int Ordem { get; set; }
    }
}