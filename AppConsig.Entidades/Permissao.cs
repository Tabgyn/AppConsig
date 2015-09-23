using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    [Table("Permissao")]
    public class Permissao : Entidade<long>
    {
        [Required]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }
        
        [Required]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Descricao { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Url { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Action { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Controller { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Icone { get; set; }
        
        public long ParenteId { get; set; }

        public int Ordem { get; set; }

        
        public bool Padrao { get; set; }

        public bool MostrarNoMenu { get; set; }
        
        public bool Crud { get; set; }

        public string Atributos { get; set; }
    }
}