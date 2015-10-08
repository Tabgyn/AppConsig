using System.Collections.Generic;
using AppConsig.Entities;

namespace AppConsig.Services.Interfaces
{
    public interface IPermissionService : IEntityService<Permission>
    {
        IEnumerable<Permission> GetProfilePermissions(long profileId);
    }
}