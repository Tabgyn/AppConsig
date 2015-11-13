using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Common.Enums;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Models;
using AppConsig.Web.Gestor.Resources;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class ServicoController : BaseController
    {
        readonly IServicoTipoServico _servicoTipoServico;

        public ServicoController(IServicoTipoServico servicoTipoServico)
        {
            _servicoTipoServico = servicoTipoServico;
        }

        // GET: Servico
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

            var servicos = _servicoTipoServico.ObterTodos(a => a.Excluido == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                servicos = servicos.Where(a => a.CriadoPor.Contains(searchString)
                || a.Nome.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "own":
                    servicos = servicos.OrderBy(a => a.CriadoPor).ToList();
                    break;
                case "own_desc":
                    servicos = servicos.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    servicos = servicos.OrderBy(a => a.CriadoEm).ToList();
                    break;
                default:
                    servicos = servicos.OrderByDescending(a => a.CriadoEm).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            var servicosModel = servicos.Select(aviso => new ServicoListModel
            {
                Id = aviso.Id,
                Nome = aviso.Nome,
                CriadoPor = aviso.CriadoPor,
                CriadoEm = aviso.CriadoEm.ToString("dd/MM/yyyy hh:mm:ss")
            }).ToList();

            ViewBag.TotalRegisters = servicosModel.Count;

            return View(servicosModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Aviso/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var servico = _servicoTipoServico.ObterPeloId(id.Value);

            if (servico == null)
            {
                return HttpNotFound();
            }

            var servicoModel = new ServicoEditModel
            {
                Id = servico.Id,
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                Ordem = servico.Ordem,

                //TODO: Ver como trabalhar com enumerador nos models
                TipoServicoRelacao = servico.TipoServicoRelacao,
                TipoServicoInerente = servico.TipoServicoInerente,
            };

            return View(servico);
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
        public ActionResult Criar([Bind(Include = "Nome,Descricao,TipoServicoRelacao,TipoServicoInerente,Ordem")] ServicoEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var servico = new Servico
                    {
                        Nome = model.Nome,
                        Descricao = model.Descricao,
                        Ordem = model.Ordem,
                        TipoServicoInerente = model.TipoServicoInerente,
                        TipoServicoRelacao = model.TipoServicoRelacao
                    };

                    _servicoTipoServico.Criar(servico);
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

            var servico = _servicoTipoServico.ObterPeloId(id.Value);

            if (servico == null)
            {
                return HttpNotFound();
            }

            return View(servico);
        }

        // POST: /Aviso/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,Descricao,TipoServicoRelacao,TipoServicoInerente,Ordem")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoTipoServico.Atualizar(servico);
                    Success(Alerts.Success, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            return View(servico);
        }

        // GET: /Aviso/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var servico = _servicoTipoServico.ObterPeloId(id.Value);

            if (servico == null)
            {
                return HttpNotFound();
            }

            return View(servico);
        }

        // POST: /Aviso/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExcluir(long id)
        {
            var servico = _servicoTipoServico.ObterPeloId(id);

            if (servico == null)
            {
                return HttpNotFound();
            }

            try
            {
                _servicoTipoServico.Excluir(servico);
                Success(Alerts.Success, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            return View(servico);
        }
    }
}