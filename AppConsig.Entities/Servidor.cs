using System;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Servidor : AuditEntity<long>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Matricula { get; set; }
        public DateTime NascidoEm { get; set; }
        public string Foto { get; set; }
        public DateTime AdmitidoEm { get; set; }
        public DateTime? AfastadoEm { get; set; }

        public long DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
    }
}