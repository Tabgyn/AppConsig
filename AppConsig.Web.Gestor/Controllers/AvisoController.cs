using System.Collections.Generic;
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

        // GET: Aviso
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //var avisos = _servicoAviso.ObterTodos().ToList();

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    avisos = avisos.Where(a => a.CriadoPor.Contains(searchString)
            //    || a.Texto.Contains(searchString)).ToList();
            //}

            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        avisos = avisos.OrderByDescending(a => a.CriadoPor).ToList();
            //        break;
            //    case "Date":
            //        avisos = avisos.OrderBy(a => a.DataCriacao).ToList();
            //        break;
            //    case "date_desc":
            //        avisos = avisos.OrderByDescending(a => a.DataCriacao).ToList();
            //        break;
            //    default:
            //        avisos = avisos.OrderBy(a => a.CriadoPor).ToList();
            //        break;
            //}

            //const int pageSize = 5;
            //int pageNumber = (page ?? 1);

            //return View(avisos.ToPagedList(pageNumber, pageSize));

            return View(new List<Aviso>().ToPagedList(1, 10));
        }

        // GET: Aviso/Detalhar/5
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aviso aviso = _servicoAviso.ObterPeloId(id.Value);

            if (aviso == null)
            {
                return HttpNotFound();
            }

            return View(aviso);
        }

        // GET: Aviso/Criar
        public ActionResult Criar()
        {
            return View();
        }

        // POST: Aviso/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Id,Texto")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                _servicoAviso.Criar(aviso);
                return RedirectToAction("Index");
            }

            return View(aviso);
        }

        // GET: Aviso/Editar/5
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aviso aviso = _servicoAviso.ObterPeloId(id.Value);

            if (aviso == null)
            {
                return HttpNotFound();
            }

            return View(aviso);
        }

        // POST: Aviso/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Texto")] Aviso aviso)
        {
            if (ModelState.IsValid)
            {
                _servicoAviso.Atualizar(aviso);
                return RedirectToAction("Index");
            }

            return View(aviso);
        }

        // GET: Aviso/Excluir/5
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aviso aviso = _servicoAviso.ObterPeloId(id.Value);

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
            Aviso person = _servicoAviso.ObterPeloId(id);

            _servicoAviso.Excluir(person);

            return RedirectToAction("Index");
        }
    }
}