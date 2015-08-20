using System;
using AppConsig.Comum;

namespace AppConsig.Entidades
{
    public class Auditoria : Entidade<long>
    {
        public long TabelaId { get; set; }
        public string TabelaNome { get; set; }
        public string Usuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Acao { get; set; }
        public string DadoAntigo { get; set; }
        public string DadoNovo { get; set; }
    }
}