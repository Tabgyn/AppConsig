using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using AppConsig.Comum.Seguranca;
using AppConsig.Servicos.Interfaces;
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
                        var usuario = _servicoUsuario.ObterTodos(u => u.Email == model.Email).First();

                        var serializeModel = new AppPrincipalSerializedModel
                        {
                            Id = usuario.Id,
                            Nome = usuario.Nome,
                            Sobrenome = usuario.Sobrenome,
                            Email = usuario.Email,
                            Admin = usuario.Admin
                        };

                        LimparDadosDoUsuario();

                        Session.Add("Avatar", usuario.Foto);

                        var serializer = new JavaScriptSerializer();

                        var userData = serializer.Serialize(serializeModel);

                        var authTicket = new FormsAuthenticationTicket(
                            1,
                            usuario.Email,
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
            Erro(Excecoes.EmailSenhaInvalido, true);

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
                        Erro(exception.Message, true);
                    }
                }
            }

            ModelState.AddModelError("Email", Excecoes.UsuarioInvalido);

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