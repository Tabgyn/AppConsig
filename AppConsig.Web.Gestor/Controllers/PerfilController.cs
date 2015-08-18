using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;
using AppConsig.Web.Base.Entidades;
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

            var perfil = _servicoPerfil.ObterPeloId(id.Value);

            if (perfil == null)
            {
                return HttpNotFound();
            }

            var permissoes = _servicoPermissao.ObterTodos().Where(p => p.Visivel || p.IsCrud);
            var permissoesSelecionadas = _servicoPermissao.ObterPermissoesDoPerfil(perfil.Id);
            ViewBag.Permissoes = GetTreeData(permissoes, permissoesSelecionadas);

            return View(perfil);
        }

        // GET: /Perfil/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            var permissoes = _servicoPermissao.ObterTodos().Where(p => p.Visivel || p.IsCrud);
            ViewBag.Permissoes = GetTreeData(permissoes);

            return View();
        }

        // POST: /Perfil/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Nome, Descricao")] Perfil perfil, long[] ckbPermissoes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ckbSelecionadas =
                        ckbPermissoes.Select(ckb => _servicoPermissao.ObterPeloId(ckb)).ToList();

                    perfil.Permissoes = ckbSelecionadas;

                    _servicoPerfil.Criar(perfil);
                    Successo("Novo perfil de usuário criado", true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(exception.Message, true);
                }
            }

            var permissoes = _servicoPermissao.ObterTodos().Where(p => p.Visivel || p.IsCrud);
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

            var permissoes = _servicoPermissao.ObterTodos().Where(p => p.Visivel || p.IsCrud);
            var permissoesSelecionadas = _servicoPermissao.ObterPermissoesDoPerfil(perfil.Id);
            ViewBag.Permissoes = GetTreeData(permissoes, permissoesSelecionadas);

            return View(perfil);
        }

        // POST: /Perfil/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id, Nome, Descricao, Permissoes")] Perfil perfil, long[] ckbPermissoes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ckbSelecionadas =
                        ckbPermissoes.Select(ckb => _servicoPermissao.ObterPeloId(ckb)).ToList();

                    perfil.Permissoes = ckbSelecionadas;

                    var oldPerfil = _servicoPerfil.ObterPerfilComPermissoes(perfil.Id);

                    oldPerfil.Nome = perfil.Nome;
                    oldPerfil.Descricao = perfil.Descricao;
                    oldPerfil.Permissoes = oldPerfil.Permissoes.Where(p => ckbSelecionadas.Contains(p)).ToList();

                    _servicoPerfil.Atualizar(oldPerfil);
                    Successo("Perfil de usuário atualizado", true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(exception.Message, true);
                }
            }

            var permissoes = _servicoPermissao.ObterTodos().Where(p => p.Visivel || p.IsCrud);
            var permissoesSelecionadas = _servicoPermissao.ObterPermissoesDoPerfil(perfil.Id);
            ViewBag.Permissoes = GetTreeData(permissoes, permissoesSelecionadas);

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

            var permissoes = _servicoPermissao.ObterTodos().Where(p => p.Visivel || p.IsCrud);
            var permissoesSelecionadas = _servicoPermissao.ObterPermissoesDoPerfil(perfil.Id);
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

            try
            {
                _servicoPerfil.Excluir(perfil);
                Successo("Perfil de usuário excluído", true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(exception.Message, true);
            }

            return View(perfil);
        }

        private static List<TreeViewNode> GetTreeData(IEnumerable<Permissao> permissoes, IEnumerable<Permissao> permissoesSelecionadas = null, long parentNodeId = 0)
        {
            var lista = permissoes.ToList();

            IList<Permissao> listaUsuario = permissoesSelecionadas?.ToList() ?? new List<Permissao>();

            var pList = lista.Where(p => p.ParenteId == parentNodeId) as IList<Permissao> ??
                             lista.Where(p => p.ParenteId == parentNodeId).ToList();

            return pList.Select(item => new TreeViewNode
            {
                Id = item.Id,
                Texto = item.Nome,
                ParenteId = item.ParenteId,
                Selecionado = listaUsuario.Any(p => p.Id == item.Id),
                Filhos = GetTreeData(lista, listaUsuario, item.Id)
            }).ToList();
        }
    }
}