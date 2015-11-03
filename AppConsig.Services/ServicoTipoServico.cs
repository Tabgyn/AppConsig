using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoTipoServico : ServicoBasico<Servico>, IServicoTipoServico
    {
        public ServicoTipoServico(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Servico>();
        }
    }
}