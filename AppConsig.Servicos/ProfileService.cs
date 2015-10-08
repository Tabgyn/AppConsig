using System.Data.Entity;
using System.Linq;
using AppConsig.Data;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ProfileService : EntityService<Profile>, IProfileService
    {
        public ProfileService(IContext context) 
            : base(context) 
        {
            Context = context;
            Dbset = Context.Set<Profile>();
        }

        public Profile GetProfileWithPermissions(long id)
        {
            return Context.Profiles.Where(p => p.Id == id)
                .Include(p => p.Permissions)
                .FirstOrDefault();
        }
    }
}