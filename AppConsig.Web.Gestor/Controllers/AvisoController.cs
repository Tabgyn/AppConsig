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
    public class AvisoController : BaseController
    {
        readonly INoticeService _noticeService;

        public AvisoController(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        // GET: /Aviso
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

            var notices = _noticeService.GetAll(a => a.Deleted == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                notices = notices.Where(a => a.CreateBy.Contains(searchString)
                || a.Text.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "own":
                    notices = notices.OrderBy(a => a.CreateBy).ToList();
                    break;
                case "own_desc":
                    notices = notices.OrderByDescending(a => a.CreateBy).ToList();
                    break;
                case "date":
                    notices = notices.OrderBy(a => a.CreateDate).ToList();
                    break;
                default:
                    notices = notices.OrderByDescending(a => a.CreateDate).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(notices.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Aviso/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notice = _noticeService.GetById(id.Value);

            if (notice == null)
            {
                return HttpNotFound();
            }

            return View(notice);
        }

        // GET: /Aviso/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        // POST: /Aviso/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Criar([Bind(Include = "Text")] Notice notice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _noticeService.Insert(notice);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(notice);
        }

        // GET: /Aviso/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notice = _noticeService.GetById(id.Value);

            if (notice == null)
            {
                return HttpNotFound();
            }

            return View(notice);
        }

        // POST: /Aviso/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Editar([Bind(Include = "Id,Text")] Notice notice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _noticeService.Update(notice);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(notice);
        }

        // GET: /Aviso/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notice = _noticeService.GetById(id.Value);

            if (notice == null)
            {
                return HttpNotFound();
            }

            return View(notice);
        }

        // POST: /Aviso/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult ConfirmarExcluir(long id)
        {
            var notice = _noticeService.GetById(id);

            if (notice == null)
            {
                return HttpNotFound();
            }

            try
            {
                _noticeService.Delete(notice);
                Success(Alerts.Sucess, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            return View(notice);
        }
    }
}