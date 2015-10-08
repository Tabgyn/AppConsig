using System;

namespace AppConsig.Common.Interfaces
{
    public interface IAuditEntity
    {
        string CreateBy { get; set; }
        DateTime CreateDate { get; set; }
        string UpdateBy { get; set; }
        DateTime UpdateDate { get; set; }
        bool Deleted { get; set; }
    }
}