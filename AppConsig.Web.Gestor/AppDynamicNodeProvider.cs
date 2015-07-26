using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using AppConsig.Web.Gestor.Seguranca;
using MvcSiteMapProvider;

namespace AppConsig.Web.Gestor
{
    public class AppDynamicNodeProvider : IDynamicNodeProvider
    {
        public IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var permissoes = ((AppPrincipal)HttpContext.Current.User).Permissoes;

            return permissoes.Select(permissao => new DynamicNode
            {
                Key = permissao.Id.ToString(CultureInfo.InvariantCulture),
                ParentKey = permissao.Parente.ToString(CultureInfo.InvariantCulture),
                Title = permissao.Nome,
                Description = permissao.Descricao,
                Url = permissao.Url,
                Action = permissao.Action,
                Controller = permissao.Controller,
                ImageUrl = permissao.UrlImagem,
                Order = permissao.Ordem
            });
        }

        public bool AppliesTo(string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
                return false;

            return GetType() == Type.GetType(providerName, false);
        }
    }
}