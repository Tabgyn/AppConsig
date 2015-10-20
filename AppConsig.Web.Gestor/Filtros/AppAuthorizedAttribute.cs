using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppConsig.Common.Security;
using AppConsig.Data;

namespace AppConsig.Web.Gestor.Filtros
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AppAuthorizedAttribute : AuthorizeAttribute
    {
        private readonly AppContext _context = new AppContext();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                // The user is not authenticated
                return false;
            }

            var usuarioLogado = httpContext.User as AppPrincipal;

            if (usuarioLogado == null)
            {
                return false;
            }

            if (usuarioLogado.IsAdmin)
            {
                return true;
            }

            var acao = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            var controle = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            var permissoes =
                _context.Usuarios.Include(u => u.Perfil)
                    .Include(u => u.Perfil.Permissoes)
                    .First(u => u.Id == usuarioLogado.Id).Perfil.Permissoes;

            authorized = permissoes.Any(permissao => permissao.Controle == controle && permissao.Acao == acao);

            return authorized;
        }
    }
}