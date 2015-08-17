using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using AppConsig.Web.Gestor.Modulos;
using AppConsig.Web.Gestor.Seguranca;
using Autofac;
using Autofac.Integration.Mvc;

namespace AppConsig.Web.Gestor
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Autofac Configuration.
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterModule(new ModuloServico());
            builder.RegisterModule(new ModuloEF());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            
            if (authCookie == null) return;
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var serializer = new JavaScriptSerializer();

            if (authTicket == null) return;
            var serializeModel = serializer.Deserialize<AppPrincipalSerializedModel>(authTicket.UserData);

            var newUser = new AppPrincipal(authTicket.Name)
                          {
                              Id = serializeModel.Id,
                              Nome = serializeModel.Nome,
                              Sobrenome = serializeModel.Sobrenome,
                              Email = serializeModel.Email
                          };

            HttpContext.Current.User = newUser;
        }

        protected void Application_End()
        {
            // clear authentication cookie
            var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie1);

            // clear session cookie
            var cookie2 = new HttpCookie("ASP.NET_SessionId", "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie2);
        }
        
        void Session_End(object sender, EventArgs e)
        {
            // clear authentication cookie
            var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie1);

            // clear session cookie
            var cookie2 = new HttpCookie("ASP.NET_SessionId", "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie2);
        }
    }
}
