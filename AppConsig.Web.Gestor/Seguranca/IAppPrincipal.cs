using System.Collections.Generic;
using System.Security.Principal;
using AppConsig.Entidades;

namespace AppConsig.Web.Gestor.Seguranca
{
    public interface IAppPrincipal : IPrincipal
    {
        long Id { get; set; }
        string Nome { get; set; }
        string Sobrenome { get; set; }
        string Email { get; set; }
        ICollection<Permissao> Permissoes { get; set; } 
    }
}