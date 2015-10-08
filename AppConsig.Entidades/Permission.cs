using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Permission : Entity<long>
    {
        [Required]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Description { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Url { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Action { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Controller { get; set; }

        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string IconClass { get; set; }
        
        public long? ParentId { get; set; }

        public int Order { get; set; }
        
        public bool IsStandard { get; set; }

        public bool ShowInMenu { get; set; }
        
        public bool IsCrud { get; set; }

        public string Attributes { get; set; }
    }
}