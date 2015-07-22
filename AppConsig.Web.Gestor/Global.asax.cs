using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using AppConsig.Dados;
using AppConsig.Web.Gestor.Modulos;
using AppConsig.Web.Gestor.Seguranca;
using Autofac;
using Autofac.Integration.Mvc;

namespace AppConsig.Web.Gestor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Autofac Configuration 
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            
            builder.RegisterModule(new ModuloServico());
            builder.RegisterModule(new ModuloEF());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                AppPrincipalSerializedModel serializeModel = serializer.Deserialize<AppPrincipalSerializedModel>(authTicket.UserData);

                AppPrincipal newUser = new AppPrincipal(authTicket.Name);
                newUser.Id = serializeModel.Id;
                newUser.Nome = serializeModel.Nome;
                newUser.Sobrenome = serializeModel.Sobrenome;
                newUser.Permissoes = serializeModel.Permissoes;

                HttpContext.Current.User = newUser;
            }
        }
    }
}
