using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AppConsig.Common.Security;
using AppConsig.Web.Base.Entities;

namespace AppConsig.Web.Gestor.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new AppPrincipal User => HttpContext.User as AppPrincipal;

        public void Success(string message, bool dismissable = false, Exception exception = null)
        {
            AddAlert(AlertMessageStyle.Success, "check", "Sucesso!", message, dismissable, exception);
        }

        public void Info(string message, bool dismissable = false, Exception exception = null)
        {
            AddAlert(AlertMessageStyle.Info, "info", "Info!", message, dismissable, exception);
        }

        public void Warning(string message, bool dismissable = false, Exception exception = null)
        {
            AddAlert(AlertMessageStyle.Warning, "warning", "Atenção!", message, dismissable, exception);
        }

        public void Erro(string message, bool dismissable = false, Exception exception = null)
        {
            AddAlert(AlertMessageStyle.Erro, "times", "Erro!", message, dismissable, exception);
        }

        private void AddAlert(string alertMessageStyle, string iconClass, string title, string text, bool disposable, Exception exception = null)
        {
            var alerts = TempData.ContainsKey(AlertMessage.TempDataKey)
                ? (List<AlertMessage>)TempData[AlertMessage.TempDataKey]
                : new List<AlertMessage>();

            alerts.Add(new AlertMessage
            {
                AlertMessageStyle = alertMessageStyle,
                IconClass = iconClass,
                Title = title,
                Text = text,
                Disposable = disposable,
                Exception = exception?.Message ?? ""
            });

            TempData[AlertMessage.TempDataKey] = alerts;
        }
    }
}