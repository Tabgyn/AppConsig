using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Audit : Entity<long>
    {
        [Required]
        public string SessaoId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string Controller { get; set; }

        [Required]
        public DateTime AuditDate { get; set; }

        public string Data { get; set; }
    }
}