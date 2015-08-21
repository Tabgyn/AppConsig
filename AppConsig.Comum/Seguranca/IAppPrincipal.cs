using System.Security.Principal;

namespace AppConsig.Comum.Seguranca
{
    public interface IAppPrincipal : IPrincipal
    {
        long Id { get; set; }
        string Nome { get; set; }
        string Sobrenome { get; set; }
        string Email { get; set; }
    }
}