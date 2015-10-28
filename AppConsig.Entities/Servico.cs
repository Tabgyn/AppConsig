using AppConsig.Common;

namespace AppConsig.Entities
{
    public class Servico : AuditEntity<long>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoServicoRelacao TipoServicoRelacao { get; set; }
        public TipoServicoInerente TipoServicoInerente { get; set; }
        public int Ordem { get; set; }
    }

    public enum TipoServicoInerente
    {
        Nenhum = 0,
        Cartao = 1,
        Seguro = 2,
        Emprestimo = 3
    }

    public enum TipoServicoRelacao
    {
        Nenhum = 0,
        Temporario = 1,
        Comissionado = 2,
        Efetivo = 4
    }
}