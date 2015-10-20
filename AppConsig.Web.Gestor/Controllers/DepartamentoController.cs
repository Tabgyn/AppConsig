using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Filtros;
using AppConsig.Web.Gestor.Resources;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class DepartamentoController : BaseController
    {
        readonly IServicoDepartamento _servicoDepartamento;

        public DepartamentoController(IServicoDepartamento servicoDepartamento)
        {
            _servicoDepartamento = servicoDepartamento;
        }

        // GET: Departamento
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

            var departamentos = _servicoDepartamento.ObterTodos(a => a.Deleted == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                departamentos = departamentos.Where(a => a.CreateBy.ToUpper().Contains(searchString.ToUpper())
                || a.Nome.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    departamentos = departamentos.OrderByDescending(a => a.Nome).ToList();
                    break;
                case "own":
                    departamentos = departamentos.OrderBy(a => a.CreateBy).ToList();
                    break;
                case "own_desc":
                    departamentos = departamentos.OrderByDescending(a => a.CreateBy).ToList();
                    break;
                case "date":
                    departamentos = departamentos.OrderBy(a => a.CreateDate).ToList();
                    break;
                case "date_desc":
                    departamentos = departamentos.OrderByDescending(a => a.CreateDate).ToList();
                    break;
                default:
                    departamentos = departamentos.OrderBy(a => a.Nome).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(departamentos.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Departamento/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var departamento = _servicoDepartamento.ObterPeloId(id.Value);

            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // GET: /Departamento/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            ViewBag.SistemasFolha = _servicoDepartamento.ObterSistemasFolha();

            return View();
        }

        // POST: /Departamento/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Criar([Bind(Include = "Codigo,Nome,Descricao,SistemaFolhaId")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoDepartamento.Criar(departamento);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            ViewBag.SistemasFolha = _servicoDepartamento.ObterSistemasFolha();

            return View(departamento);
        }

        // GET: /Departamento/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var departamento = _servicoDepartamento.ObterPeloId(id.Value);

            if (departamento == null)
            {
                return HttpNotFound();
            }

            ViewBag.SistemasFolha = _servicoDepartamento.ObterSistemasFolha();

            return View(departamento);
        }

        // POST: /Departamento/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Editar([Bind(Include = "Id,Codigo,Nome,Descricao,SistemaFolhaId")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoDepartamento.Atualizar(departamento);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            ViewBag.SistemasFolha = _servicoDepartamento.ObterSistemasFolha();

            return View(departamento);
        }

        // GET: /Departamento/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var departamento = _servicoDepartamento.ObterPeloId(id.Value);

            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // POST: /Departamento/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult ConfirmarExcluir(long id)
        {
            var departamento = _servicoDepartamento.ObterPeloId(id);

            if (departamento == null)
            {
                return HttpNotFound();
            }

            try
            {
                _servicoDepartamento.Excluir(departamento);
                Success(Alerts.Sucess, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            return View(departamento);
        }
    }
}