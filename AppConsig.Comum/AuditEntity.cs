using System;
using System.ComponentModel.DataAnnotations;
using AppConsig.Common.Interfaces;

namespace AppConsig.Common
{
    public abstract class AuditEntity<T> : Entity<T>, IAuditEntity
    {
        [MaxLength(256)]
        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        [MaxLength(256)]
        public string UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool Deleted { get; set; }
    }
}