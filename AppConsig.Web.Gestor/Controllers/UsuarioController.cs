using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Resources;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class UsuarioController : BaseController
    {
        readonly IServicoUsuario _servicoUsuario;

        public UsuarioController(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        // GET: /Usuario
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? itemsPerPage)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OwnerSortParam = sortOrder == "own" ? "own_desc" : "own";
            ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var usuarios = _servicoUsuario.ObterTodos(a => a.Excluido == false && a.EhAdministrador == false).ToList();
            var modelos = usuarios.Select(usuario => new UsuarioEditModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                CriadoPor = usuario.CriadoPor,
                DataCriacao = usuario.CriadoEm
            }).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                modelos = modelos.Where(a => a.CriadoPor.Contains(searchString)
                || a.NomeCompleto.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "own_desc":
                    modelos = modelos.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    modelos = modelos.OrderBy(a => a.DataCriacao).ToList();
                    break;
                case "date_desc":
                    modelos = modelos.OrderByDescending(a => a.DataCriacao).ToList();
                    break;
                default:
                    modelos = modelos.OrderBy(a => a.CriadoPor).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(modelos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Usuario/Conta
        [HttpGet]
        public ActionResult Conta()
        {
            var usuarioLogado = User;

            if (usuarioLogado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var usuario = _servicoUsuario.ObterPeloId(usuarioLogado.Id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var modelo = new UsuarioContaModel
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

            return View(modelo);
        }

        // POST: Usuario/Conta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Conta(UsuarioContaModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _servicoUsuario.ObterPeloId(modelo.Id);

                    if (usuario == null)
                    {
                        return HttpNotFound();
                    }

                    usuario.Nome = modelo.Nome;
                    usuario.Sobrenome = modelo.Sobrenome;
                    usuario.Foto = modelo.Foto;
                    usuario.Telefone = modelo.Telefone;
                    usuario.Celular = modelo.Celular;
                    usuario.Endereco = modelo.Endereco;
                    usuario.EnderecoComplemento = modelo.EnderecoComplemento;
                    usuario.Facebook = modelo.Facebook;
                    usuario.Twitter = modelo.Twitter;

                    var httpPostedFileBase = Request.Files[0];

                    if (httpPostedFileBase != null && httpPostedFileBase.ContentLength > 0)
                    {
                        var allowedExtensions = new[] { ".jpeg", ".jpg", ".png", ".gif" };
                        var extension = Path.GetExtension(httpPostedFileBase.FileName);
                        if (!allowedExtensions.Contains(extension))
                        {
                            throw new Exception(Exceptions.WrongImageFormat);
                        }

                        using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
                        {
                            var fileData = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
                            usuario.Foto = modelo.Foto = Convert.ToBase64String(fileData);
                        }
                    }

                    _servicoUsuario.Atualizar(usuario);
                    Success(Alerts.Success, true);
                }
                catch (Exception exception)
                {
                    Erro(exception.Message, true);
                }
            }
            
            return View(modelo);
        }

        // GET: /Usuario/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = _servicoUsuario.ObterPeloId(id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var model = new UsuarioEditModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
            };

            return View(model);
        }

        // GET: /Usuario/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        // POST: /Usuario/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Nome,Sobrenome,Email")] UsuarioEditModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario
                    {
                        Nome = modelo.Nome,
                        Sobrenome = modelo.Sobrenome,
                        Email = modelo.Email
                    };

                    // TODO: Se nenhum perfil for selecionado, o usuário deverá ser de perfil padrão

                    _servicoUsuario.Criar(usuario);
                    Success(Alerts.Success, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(modelo);
        }

        // GET: /Usuario/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = _servicoUsuario.ObterPeloId(id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            if (usuario.EhAdministrador)
            {
                return RedirectToAction("Index");
            }

            var model = new UsuarioEditModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
            };

            return View(model);
        }

        // POST: /Usuario/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,Sobrenome,Email")] UsuarioEditModel modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _servicoUsuario.ObterPeloId(modelo.Id);

                    if (usuario.EhAdministrador)
                    {
                        throw new Exception(Exceptions.ActionNotAllowed);
                    }

                    usuario.Nome = modelo.Nome;
                    usuario.Sobrenome = modelo.Sobrenome;
                    usuario.Email = modelo.Email;

                    _servicoUsuario.Atualizar(usuario);
                    Success(Alerts.Success, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(modelo);
        }

        // GET: /Usuario/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = _servicoUsuario.ObterPeloId(id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            if (usuario.EhAdministrador)
            {
                return RedirectToAction("Index");
            }

            var modelo = new UsuarioEditModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
            };

            return View(modelo);
        }

        // POST: /Usuario/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExcluir(long id)
        {
            var usuario = _servicoUsuario.ObterPeloId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            if (usuario.EhAdministrador)
            {
                throw new Exception(Exceptions.ActionNotAllowed);
            }

            try
            {
                _servicoUsuario.Excluir(usuario);
                Success(Alerts.Success, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            var modelo = new UsuarioEditModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
            };

            return View(modelo);
        }
    }
}