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
        readonly IServicoAviso _servicoAviso;

        public AvisoController(IServicoAviso servicoAviso)
        {
            _servicoAviso = servicoAviso;
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

            var avisos = _servicoAviso.ObterTodos(a => a.Deleted == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                avisos = avisos.Where(a => a.CreateBy.Contains(searchString)
                || a.Texto.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "own":
                    avisos = avisos.OrderBy(a => a.CreateBy).ToList();
                    break;
                case "own_desc":
                    avisos = avisos.OrderByDescending(a => a.CreateBy).ToList();
                    break;
                case "date":
                    avisos = avisos.OrderBy(a => a.CreateDate).ToList();
                    break;
                default:
                    avisos = avisos.OrderByDescending(a => a.CreateDate).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(avisos.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Aviso/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aviso = _servicoAviso.ObterPeloId(id.Value);

            if (aviso == null)
            {
                return HttpNotFound();
            }

            return View(aviso);
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
        public ActionResult Criar([Bind(Include = "Texto")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoAviso.Criar(aviso);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(aviso);
        }

        // GET: /Aviso/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aviso = _servicoAviso.ObterPeloId(id.Value);

            if (aviso == null)
            {
                return HttpNotFound();
            }

            return View(aviso);
        }

        // POST: /Aviso/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Editar([Bind(Include = "Id,Texto")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoAviso.Atualizar(aviso);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(aviso);
        }

        // GET: /Aviso/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var aviso = _servicoAviso.ObterPeloId(id.Value);

            if (aviso == null)
            {
                return HttpNotFound();
            }

            return View(aviso);
        }

        // POST: /Aviso/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult ConfirmarExcluir(long id)
        {
            var aviso = _servicoAviso.ObterPeloId(id);

            if (aviso == null)
            {
                return HttpNotFound();
            }

            try
            {
                _servicoAviso.Excluir(aviso);
                Success(Alerts.Sucess, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            return View(aviso);
        }
    }
}