using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Resources;
using AutoMapper;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class ConsignacaoController : BaseController
    {
        readonly IServicoConsignacao _servicoConsignacao;

        public ConsignacaoController(IServicoConsignacao servicoConsignacao)
        {
            _servicoConsignacao = servicoConsignacao;
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

            var consignacoes = _servicoConsignacao.ObterTodos(a => a.Excluido == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                DateTime dateString;
                if (DateTime.TryParse(searchString, out dateString))
                {
                    consignacoes = consignacoes.Where(a => a.CriadoEm.Date <= dateString.Date).ToList();
                }
                else
                {
                    consignacoes = consignacoes.Where(a => a.CriadoPor.Contains(searchString)
                    || a.Nome.Contains(searchString)).ToList();
                }
            }

            switch (sortOrder)
            {
                case "own":
                    consignacoes = consignacoes.OrderBy(a => a.CriadoPor).ToList();
                    break;
                case "own_desc":
                    consignacoes = consignacoes.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    consignacoes = consignacoes.OrderBy(a => a.CriadoEm).ToList();
                    break;
                case "date_desc":
                    consignacoes = consignacoes.OrderByDescending(a => a.CriadoEm).ToList();
                    break;
                case "name_desc":
                    consignacoes = consignacoes.OrderByDescending(a => a.Nome).ToList();
                    break;
                default:
                    consignacoes = consignacoes.OrderBy(a => a.Nome).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            var model = Mapper.Map<IList<ConsignacaoListModel>>(consignacoes);

            ViewBag.TotalRegisters = model.Count;

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

            var consignacao = _servicoConsignacao.ObterPeloId(id.Value);

            if (consignacao == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ConsignacaoEditModel>(consignacao);

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
        public ActionResult Criar(
            [Bind(Include ="Nome,Descricao,Codigo,MaximoParcela,ValorMinimo,InicioDaVigenciaEm," +
                           "FimDaVigenciaEm,PermiteDescontoParcial,PermiteLancamentoManual," +
                           "PermiteOutrasOcorrencias,ConsignatariaId,ServicoId")] ConsignacaoEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var consignacao = Mapper.Map<Consignacao>(model);

                    _servicoConsignacao.Criar(consignacao);
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

            var consignataria = _servicoConsignacao.ObterPeloId(id.Value);

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
        public ActionResult Editar([Bind(Include = "Id,Nome,Descricao,Codigo,MaximoParcela,ValorMinimo,InicioDaVigenciaEm," +
                           "FimDaVigenciaEm,PermiteDescontoParcial,PermiteLancamentoManual," +
                           "PermiteOutrasOcorrencias,ConsignatariaId,ServicoId")] ConsignatariaEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var consignataria = _servicoConsignacao.ObterPeloId(model.Id);

                    Mapper.Map(model, consignataria);

                    _servicoConsignacao.Atualizar(consignataria);
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

            var consignataria = _servicoConsignacao.ObterPeloId(id.Value);

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
            var consignataria = _servicoConsignacao.ObterPeloId(id);

            if (consignataria == null)
            {
                return HttpNotFound();
            }

            try
            {
                _servicoConsignacao.Excluir(consignataria);
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