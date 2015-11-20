using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoConsignacao : ServicoBasico<Consignacao>, IServicoConsignacao
    {
        public ServicoConsignacao(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Consignacao>();
        }
    }
}