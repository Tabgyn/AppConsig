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
    public class OrgaoController : BaseController
    {
        readonly IDepartamentService _departamentService;

        public OrgaoController(IDepartamentService departamentService)
        {
            _departamentService = departamentService;
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

            var departments = _departamentService.GetAll(a => a.Deleted == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                departments = departments.Where(a => a.CreateBy.ToUpper().Contains(searchString.ToUpper())
                || a.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    departments = departments.OrderByDescending(a => a.Name).ToList();
                    break;
                case "own":
                    departments = departments.OrderBy(a => a.CreateBy).ToList();
                    break;
                case "own_desc":
                    departments = departments.OrderByDescending(a => a.CreateBy).ToList();
                    break;
                case "date":
                    departments = departments.OrderBy(a => a.CreateDate).ToList();
                    break;
                case "date_desc":
                    departments = departments.OrderByDescending(a => a.CreateDate).ToList();
                    break;
                default:
                    departments = departments.OrderBy(a => a.Name).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(departments.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Orgao/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = _departamentService.GetById(id.Value);

            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // GET: /Orgao/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            ViewBag.SistemasFolha = _departamentService.GetHumanResourceSystems();

            return View();
        }

        // POST: /Orgao/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Criar([Bind(Include = "Codigo,Nome,Descricao,SistemaFolhaId")] Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departamentService.Insert(department);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            ViewBag.SistemasFolha = _departamentService.GetHumanResourceSystems();

            return View(department);
        }

        // GET: /Orgao/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = _departamentService.GetById(id.Value);

            if (department == null)
            {
                return HttpNotFound();
            }

            ViewBag.SistemasFolha = _departamentService.GetHumanResourceSystems();

            return View(department);
        }

        // POST: /Orgao/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Editar([Bind(Include = "Id,Codigo,Nome,Descricao,SistemaFolhaId")] Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departamentService.Update(department);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            ViewBag.SistemasFolha = _departamentService.GetHumanResourceSystems();

            return View(department);
        }

        // GET: /Orgao/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = _departamentService.GetById(id.Value);

            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // POST: /Orgao/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult ConfirmarExcluir(long id)
        {
            var department = _departamentService.GetById(id);

            if (department == null)
            {
                return HttpNotFound();
            }

            try
            {
                _departamentService.Delete(department);
                Success(Alerts.Sucess, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            return View(department);
        }
    }
}