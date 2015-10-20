using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Permissao : Entity<long>
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
        public string Acao { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Controle { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string ClasseIcone { get; set; }
        
        public long? ParenteId { get; set; }

        public int Ordem { get; set; }
        
        public bool EhPadrao { get; set; }

        public bool MostrarNoMenu { get; set; }
        
        public bool EhCRUD { get; set; }

        public string Atributos { get; set; }
    }
}