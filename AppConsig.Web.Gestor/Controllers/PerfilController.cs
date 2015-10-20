using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Base.Entities;
using AppConsig.Web.Gestor.Filtros;
using AppConsig.Web.Gestor.Resources;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class PerfilController : BaseController
    {
        readonly IServicoPerfil _servicoPerfil;
        readonly IServicoPermissao _servicoPermissao;

        public PerfilController(IServicoPerfil servicoPerfil, IServicoPermissao servicoPermissao)
        {
            _servicoPerfil = servicoPerfil;
            _servicoPermissao = servicoPermissao;
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

            var perfis = _servicoPerfil.ObterTodos(a => a.Deleted == false && a.EhEditavel).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                perfis = perfis.Where(a => a.CreateBy.Contains(searchString)
                || a.Nome.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    perfis = perfis.OrderByDescending(a => a.Nome).ToList();
                    break;
                case "own":
                    perfis = perfis.OrderBy(a => a.CreateBy).ToList();
                    break;
                case "own_desc":
                    perfis = perfis.OrderByDescending(a => a.CreateBy).ToList();
                    break;
                case "date":
                    perfis = perfis.OrderBy(a => a.CreateDate).ToList();
                    break;
                case "date_desc":
                    perfis = perfis.OrderByDescending(a => a.CreateDate).ToList();
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

            var perfil = _servicoPerfil.ObterPeloId(id.Value);

            if (perfil == null)
            {
                return HttpNotFound();
            }

            var permissoes = _servicoPermissao.ObterTodos(p => p.EhPadrao == false);
            var permissoesDoPerfil = _servicoPerfil.ObterPerfilComPermissoes(perfil.Id).Permissoes;

            ViewBag.Permissoes = GetTreeData(permissoes, permissoesDoPerfil);

            return View(perfil);
        }

        // GET: /Perfil/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            var permissoes = _servicoPermissao.ObterTodos(p => p.EhPadrao == false);

            ViewBag.Permissoes = GetTreeData(permissoes);

            return View();
        }

        // POST: /Perfil/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Nome, Descricao")] Perfil perfil, long[] ckbPermissoesSelecionadas)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permissoesPadrao = _servicoPermissao.ObterTodos(p => p.EhPadrao).ToList();

                    foreach (var p in permissoesPadrao)
                    {
                        perfil.Permissoes.Add(p);
                    }

                    var permissoesSeleciondas =
                        ckbPermissoesSelecionadas.Select(ckb => _servicoPermissao.ObterPeloId(ckb)).ToList();

                    foreach (var s in permissoesSeleciondas)
                    {
                        perfil.Permissoes.Add(s);
                    }

                    perfil.EhEditavel = true;

                    _servicoPerfil.Criar(perfil);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            var permissoes = _servicoPermissao.ObterTodos(p => p.EhPadrao == false);

            ViewBag.Permissoes = GetTreeData(permissoes);

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

            if (!perfil.EhEditavel)
            {
                return RedirectToAction("Index");
            }

            var permissoes = _servicoPermissao.ObterTodos(p => p.EhPadrao == false);
            var permissoesDoPerfil = _servicoPerfil.ObterPerfilComPermissoes(perfil.Id).Permissoes;

            ViewBag.Permissoes = GetTreeData(permissoes, permissoesDoPerfil);

            return View(perfil);
        }

        // POST: /Perfil/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id, Nome, Descricao")] Perfil perfil, long[] ckbPermissoesSelecionadas)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var permissoesSelecionadas =
                        ckbPermissoesSelecionadas.Select(ckb => _servicoPermissao.ObterPeloId(ckb)).ToList();
                    var perfilOriginal = _servicoPerfil.ObterPerfilComPermissoes(perfil.Id);

                    if (!perfilOriginal.EhEditavel)
                    {
                        throw new Exception(Exceptions.ActionNotAllowed);
                    }

                    perfilOriginal.Nome = perfil.Nome;
                    perfilOriginal.Descricao = perfil.Descricao;
                    perfilOriginal.Permissoes = perfilOriginal.Permissoes.Where(p => permissoesSelecionadas.Contains(p)).ToList();

                    _servicoPerfil.Atualizar(perfilOriginal);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            var permissoes = _servicoPermissao.ObterTodos(p => p.EhPadrao == false);
            var permissoesDoPerfil = _servicoPerfil.ObterPerfilComPermissoes(perfil.Id).Permissoes;

            ViewBag.Permissoes = GetTreeData(permissoes, permissoesDoPerfil);

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

            if (!perfil.EhEditavel)
            {
                return RedirectToAction("Index");
            }

            var permissoes = _servicoPermissao.ObterTodos(p => p.EhPadrao == false);
            var permissoesSelecionadas = _servicoPerfil.ObterPerfilComPermissoes(perfil.Id).Permissoes;

            ViewBag.Permissoes = GetTreeData(permissoes, permissoesSelecionadas);

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

            if (!perfil.EhEditavel)
            {
                throw new Exception(Exceptions.ActionNotAllowed);
            }

            try
            {
                _servicoPerfil.Excluir(perfil);
                Success(Alerts.Sucess, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            return View(perfil);
        }

        private static List<TreeViewNode> GetTreeData(IEnumerable<Permissao> permissions, IEnumerable<Permissao> selectedPermissions = null, long parentNodeId = 0)
        {
            var list = permissions.ToList();

            IList<Permissao> userList = selectedPermissions?.ToList() ?? new List<Permissao>();

            var pList = list.Where(p => p.ParenteId == parentNodeId) as IList<Permissao> ??
                             list.Where(p => p.ParenteId == parentNodeId).ToList();

            return pList.Select(item => new TreeViewNode
            {
                Id = item.Id,
                Text = item.Nome,
                ParentId = item.ParenteId,
                Selected = userList.Any(p => p.Id == item.Id),
                Children = GetTreeData(list, userList, item.Id)
            }).ToList();
        }
    }
}