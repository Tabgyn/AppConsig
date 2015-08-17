using System.Web.Mvc;

namespace AppConsig.Web.Gestor.Controllers
{
    public class VisaoGeralController : BaseController
    {
        // GET: VisaoGeral
        public ActionResult Index()
        {
            Atencao(
                "Lorem ipsum dolor sit amet, usu mucius audiam admodum at. Eam duis sadipscing an, ad pro vivendo perfecto.",
                true);
            Successo(
                "Lorem ipsum dolor sit amet, usu mucius audiam admodum at. Eam duis sadipscing an, ad pro vivendo perfecto.",
                true);
            Informacao(
                "Lorem ipsum dolor sit amet, usu mucius audiam admodum at. Eam duis sadipscing an, ad pro vivendo perfecto.",
                true);
            Erro(
                "Lorem ipsum dolor sit amet, usu mucius audiam admodum at. Eam duis sadipscing an, ad pro vivendo perfecto.",
                true);
            return View();
        }
    }
}