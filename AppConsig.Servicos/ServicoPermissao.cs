using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoPermissao : ServicoEntidade<Permissao>, IServicoPermissao
    {
        public ServicoPermissao(IContexto contexto) 
            : base(contexto) 
        {
            Contexto = contexto;
            Dbset = Contexto.Set<Permissao>();
        }
    }
}