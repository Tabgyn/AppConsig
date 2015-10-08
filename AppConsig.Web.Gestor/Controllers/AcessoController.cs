using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using AppConsig.Common.Security;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Filtros;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Resources;
using MvcSiteMapProvider;

namespace AppConsig.Web.Gestor.Controllers
{
    [AllowAnonymous]
    public class AcessoController : BaseController
    {
        readonly IUserService _userService;

        public AcessoController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Acesso
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Acesso
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Login(AcessoModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_userService.ValidateUser(model.Email, model.Senha))
                    {
                        var user = _userService.GetAll(u => u.Email == model.Email).First();

                        var serializeModel = new AppPrincipalSerializedModel
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Surname = user.Surname,
                            Email = user.Email,
                            IsAdmin = user.IsAdmin
                        };

                        LimparDadosDoUsuario();

                        Session.Add("Avatar", user.Picture);

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
        public ActionResult CriarNovaSenha()
        {
            return View();
        }

        // POST: ReeviarSenha
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult CriarNovaSenha(CriarNovaSenhaModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetAll(u => u.Email == model.Email).FirstOrDefault();

                if (user != null)
                {
                    try
                    {
                        _userService.ResetPassword(user);

                        return RedirectToAction("Login");
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

        [Audit]
        public ActionResult Logout()
        {
            LimparDadosDoUsuario();

            return RedirectToAction("Login");
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