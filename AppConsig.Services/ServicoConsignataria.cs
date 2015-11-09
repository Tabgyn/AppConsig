using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoConsignataria : ServicoBasico<Consignataria>, IServicoConsignataria
    {
        public ServicoConsignataria(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Consignataria>();
        }
    }
}