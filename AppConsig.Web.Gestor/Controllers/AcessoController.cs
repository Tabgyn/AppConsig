using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using AppConsig.Common.Security;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Base.Entities;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Resources;
using MvcSiteMapProvider;
using Newtonsoft.Json;

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
            if (!ModelState.IsValid) return View(model);
            if (!ValidateCaptcha()) return View(model);

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

                Erro(Exceptions.LoginOrPasswordInvalid, true);
            }
            catch (Exception exception)
            {
                Erro(exception.Message, true);
            }

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

        private bool ValidateCaptcha()
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            var secret = WebConfigurationManager.AppSettings["CaptchaSecretKey"];

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={response}");

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return false;

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        Erro("[reCaptcha] A chave secreta está faltando", true);
                        break;
                    case ("invalid-input-secret"):
                        Erro("[reCaptcha] A chave secreta é inválida ou mal formatada", true);
                        break;
                    case ("missing-input-response"):
                        Erro("Por favor resolva o captcha", true);
                        break;
                    case ("invalid-input-response"):
                        Erro("[reCaptcha] O parâmetro de resposta é inválido ou mal formatado", true);
                        break;
                    default:
                        Erro("[reCaptcha] Ocorreu um erro. Por favor tente novamente", true);
                        break;
                }
            }
            else
            {
                return true;
            }

            return false;
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