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

            var loggedUser = httpContext.User as AppPrincipal;

            if (loggedUser == null)
            {
                return false;
            }

            if (loggedUser.IsAdmin)
            {
                return true;
            }

            var action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            var controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            var permissions =
                _context.Users.Include(u => u.Profile)
                    .Include(u => u.Profile.Permissions)
                    .First(u => u.Id == loggedUser.Id).Profile.Permissions;

            authorized = permissions.Any(permissao => permissao.Controller == controller && permissao.Action == action);

            return authorized;
        }
    }
}