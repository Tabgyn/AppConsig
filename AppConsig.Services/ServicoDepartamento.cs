using System.Collections.Generic;
using System.Linq;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoDepartamento : ServicoBasico<Departamento>, IServicoDepartamento
    {
        public ServicoDepartamento(IContext context) : base(context)
        {
            Context = context;
            Dbset = Context.Set<Departamento>();
        }

        public List<SistemaFolha> ObterSistemasFolha()
        {
            return Context.SistemasFolha.ToList();
        }
    }
}