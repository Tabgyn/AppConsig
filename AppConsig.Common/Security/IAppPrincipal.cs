using System.Security.Principal;

namespace AppConsig.Common.Security
{
    public interface IAppPrincipal : IPrincipal
    {
        long Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        bool IsAdmin { get; set; }
    }
}