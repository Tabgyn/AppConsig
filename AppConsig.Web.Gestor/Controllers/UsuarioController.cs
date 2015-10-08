using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Filtros;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Resources;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class UsuarioController : BaseController
    {
        readonly IUserService _userService;

        public UsuarioController(IUserService userService)
        {
            _userService = userService;
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

            var users = _userService.GetAll(a => a.Deleted == false && a.IsAdmin == false).ToList();
            var models = users.Select(usuario => new UsuarioEditaModel
            {
                Id = usuario.Id,
                Nome = usuario.Name,
                Sobrenome = usuario.Surname,
                Email = usuario.Email,
                CriadoPor = usuario.CreateBy,
                DataCriacao = usuario.CreateDate
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
            var loggedUser = User;

            if (loggedUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var user = _userService.GetById(loggedUser.Id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new UsuarioContaModel
            {
                Id = user.Id,
                Nome = user.Name,
                Sobrenome = user.Surname,
                Foto = user.Picture,
                Telefone = user.PhoneNumber,
                Celular = user.MobileNumber,
                Endereco = user.Address,
                EnderecoComplemento = user.ComplementAddress,
                Facebook = user.Facebook,
                Twitter = user.Twitter
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
                    var user = _userService.GetById(model.Id);

                    if (user == null)
                    {
                        return HttpNotFound();
                    }

                    user.Name = model.Nome;
                    user.Surname = model.Sobrenome;
                    user.Picture = model.Foto;
                    user.PhoneNumber = model.Telefone;
                    user.MobileNumber = model.Celular;
                    user.Address = model.Endereco;
                    user.ComplementAddress = model.EnderecoComplemento;
                    user.Facebook = model.Facebook;
                    user.Twitter = model.Twitter;

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
                            user.Picture = model.Foto = Convert.ToBase64String(fileData);
                        }
                    }

                    _userService.Update(user);
                    Success(Alerts.Sucess, true);
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
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userService.GetById(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            var model = new UsuarioEditaModel
            {
                Id = user.Id,
                Nome = user.Name,
                Sobrenome = user.Surname,
                Email = user.Email,
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
        [Audit]
        public ActionResult Criar([Bind(Include = "Nome,Sobrenome,Email")] UsuarioEditaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new User
                    {
                        Name = model.Nome,
                        Surname = model.Sobrenome,
                        Email = model.Email
                    };

                    // TODO: Se nenhum perfil for selecionado, o usuário deverá ser de perfil padrão

                    _userService.Insert(usuario);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(model);
        }

        // GET: /Usuario/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userService.GetById(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (user.IsAdmin)
            {
                return RedirectToAction("Index");
            }

            var model = new UsuarioEditaModel
            {
                Id = user.Id,
                Nome = user.Name,
                Sobrenome = user.Surname,
                Email = user.Email,
            };

            return View(model);
        }

        // POST: /Usuario/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Editar([Bind(Include = "Id,Nome,Sobrenome,Email")] UsuarioEditaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.GetById(model.Id);

                    if (user.IsAdmin)
                    {
                        throw new Exception(Exceptions.ActionNotAllowed);
                    }

                    user.Name = model.Nome;
                    user.Surname = model.Sobrenome;
                    user.Email = model.Email;

                    _userService.Update(user);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(model);
        }

        // GET: /Usuario/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userService.GetById(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (user.IsAdmin)
            {
                return RedirectToAction("Index");
            }

            var model = new UsuarioEditaModel
            {
                Id = user.Id,
                Nome = user.Name,
                Sobrenome = user.Surname,
                Email = user.Email,
            };

            return View(model);
        }

        // POST: /Usuario/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult ConfirmarExcluir(long id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (user.IsAdmin)
            {
                throw new Exception(Exceptions.ActionNotAllowed);
            }

            try
            {
                _userService.Delete(user);
                Success(Alerts.Sucess, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            var model = new UsuarioEditaModel
            {
                Id = user.Id,
                Nome = user.Name,
                Sobrenome = user.Surname,
                Email = user.Email,
            };

            return View(model);
        }
    }
}