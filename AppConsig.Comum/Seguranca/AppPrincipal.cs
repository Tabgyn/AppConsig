using System;
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
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
    }
}