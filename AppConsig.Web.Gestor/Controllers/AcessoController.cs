using System.Web.Mvc;
using AppConsig.Web.Gestor.Models;

namespace AppConsig.Web.Gestor.Controllers
{
    [AllowAnonymous]
    public class AcessoController : Controller
    {
        // GET: Acesso
        public ActionResult Index()
        {
            return View();
        }

        // POST: Acesso
        [HttpPost]
        public ActionResult Index(AcessoModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View(model);
        }

        // GET: ReeviarSenha
        public ActionResult NovaSenha()
        {
            return View();
        }

        // POST: ReeviarSenha
        [HttpPost]
        public ActionResult NovaSenha(NovaSenhaModel model)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View(model);
        }
    }
}