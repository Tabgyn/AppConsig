﻿using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Notice : AuditEntity<long>
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(256, ErrorMessage = "{0} deve ter no máximo {1} caracteres")]
        public string Text { get; set; }
    }
}