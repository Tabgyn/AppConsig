using AppConsig.Data;
using Autofac;

namespace AppConsig.Web.Gestor.Modulos
{
    public class EntityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(AppContext)).As(typeof(IContext)).InstancePerLifetimeScope();
        }
    }
}