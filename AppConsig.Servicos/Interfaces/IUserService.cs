using System.Collections.Generic;
using AppConsig.Entities;

namespace AppConsig.Services.Interfaces
{
    public interface IUserService : IEntityService<User>
    {
        bool ValidateUser(string login, string password);
        void ResetPassword(User user);
        List<Permission> GetPermissionsFromUser(long userId);
    }
}