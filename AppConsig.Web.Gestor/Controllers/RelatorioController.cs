using System.Web.Mvc;

namespace AppConsig.Web.Gestor.Controllers
{
    public class RelatorioController : BaseController
    {
        // GET: Relatorio de contratos
        public ActionResult Contrato()
        {
            return View();
        }

        // GET: Relatorio de servidores
        public ActionResult Servidor()
        {
            return View();
        }
    }
}