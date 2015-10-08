using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class PermissionService : EntityService<Permission>, IPermissionService
    {
        public PermissionService(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Permission>();
        }

        public IEnumerable<Permission> GetProfilePermissions(long profileId)
        {
            var permissions =
                Context.Profiles.Where(p => p.Id == profileId)
                    .Include(p => p.Permissions)
                    .First()
                    .Permissions.ToList();

            return permissions;
        }
    }
}