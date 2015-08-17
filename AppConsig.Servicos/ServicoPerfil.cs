using System.Data.Entity;
using System.Linq;
using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoPerfil : ServicoEntidade<Perfil>, IServicoPerfil
    {
        public ServicoPerfil(IContexto contexto) 
            : base(contexto) 
        {
            Contexto = contexto;
            Dbset = Contexto.Set<Perfil>();
        }

        public Perfil ObterPerfilComPermissoes(long id)
        {
            return Contexto.Perfis.Where(p => p.Id == id)
                .Include(p => p.Permissoes)
                .FirstOrDefault();
        }
    }
}