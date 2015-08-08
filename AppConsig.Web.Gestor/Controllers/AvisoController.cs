using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;
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
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var avisos = _servicoAviso.ObterTodos(a => a.Excluido == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                avisos = avisos.Where(a => a.CriadoPor.Contains(searchString)
                || a.Texto.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    avisos = avisos.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "Date":
                    avisos = avisos.OrderBy(a => a.DataCriacao).ToList();
                    break;
                case "date_desc":
                    avisos = avisos.OrderByDescending(a => a.DataCriacao).ToList();
                    break;
                default:
                    avisos = avisos.OrderBy(a => a.CriadoPor).ToList();
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
        public ActionResult Criar([Bind(Include = "Texto")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoAviso.Criar(aviso);
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception);
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
        public ActionResult Editar([Bind(Include = "Id,Texto")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoAviso.Atualizar(aviso);
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception);
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

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception);
            }

            return View(aviso);
        }
    }
}