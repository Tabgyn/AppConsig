using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using AppConsig.Common.Security;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Resources;
using MvcSiteMapProvider;

namespace AppConsig.Web.Gestor.Controllers
{
    [AllowAnonymous]
    public class AcessoController : BaseController
    {
        readonly IServicoUsuario _servicoUsuario;

        public AcessoController(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        // GET: Acesso
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        // POST: Acesso
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AcessoModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_servicoUsuario.ValidarUsuario(model.Email, model.Senha))
                    {
                        var user = _servicoUsuario.ObterTodos(u => u.Email == model.Email).First();

                        var serializeModel = new AppPrincipalSerializedModel
                        {
                            Id = user.Id,
                            Name = user.Nome,
                            Surname = user.Sobrenome,
                            Email = user.Email,
                            IsAdmin = user.EhAdministrador
                        };

                        LimparDadosDoUsuario();

                        var serializer = new JavaScriptSerializer();

                        var userData = serializer.Serialize(serializeModel);

                        var authTicket = new FormsAuthenticationTicket(
                            1,
                            user.Email,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(30),
                            false,
                            userData);

                        var encTicket = FormsAuthentication.Encrypt(authTicket);
                        var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "VisaoGeral");
                    }
                }
                catch (Exception exception)
                {
                    Erro(exception.Message, true);
                }
            }

            //ModelState.AddModelError("", Excecoes.EmailSenhaInvalido);
            Erro(Exceptions.LoginOrPasswordInvalid, true);

            return View(model);
        }

        // GET: ReeviarSenha
        public ActionResult ResetarSenha()
        {
            return View();
        }

        // POST: ReeviarSenha
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetarSenha(CriarNovaSenhaModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _servicoUsuario.ObterTodos(u => u.Email == model.Email).FirstOrDefault();

                if (user != null)
                {
                    try
                    {
                        _servicoUsuario.ResetarSenha(user);

                        Success("Uma nova senha foi criada, por favor verique seu e-mail", true);

                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        Erro(exception.Message, true);
                    }
                }
            }

            ModelState.AddModelError("Email", Exceptions.InvalidUser);

            return View(model);
        }

        public ActionResult Sair()
        {
            LimparDadosDoUsuario();

            return RedirectToAction("Index");
        }

        private void LimparDadosDoUsuario()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            SiteMaps.ReleaseSiteMap();

            // clear authentication cookie
            var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie1);

            // clear session cookie
            var cookie2 = new HttpCookie("ASP.NET_SessionId", "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie2);
        }
    }
}