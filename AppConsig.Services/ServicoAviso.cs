using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoAviso : ServicoBasico<Aviso>, IServicoAviso
    {
        public ServicoAviso(IContext context) 
            : base(context) 
        { 
            Context = context; 
            Dbset = Context.Set<Aviso>(); 
        }
    }
}