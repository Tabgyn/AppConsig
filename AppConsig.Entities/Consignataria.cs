using AppConsig.Common;
using AppConsig.Common.Enums;

namespace AppConsig.Entities
{
    public class Consignataria : AuditEntity<long>
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string CNPJ { get; set; }
        public string Codigo { get; set; }
        public string Email { get; set; }
        public TipoRepresentante TipoRepresentante { get; set; }
    }
}