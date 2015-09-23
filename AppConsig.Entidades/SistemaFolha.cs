using System.ComponentModel.DataAnnotations;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    public class SistemaFolha : Entidade<long>
    {
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }
    }
}