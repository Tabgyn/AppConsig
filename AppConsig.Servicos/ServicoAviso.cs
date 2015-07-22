using System.Linq;
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
            _Contexto = contexto; 
            _Dbset = _Contexto.Set<Aviso>(); 
        }
        
        public Aviso ObterPeloId(long id) {
            return _Dbset.FirstOrDefault(x => x.Id == id); 
        } 
    }
}