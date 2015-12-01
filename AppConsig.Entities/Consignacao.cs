using System;
using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Consignacao : AuditEntity<long>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public int MaximoParcela { get; set; }
        public decimal ValorMinimo { get; set; }
        public DateTime InicioDaVigenciaEm { get; set; }
        public DateTime? FimDaVigenciaEm { get; set; }
        public bool PermiteDescontoParcial { get; set; }
        public bool PermiteLancamentoManual { get; set; }
        public bool PermiteOutrasOcorrencias { get; set; }

        public long ConsignatariaId { get; set; }
        public Consignataria Consignataria { get; set; }
        public long ServicoId { get; set; }
        public Servico Servico { get; set; }
    }
}