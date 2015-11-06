using System.Data.Entity;
using System.Linq;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoServidor : ServicoBasico<Servidor>, IServicoServidor
    {
        public ServicoServidor(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Servidor>();
        }

        public Servidor ObterServidorComDepartamento(long id)
        {
            var servidor = Dbset.Find(id);

            return Context.Servidores.Where(p => p.Id == servidor.Id)
                    .Include(p => p.Departamento)
                    .First();
        }
    }
}