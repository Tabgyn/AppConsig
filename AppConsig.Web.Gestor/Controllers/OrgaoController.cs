using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;
using AppConsig.Web.Gestor.Resources;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class OrgaoController : BaseController
    {
        readonly IServicoOrgao _servicoOrgao;

        public OrgaoController(IServicoOrgao servicoOrgao)
        {
            _servicoOrgao = servicoOrgao;
        }

        // GET: Orgao
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

            var orgaos = _servicoOrgao.ObterTodos(a => a.Excluido == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                orgaos = orgaos.Where(a => a.CriadoPor.ToUpper().Contains(searchString.ToUpper())
                || a.Nome.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orgaos = orgaos.OrderByDescending(a => a.Nome).ToList();
                    break;
                case "own":
                    orgaos = orgaos.OrderBy(a => a.CriadoPor).ToList();
                    break;
                case "own_desc":
                    orgaos = orgaos.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    orgaos = orgaos.OrderBy(a => a.DataCriacao).ToList();
                    break;
                case "date_desc":
                    orgaos = orgaos.OrderByDescending(a => a.DataCriacao).ToList();
                    break;
                default:
                    orgaos = orgaos.OrderBy(a => a.Nome).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(orgaos.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Orgao/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var orgao = _servicoOrgao.ObterPeloId(id.Value);

            if (orgao == null)
            {
                return HttpNotFound();
            }

            return View(orgao);
        }

        // GET: /Orgao/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            ViewBag.SistemasFolha = _servicoOrgao.ObterSistemasFolha();

            return View();
        }

        // POST: /Orgao/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Codigo,Nome,Descricao,SistemaFolhaId")] Orgao orgao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoOrgao.Criar(orgao);
                    Successo(Alertas.Sucesso, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alertas.Erro, true, exception);
                }
            }

            ViewBag.SistemasFolha = _servicoOrgao.ObterSistemasFolha();

            return View(orgao);
        }

        // GET: /Orgao/Editar/5
        [HttpGet]
        public ActionResult Editar(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var orgao = _servicoOrgao.ObterPeloId(id.Value);

            if (orgao == null)
            {
                return HttpNotFound();
            }

            ViewBag.SistemasFolha = _servicoOrgao.ObterSistemasFolha();

            return View(orgao);
        }

        // POST: /Orgao/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Codigo,Nome,Descricao,SistemaFolhaId")] Orgao orgao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoOrgao.Atualizar(orgao);
                    Successo(Alertas.Sucesso, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alertas.Erro, true, exception);
                }
            }

            ViewBag.SistemasFolha = _servicoOrgao.ObterSistemasFolha();

            return View(orgao);
        }

        // GET: /Orgao/Excluir/5
        [HttpGet]
        public ActionResult Excluir(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var orgao = _servicoOrgao.ObterPeloId(id.Value);

            if (orgao == null)
            {
                return HttpNotFound();
            }

            return View(orgao);
        }

        // POST: /Orgao/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExcluir(Guid id)
        {
            var orgao = _servicoOrgao.ObterPeloId(id);

            if (orgao == null)
            {
                return HttpNotFound();
            }

            try
            {
                _servicoOrgao.Excluir(orgao);
                Successo(Alertas.Sucesso, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alertas.Erro, true, exception);
            }

            return View(orgao);
        }
    }
}