using System;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using AppConsig.Data;
using AppConsig.Entities;

namespace AppConsig.Web.Gestor.Filtros
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var action = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            var controller = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            var userName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous";
            var ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            var data = Json.Encode(new
            {
                request.Cookies,
                request.Headers,
                request.Files,
                request.Form,
                request.QueryString,
                request.Params
            });
            var sessionId = request.Cookies[FormsAuthentication.FormsCookieName] != null
                ? request.Cookies[FormsAuthentication.FormsCookieName].Value
                : "NoSession";
            var audit = new Audit
            {
                UserName = userName,
                SessaoId = sessionId,
                Action = action,
                Controller = controller,
                IpAddress = ipAddress,
                AuditDate = DateTime.Now,
                Data = data
            };
            var context = new AppContext();

            context.Audits.Add(audit);
            context.SaveChanges();

            base.OnActionExecuting(filterContext);
        }
    }
}