using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Aviso : AuditEntity<long>
    {
        public string Texto { get; set; }
    }
}