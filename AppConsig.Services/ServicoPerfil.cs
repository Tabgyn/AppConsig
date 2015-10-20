using System.Data.Entity;
using System.Linq;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoPerfil : ServicoBasico<Perfil>, IServicoPerfil
    {
        public ServicoPerfil(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Perfil>();
        }

        public Perfil ObterPerfilComPermissoes(long perfilId)
        {
            return Context.Perfis.Where(p => p.Id == perfilId)
                .Include(p => p.Permissoes)
                .FirstOrDefault();
        }
    }
}