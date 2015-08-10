using System;
using System.Net;
using System.Web.Mvc;
using AppConsig.Servicos.Interfaces;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Seguranca;

namespace AppConsig.Web.Gestor.Controllers
{
    public class UsuarioController : BaseController
    {
        readonly IServicoUsuario _servicoUsuario;

        public UsuarioController(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        // GET: Usuario
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Conta
        [HttpGet]
        public ActionResult Conta()
        {
            var usuarioLogado = User as AppPrincipal;

            if (usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var usuario = _servicoUsuario.ObterPeloId(usuarioLogado.Id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var model = new UsuarioModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Foto = usuario.Foto,
                Telefone = usuario.Telefone,
                Celular = usuario.Celular,
                Endereco = usuario.Endereco,
                EnderecoComplemento = usuario.EnderecoComplemento,
                Facebook = usuario.Facebook,
                Twitter = usuario.Twitter
            };

            return View(model);
        }

        // POST: Usuario/Conta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Conta(UsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _servicoUsuario.ObterPeloId(model.Id);

                    if (usuario == null)
                    {
                        return HttpNotFound();
                    }

                    usuario.Nome = model.Nome;
                    usuario.Sobrenome = model.Sobrenome;
                    usuario.Foto = model.Foto;
                    usuario.Telefone = model.Telefone;
                    usuario.Celular = model.Celular;
                    usuario.Endereco = model.Endereco;
                    usuario.EnderecoComplemento = model.EnderecoComplemento;
                    usuario.Facebook = model.Facebook;
                    usuario.Twitter = model.Twitter;

                    _servicoUsuario.Atualizar(usuario);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception);
                }
            }

            return View(model);
        }
    }
}