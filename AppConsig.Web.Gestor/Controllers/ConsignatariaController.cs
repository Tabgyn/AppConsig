using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Resources;
using AutoMapper;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class ConsignatariaController : BaseController
    {
        readonly IServicoConsignataria _servicoConsignataria;

        public ConsignatariaController(IServicoConsignataria servicoConsignataria)
        {
            _servicoConsignataria = servicoConsignataria;
        }

        // GET: /Consignataria
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? itemsPerPage)
        {
            //Baseado no default
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "name" : sortOrder;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
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

            var consignatarias = _servicoConsignataria.ObterTodos(a => a.Excluido == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                DateTime dateString;
                if (DateTime.TryParse(searchString, out dateString))
                {
                    consignatarias = consignatarias.Where(a => a.CriadoEm.Date <= dateString.Date).ToList();
                }
                else
                {
                    consignatarias = consignatarias.Where(a => a.CriadoPor.Contains(searchString)
                    || a.Nome.Contains(searchString)).ToList();
                }
            }

            switch (sortOrder)
            {
                case "own":
                    consignatarias = consignatarias.OrderBy(a => a.CriadoPor).ToList();
                    break;
                case "own_desc":
                    consignatarias = consignatarias.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    consignatarias = consignatarias.OrderBy(a => a.CriadoEm).ToList();
                    break;
                case "date_desc":
                    consignatarias = consignatarias.OrderByDescending(a => a.CriadoEm).ToList();
                    break;
                case "name_desc":
                    consignatarias = consignatarias.OrderByDescending(a => a.Nome).ToList();
                    break;
                default:
                    consignatarias = consignatarias.OrderBy(a => a.Nome).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            var model = Mapper.Map<IList<ConsignatariaListModel>>(consignatarias);

            ViewBag.ItemsPerPage = itemsPerPage;

            return View(model.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Consignataria/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consignataria = _servicoConsignataria.ObterPeloId(id.Value);

            if (consignataria == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ConsignatariaEditModel>(consignataria);

            return View(model);
        }

        // GET: /Consignataria/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        // POST: /Consignataria/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Nome,Sigla,Codigo,CNPJ,Email,TipoRepresentante")] ConsignatariaEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var consignataria = Mapper.Map<Consignataria>(model);

                    _servicoConsignataria.Criar(consignataria);
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

        // GET: /Consignataria/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consignataria = _servicoConsignataria.ObterPeloId(id.Value);

            if (consignataria == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ConsignatariaEditModel>(consignataria);

            return View(model);
        }

        // POST: /Consignataria/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,Sigla,Codigo,CNPJ,Email,TipoRepresentante")] ConsignatariaEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var consignataria = _servicoConsignataria.ObterPeloId(model.Id);

                    Mapper.Map(model, consignataria);

                    _servicoConsignataria.Atualizar(consignataria);
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

        // GET: /Consignataria/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var consignataria = _servicoConsignataria.ObterPeloId(id.Value);

            if (consignataria == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ConsignatariaEditModel>(consignataria);

            return View(model);
        }

        // POST: /Consignataria/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExcluir(long id)
        {
            var consignataria = _servicoConsignataria.ObterPeloId(id);

            if (consignataria == null)
            {
                return HttpNotFound();
            }

            try
            {
                _servicoConsignataria.Excluir(consignataria);
                Success(Alerts.Success, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            var model = Mapper.Map<ConsignatariaEditModel>(consignataria);

            return View(model);
        }
    }
}