using System.Collections.Generic;
using AppConsig.Entities;

namespace AppConsig.Services.Interfaces
{
    public interface IDepartamentService : IEntityService<Department>
    {
        List<HumanResourceSystem> GetHumanResourceSystems();
    }
}