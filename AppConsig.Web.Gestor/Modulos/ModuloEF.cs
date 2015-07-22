using AppConsig.Dados;
using Autofac;

namespace AppConsig.Web.Gestor.Modulos
{
    public class ModuloEF : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(AppContexto)).As(typeof(IContexto)).InstancePerLifetimeScope();
        }
    }
}