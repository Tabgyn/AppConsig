using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AppConsig.Web.Gestor.Models;

namespace AppConsig.Web.Gestor.Controllers
{
    public class ArquivoController : BaseController
    {
        // GET: Arquivo/Folha
        public ActionResult Folha()
        {
            return View();
        }

        // GET: Arquivo/Lote
        public ActionResult Lote()
        {
            return View();
        }

        // GET: Arquivo/Movimento
        public ActionResult Movimento()
        {
            return View();
        }

        // GET: Arquivo/Retorno
        public ActionResult Retorno()
        {
            return View();
        }
    }
}