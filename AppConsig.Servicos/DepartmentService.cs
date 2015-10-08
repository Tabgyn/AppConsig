using System.Collections.Generic;
using System.Linq;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class DepartmentService : EntityService<Department>, IDepartamentService
    {
        public DepartmentService(IContext context) : base(context)
        {
            Context = context;
            Dbset = Context.Set<Department>();
        }

        public List<HumanResourceSystem> GetHumanResourceSystems()
        {
            var systems = Context.HumanResourceSystems.ToList();

            return systems;
        }
    }
}