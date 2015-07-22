using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppConsig.Web.Gestor.Models;

namespace AppConsig.Web.Gestor.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //var usuario = _servicoUsuario.ValidarUsuario(model.Login, model.Senha);
            }

            return View(model);
        }
    }
}