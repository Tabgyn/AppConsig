﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppConsig.Comum.Seguranca;
using AppConsig.Dados;

namespace AppConsig.Web.Gestor.Filtros
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AppAuthorizedAttribute : AuthorizeAttribute
    {
        private readonly AppContexto _contexto = new AppContexto();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                // The user is not authenticated
                return false;
            }

            var usuarioLogado = httpContext.User as AppPrincipal;

            if (usuarioLogado == null)
            {
                return false;
            }

            if (usuarioLogado.Admin)
            {
                return true;
            }

            var action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            var controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            var permissoes =
                _contexto.Usuarios.Include(u => u.Perfil)
                    .Include(u => u.Perfil.Permissoes)
                    .First(u => u.Id == usuarioLogado.Id).Perfil.Permissoes;
            var autorizado = permissoes.Any(permissao => permissao.Controller == controller && permissao.Action == action);

            return autorizado;
        }
    }
}