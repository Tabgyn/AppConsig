using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using AppConsig.Dados;
using AppConsig.Web.Gestor.Seguranca;
using MvcSiteMapProvider;

namespace AppConsig.Web.Gestor
{
    public class AppDynamicNodeProvider : IDynamicNodeProvider
    {
        private readonly AppContexto _contexto = new AppContexto();

        public IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var usuarioLogado = ((AppPrincipal)HttpContext.Current.User);
            var permissoes =
                _contexto.Usuarios.Include(u => u.Perfil)
                    .Include(u => u.Perfil.Permissoes)
                    .First(u => u.Id == usuarioLogado.Id).Perfil.Permissoes;

            var nodeList = new List<DynamicNode>();
            foreach (var permissao in permissoes)
            {
                var dNode = new DynamicNode
                {
                    Key = permissao.Id.ToString(CultureInfo.InvariantCulture),
                    ParentKey = permissao.ParenteId.ToString(CultureInfo.InvariantCulture),
                    Title = permissao.Nome,
                    Description = permissao.Descricao,
                    Url = permissao.Url,
                    Action = permissao.Action,
                    Controller = permissao.Controller,
                    Order = permissao.Ordem
                };

                //Quando for CRUD
                if (permissao.IsCrud)
                {
                    var cList = permissao.Atributos.Split(';');
                    dNode.PreservedRouteParameters = cList;
                }

                dNode.Attributes.Add("icone", permissao.Icone);
                dNode.Attributes.Add("visibility", permissao.Visivel ? "" : "!MenuHelper");

                nodeList.Add(dNode);
            }

            return nodeList;
        }

        public bool AppliesTo(string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
                return false;

            return GetType() == Type.GetType(providerName, false);
        }
    }
}