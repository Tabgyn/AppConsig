using System;
using System.Security.Principal;

namespace AppConsig.Comum.Seguranca
{
    public interface IAppPrincipal : IPrincipal
    {
        Guid Id { get; set; }
        string Nome { get; set; }
        string Sobrenome { get; set; }
        string Email { get; set; }
        bool Admin { get; set; }
    }
}