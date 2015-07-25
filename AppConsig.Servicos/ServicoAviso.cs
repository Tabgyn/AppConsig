using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoAviso : ServicoEntidade<Aviso>, IServicoAviso
    {
        public ServicoAviso(IContexto contexto) 
            : base(contexto) 
        { 
            Contexto = contexto; 
            Dbset = Contexto.Set<Aviso>(); 
        }
    }
}