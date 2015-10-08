using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class HumanResourceSystem : Entity<long>
    {
        [MaxLength(100, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}