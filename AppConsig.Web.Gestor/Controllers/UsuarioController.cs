using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;
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

            var usuarios = _servicoUsuario.ObterTodos(a => a.Excluido == false && a.Admin == false).ToList();
            var models = usuarios.Select(usuario => new UsuarioEditaModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                CriadoPor = usuario.CriadoPor,
                DataCriacao = usuario.DataCriacao
            }).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                models = models.Where(a => a.CriadoPor.Contains(searchString)
                || a.NomeCompleto.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "own_desc":
                    models = models.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    models = models.OrderBy(a => a.DataCriacao).ToList();
                    break;
                case "date_desc":
                    models = models.OrderByDescending(a => a.DataCriacao).ToList();
                    break;
                default:
                    models = models.OrderBy(a => a.CriadoPor).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(models.ToPagedList(pageNumber, pageSize));
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

            var model = new UsuarioContaModel
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
        public ActionResult Conta(UsuarioContaModel model)
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

                    var httpPostedFileBase = Request.Files[0];

                    if (httpPostedFileBase != null && httpPostedFileBase.ContentLength > 0)
                    {
                        var allowedExtensions = new[] { ".jpeg", ".jpg", ".png", ".gif" };
                        var extension = Path.GetExtension(httpPostedFileBase.FileName);
                        if (!allowedExtensions.Contains(extension))
                        {
                            throw new Exception(Excecoes.ImagemFormatoIncorreto);
                        }

                        using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
                        {
                            var fileData = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
                            usuario.Foto = model.Foto = Convert.ToBase64String(fileData);
                        }
                    }

                    _servicoUsuario.Atualizar(usuario);
                    Successo(Alertas.Sucesso, true);
                }
                catch (Exception exception)
                {
                    Erro(exception.Message, true);
                }
            }

            Session.Add("Avatar", model.Foto);

            return View(model);
        }

        // GET: /Usuario/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(Guid? id)
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

            var model = new UsuarioEditaModel
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
        public ActionResult Criar([Bind(Include = "Nome,Sobrenome,Email")] UsuarioEditaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario
                    {
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Email = model.Email
                    };

                    // TODO: Se nenhum perfil for selecionado, o usuário deverá ser de perfil padrão

                    _servicoUsuario.Criar(usuario);
                    Successo(Alertas.Sucesso, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alertas.Erro, true, exception);
                }
            }

            return View(model);
        }

        // GET: /Usuario/Editar/5
        [HttpGet]
        public ActionResult Editar(Guid? id)
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

            if (usuario.Admin)
            {
                return RedirectToAction("Index");
            }

            var model = new UsuarioEditaModel
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
        public ActionResult Editar([Bind(Include = "Id,Nome,Sobrenome,Email")] UsuarioEditaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _servicoUsuario.ObterPeloId(model.Id);

                    if (usuario.Admin)
                    {
                        throw new Exception(Excecoes.AcaoNaoPermitida);
                    }

                    usuario.Nome = model.Nome;
                    usuario.Sobrenome = model.Sobrenome;
                    usuario.Email = model.Email;

                    _servicoUsuario.Atualizar(usuario);
                    Successo(Alertas.Sucesso, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alertas.Erro, true, exception);
                }
            }

            return View(model);
        }

        // GET: /Usuario/Excluir/5
        [HttpGet]
        public ActionResult Excluir(Guid? id)
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

            if (usuario.Admin)
            {
                return RedirectToAction("Index");
            }

            var model = new UsuarioEditaModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
            };

            return View(model);
        }

        // POST: /Usuario/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExcluir(Guid id)
        {
            var usuario = _servicoUsuario.ObterPeloId(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            if (usuario.Admin)
            {
                throw new Exception(Excecoes.AcaoNaoPermitida);
            }

            try
            {
                _servicoUsuario.Excluir(usuario);
                Successo(Alertas.Sucesso, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alertas.Erro, true, exception);
            }

            var model = new UsuarioEditaModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
            };

            return View(model);
        }
    }
}