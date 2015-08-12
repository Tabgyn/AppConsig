using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AppConsig.Comum;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class PerfilController : Controller
    {
        readonly IServicoPerfil _servicoPerfil;
        readonly IServicoPermissao _servicoPermissao;
        readonly IServicoUsuario _servicoUsuario;

        public PerfilController(IServicoPerfil servicoPerfil, IServicoPermissao servicoPermissao, IServicoUsuario servicoUsuario)
        {
            _servicoPerfil = servicoPerfil;
            _servicoPermissao = servicoPermissao;
            _servicoUsuario = servicoUsuario;
        }

        // GET: /Perfil
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? itemsPerPage)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "" : "name_desc";
            ViewBag.OwnerSortParam = sortOrder == "own" ? "own_desc" : "own";
            ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var perfis = _servicoPerfil.ObterTodos(a => a.Excluido == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                perfis = perfis.Where(a => a.CriadoPor.Contains(searchString)
                || a.Nome.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    perfis = perfis.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "own":
                    perfis = perfis.OrderBy(a => a.CriadoPor).ToList();
                    break;
                case "own_desc":
                    perfis = perfis.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    perfis = perfis.OrderBy(a => a.DataCriacao).ToList();
                    break;
                case "date_desc":
                    perfis = perfis.OrderByDescending(a => a.DataCriacao).ToList();
                    break;
                default:
                    perfis = perfis.OrderBy(a => a.Nome).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(perfis.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Perfil/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aviso = _servicoPerfil.ObterPeloId(id.Value);

            if (aviso == null)
            {
                return HttpNotFound();
            }

            return View(aviso);
        }

        // GET: /Perfil/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            var permissoes = _servicoPermissao.ObterTodos().Where(p => p.Visivel || p.IsCrud);
            var jsonModel = new JavaScriptSerializer().Serialize(GetTreeData(permissoes));

            ViewBag.Permissoes = jsonModel;

            return View();
        }

        // POST: /Perfil/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Texto")] Perfil perfil, long[] ckbPermissoes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoPerfil.Criar(perfil);
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception);
                }
            }

            return View(perfil);
        }

        // GET: /Perfil/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var perfil = _servicoPerfil.ObterPeloId(id.Value);

            if (perfil == null)
            {
                return HttpNotFound();
            }

            return View(perfil);
        }

        // POST: /Perfil/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Texto")] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoPerfil.Atualizar(perfil);
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception);
                }
            }

            return View(perfil);
        }

        // GET: /Perfil/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var perfil = _servicoPerfil.ObterPeloId(id.Value);

            if (perfil == null)
            {
                return HttpNotFound();
            }

            return View(perfil);
        }

        // POST: /Perfil/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExcluir(long id)
        {
            var perfil = _servicoPerfil.ObterPeloId(id);

            if (perfil == null)
            {
                return HttpNotFound();
            }

            try
            {
                _servicoPerfil.Excluir(perfil);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception);
            }

            return View(perfil);
        }

        private static List<JsTreeNode> GetTreeData(IEnumerable<Permissao> permissoes, IEnumerable<Permissao> permissoesDoUsuario = null, long parentNodeId = 0)
        {
            var lista = permissoes.ToList();

            IList<Permissao> listaUsuario = permissoesDoUsuario?.ToList() ?? new List<Permissao>();

            var pList = lista.Where(p => p.ParenteId == parentNodeId) as IList<Permissao> ??
                             lista.Where(p => p.ParenteId == parentNodeId).ToList();

            return pList.Select(item => new JsTreeNode
            {
                id = item.Id,
                text = item.Nome,
                state = new state
                {
                    selected = listaUsuario.Any(p => p.ParenteId == parentNodeId)
                },
                children = GetTreeData(lista, listaUsuario, item.Id)
            }).ToList();
        }
    }
}