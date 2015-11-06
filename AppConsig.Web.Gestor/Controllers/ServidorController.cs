using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Common;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Gestor.Models;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class ServidorController : Controller
    {
        readonly IServicoServidor _servicoServidor;

        public ServidorController(IServicoServidor servicoServidor)
        {
            _servicoServidor = servicoServidor;
        }

        // GET: /Servidor
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? itemsPerPage)
        {
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "date_desc" : sortOrder;
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

            var servidores = _servicoServidor.ObterTodos(a => a.Excluido == false).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                DateTime dateString;
                if (DateTime.TryParse(searchString, out dateString))
                {
                    servidores = servidores.Where(a => a.CriadoEm.Date <= dateString.Date).ToList();
                }
                else
                {
                    servidores = servidores.Where(a => a.Nome.ToLower().Contains(searchString.ToLower())
                    || a.CPF.Contains(searchString.Replace(".", "").Replace("-", ""))
                    || a.Matricula.Contains(searchString)).ToList();
                }
            }

            switch (sortOrder)
            {
                case "name":
                    servidores = servidores.OrderBy(a => a.Nome).ToList();
                    break;
                case "name_desc":
                    servidores = servidores.OrderByDescending(a => a.Nome).ToList();
                    break;
                case "own":
                    servidores = servidores.OrderBy(a => a.CriadoPor).ToList();
                    break;
                case "own_desc":
                    servidores = servidores.OrderByDescending(a => a.CriadoPor).ToList();
                    break;
                case "date":
                    servidores = servidores.OrderBy(a => a.CriadoEm).ToList();
                    break;
                default:
                    servidores = servidores.OrderByDescending(a => a.CriadoEm).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            var servidoresModel = servidores.Select(s => new ServidorListModel
            {
                Id = s.Id,
                Nome = s.Nome,
                CPF = StringHelper.MascaraCnpjCpf(s.CPF),
                Matricula = s.Matricula
            }).ToList();

            ViewBag.TotalRegisters = servidoresModel.Count;

            return View(servidoresModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Aviso/Servidor/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var servidor = _servicoServidor.ObterServidorComDepartamento(id.Value);

            if (servidor == null)
            {
                return HttpNotFound();
            }

            var servidorModel = new ServidorEditModel
            {
                Id = servidor.Id,
                Nome = servidor.Nome,
                CPF = StringHelper.MascaraCnpjCpf(servidor.CPF),
                Matricula = servidor.Matricula,
                AdmitidoEm = servidor.AdmitidoEm.ToString("dd/MM/yyyy"),
                AfastadoEm = servidor.AfastadoEm.ToString("dd/MM/yyyy"),
                Departamento = servidor.Departamento.Nome,
                Foto = servidor.Foto,
                NascidoEm = servidor.NascidoEm.ToString("dd/MM/yyyy")
            };

            return View(servidorModel);
        }
    }
}