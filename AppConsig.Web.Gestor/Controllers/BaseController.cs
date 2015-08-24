using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AppConsig.Web.Base.Entidades;

namespace AppConsig.Web.Gestor.Controllers
{
    public class BaseController : Controller
    {
        public void Successo(string message, bool dismissable = false, Exception exception = null)
        {
            AddAlerta(EstiloAlerta.Successo, "check", "Sucesso!", message, dismissable, exception);
        }

        public void Informacao(string message, bool dismissable = false, Exception exception = null)
        {
            AddAlerta(EstiloAlerta.Informacao, "info", "Info!", message, dismissable, exception);
        }

        public void Atencao(string message, bool dismissable = false, Exception exception = null)
        {
            AddAlerta(EstiloAlerta.Atencao, "warning", "Atenção!", message, dismissable, exception);
        }

        public void Erro(string message, bool dismissable = false, Exception exception = null)
        {
            AddAlerta(EstiloAlerta.Erro, "times", "Erro!", message, dismissable, exception);
        }

        private void AddAlerta(string estiloAlerta, string estiloIcone, string titulo, string texto, bool descartavel, Exception exception = null)
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
                Descartavel = descartavel,
                Exception = exception?.Message ?? ""
            });
            
            TempData[Alerta.TempDataKey] = alertas;
        }
    }
}