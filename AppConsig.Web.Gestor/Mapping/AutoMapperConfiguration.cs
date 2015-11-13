using AutoMapper;

namespace AppConsig.Web.Gestor.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ConsignatariaMapping());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}