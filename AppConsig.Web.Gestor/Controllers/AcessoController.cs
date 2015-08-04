using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using AppConsig.Servicos.Interfaces;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Seguranca;
using MvcSiteMapProvider;

namespace AppConsig.Web.Gestor.Controllers
{
    [AllowAnonymous]
    public class AcessoController : Controller
    {
        readonly IServicoUsuario _servicoUsuario;

        public AcessoController(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

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
                try
                {
                    if (_servicoUsuario.ValidarUsuario(model.Email, model.Senha))
                    {
                        var usuario = _servicoUsuario.ObterTodos(u => u.Email == model.Email).First();

                        var serializeModel = new AppPrincipalSerializedModel
                        {
                            Id = usuario.Id,
                            Nome = usuario.Nome,
                            Sobrenome = usuario.Sobrenome,
                            Email = usuario.Email
                        };

                        var serializer = new JavaScriptSerializer();

                        var userData = serializer.Serialize(serializeModel);

                        var authTicket = new FormsAuthenticationTicket(
                            1,
                            usuario.Login,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            false,
                            userData);

                        var encTicket = FormsAuthentication.Encrypt(authTicket);
                        var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "VisaoGeral");
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception);
                }
            }

            ModelState.AddModelError("", "E-mail e/ou senha inválido(s)");

            return View(model);
        }

        // GET: ReeviarSenha
        public ActionResult CriarNovaSenha()
        {
            return View();
        }

        // POST: ReeviarSenha
        [HttpPost]
        public ActionResult CriarNovaSenha(CriarNovaSenhaModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _servicoUsuario.ObterTodos(u => u.Email == model.Email).FirstOrDefault();

                if (usuario != null)
                {
                    try
                    {
                        _servicoUsuario.ReeviarSenha(usuario);

                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        ModelState.AddModelError("", exception);
                    }
                }
            }

            ModelState.AddModelError("Email", "Não há usuário cadastrado para o e-mail informado");

            return View(model);
        }

        public ActionResult Sair()
        {
            SiteMaps.ReleaseSiteMap();

            FormsAuthentication.SignOut();
            
            return RedirectToAction("Index");
        }
    }
}