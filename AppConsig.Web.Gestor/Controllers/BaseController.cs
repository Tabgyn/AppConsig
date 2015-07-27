using System.Web.Mvc;
using AppConsig.Web.Gestor.Seguranca;

namespace AppConsig.Web.Gestor.Controllers
{
    [SecureAuthorized]
    public class BaseController : Controller
    {
        
    }
}