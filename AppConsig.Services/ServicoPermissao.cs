using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoPermissao : ServicoBasico<Permissao>, IServicoPermissao
    {
        public ServicoPermissao(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Permissao>();
        }
    }
}