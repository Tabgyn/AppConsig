using System.Reflection;
using Autofac;

namespace AppConsig.Web.Gestor.Modulos
{
    public class ModuloServico : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("AppConsig.Servicos"))

                      .Where(t => t.Name.StartsWith("Servico"))

                      .AsImplementedInterfaces()

                      .InstancePerLifetimeScope();
        }
    }
}