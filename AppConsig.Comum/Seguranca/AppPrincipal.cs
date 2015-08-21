using System.Security.Principal;

namespace AppConsig.Comum.Seguranca
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
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
    }
}