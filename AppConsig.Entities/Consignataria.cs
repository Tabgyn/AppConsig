using AppConsig.Common;

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

    public enum TipoRepresentante
    {
        Matriz = 1,
        Escritorio = 2,
        RepresentanteLegal = 3,
        Agencia = 4,
        Filial = 5,
        Sucursal = 6
    }
}