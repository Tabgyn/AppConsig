using AppConsig.Entities;

namespace AppConsig.Services.Interfaces
{
    public interface IProfileService : IEntityService<Profile>
    {
        Profile GetProfileWithPermissions(long id);
    }
}