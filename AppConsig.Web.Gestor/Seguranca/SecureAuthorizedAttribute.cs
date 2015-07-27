using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppConsig.Web.Gestor.Seguranca
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SecureAuthorizedAttribute : AuthorizeAttribute
    {
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

            var action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            var controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var autorizado = usuarioLogado.Permissoes.Any(permissao => permissao.Controller == controller && permissao.Action == action);
            return autorizado;
        }
    }
}