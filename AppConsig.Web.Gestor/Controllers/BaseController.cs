using System.Collections.Generic;
using System.Web.Mvc;
using AppConsig.Web.Base.Entidades;

namespace AppConsig.Web.Gestor.Controllers
{
    public class BaseController : Controller
    {
        public void Successo(string message, bool dismissable = false)
        {
            AddAlerta(EstiloAlerta.Successo, "check", "Sucesso!", message, dismissable);
        }

        public void Informacao(string message, bool dismissable = false)
        {
            AddAlerta(EstiloAlerta.Informacao, "info", "Info!", message, dismissable);
        }

        public void Atencao(string message, bool dismissable = false)
        {
            AddAlerta(EstiloAlerta.Atencao, "warning", "Atencao!", message, dismissable);
        }

        public void Erro(string message, bool dismissable = false)
        {
            AddAlerta(EstiloAlerta.Erro, "times", "Erro!", message, dismissable);
        }

        private void AddAlerta(string estiloAlerta, string estiloIcone, string titulo, string texto, bool descartavel)
        {
            var alertas = TempData.ContainsKey(Alerta.TempDataKey)
                ? (List<Alerta>)TempData[Alerta.TempDataKey]
                : new List<Alerta>();

            alertas.Add(new Alerta
            {
                EstiloAlerta = estiloAlerta,
                EstiloIcone = estiloIcone,
                Titulo = titulo,
                Texto = texto,
                Descartavel = descartavel
            });

            TempData[Alerta.TempDataKey] = alertas;
        }
    }
}