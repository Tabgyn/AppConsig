using System.Web.Mvc;
using AppConsig.Web.Gestor.Filtros;

namespace AppConsig.Web.Gestor
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AppAuthorizedAttribute());
        }
    }
}
