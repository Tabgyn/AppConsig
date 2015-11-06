using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Models;
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
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "date_desc" : sortOrder;
            ViewBag.CurrentSort = sortOrder;
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

            ViewBag.CurrentFilter = searchString;

            var avisos = _servicoAviso.ObterTodos(a => a.Excluido == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                DateTime dateString;
                if (DateTime.TryParse(searchString, out dateString))
                {
                    avisos = avisos.Where(a => a.CriadoEm.Date <= dateString.Date).ToList();
                }
                else
                {
                    avisos = avisos.Where(a => a.CriadoPor.Contains(searchString)
                    || a.Texto.Contains(searchString)).ToList();
                }
            }

            switch (sortOrder)
            {
                case "own":
                    avisos = avisos.OrderBy(a => a.CriadoPor).ToList();
                    break;
                case "own_desc":
                    avisos = avisos.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    avisos = avisos.OrderBy(a => a.CriadoEm).ToList();
                    break;
                default:
                    avisos = avisos.OrderByDescending(a => a.CriadoEm).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            var avisosModel = avisos.Select(aviso => new AvisoListModel
            {
                Id = aviso.Id,
                Texto = aviso.Texto,
                CriadoPor = aviso.CriadoPor,
                CriadoEm = aviso.CriadoEm.ToString("dd/MM/yyyy hh:mm:ss")
            }).ToList();

            ViewBag.TotalRegisters = avisosModel.Count;

            return View(avisosModel.ToPagedList(pageNumber, pageSize));
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

            var avisoModel = new AvisoEditModel
            {
                Id = aviso.Id,
                Texto = aviso.Texto
            };

            return View(avisoModel);
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
        public ActionResult Criar([Bind(Include = "Texto")] AvisoEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var aviso = new Aviso { Texto = model.Texto };
                    _servicoAviso.Criar(aviso);
                    Success(Alerts.Success, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(model);
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

            var model = new AvisoEditModel
            {
                Id = aviso.Id,
                Texto = aviso.Texto
            };

            return View(model);
        }

        // POST: /Aviso/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Texto")] AvisoEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var aviso = _servicoAviso.ObterPeloId(model.Id);

                    aviso.Texto = model.Texto;
                    _servicoAviso.Atualizar(aviso);
                    Success(Alerts.Success, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(model);
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

            var model = new AvisoEditModel
            {
                Id = aviso.Id,
                Texto = aviso.Texto
            };

            return View(model);
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
                Success(Alerts.Success, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            var model = new AvisoEditModel
            {
                Id = aviso.Id,
                Texto = aviso.Texto
            };

            return View(model);
        }
    }
}