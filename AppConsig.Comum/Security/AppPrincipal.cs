using System.Security.Principal;

namespace AppConsig.Common.Security
{
    public class AppPrincipal : IAppPrincipal
    {
        public AppPrincipal(string name)
        {
            Identity = new GenericIdentity(name);
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        public IIdentity Identity { get; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}