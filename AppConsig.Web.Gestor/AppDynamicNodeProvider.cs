using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AppConsig.Common.Security;
using AppConsig.Data;
using MvcSiteMapProvider;

namespace AppConsig.Web.Gestor
{
    public class AppDynamicNodeProvider : IDynamicNodeProvider
    {
        private readonly AppContext _context = new AppContext();

        public IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var usuarioLogado = ((AppPrincipal)HttpContext.Current.User);
            var permissoes = _context.Usuarios.Include(u => u.Perfil)
                .Include(u => u.Perfil.Permissoes)
                .First(u => u.Id == usuarioLogado.Id).Perfil.Permissoes;

            var nodeList = new List<DynamicNode>();
            foreach (var permissao in permissoes)
            {
                var dNode = new DynamicNode
                {
                    Key = permissao.Id.ToString(),
                    ParentKey = permissao.ParenteId.ToString(),
                    Title = permissao.Nome,
                    Description = permissao.Descricao,
                    Url = permissao.Url,
                    Action = permissao.Acao,
                    Controller = permissao.Controle,
                    Order = permissao.Ordem
                };

                //Quando for CRUD
                if (permissao.EhCRUD)
                {
                    var cList = permissao.Atributos.Split(';');
                    dNode.PreservedRouteParameters = cList;
                }

                dNode.Attributes.Add("icon", permissao.ClasseIcone);
                dNode.Attributes.Add("visibility", permissao.MostrarNoMenu ? string.Empty : "!MenuHelper");

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