using System.Reflection;
using Autofac;

namespace AppConsig.Web.Gestor.Modulos
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("AppConsig.Services"))

                      .Where(t => t.Name.StartsWith("Service"))

                      .AsImplementedInterfaces()

                      .InstancePerLifetimeScope();
        }
    }
}