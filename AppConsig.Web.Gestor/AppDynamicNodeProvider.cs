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
            var loggedUser = ((AppPrincipal)HttpContext.Current.User);
            var permissions = _context.Usuarios.Include(u => u.Perfil)
                .Include(u => u.Perfil.Permissoes)
                .First(u => u.Id == loggedUser.Id).Perfil.Permissoes;

            var nodeList = new List<DynamicNode>();
            foreach (var permission in permissions)
            {
                var dNode = new DynamicNode
                {
                    Key = permission.Id.ToString(),
                    ParentKey = permission.ParenteId.ToString(),
                    Title = permission.Nome,
                    Description = permission.Descricao,
                    Url = permission.Url,
                    Action = permission.Acao,
                    Controller = permission.Controle,
                    Order = permission.Ordem
                };

                //Quando for CRUD
                if (permission.EhCRUD)
                {
                    var cList = permission.Atributos.Split(';');
                    dNode.PreservedRouteParameters = cList;
                }

                dNode.Attributes.Add("icon", permission.ClasseIcone);
                dNode.Attributes.Add("visibility", permission.MostrarNoMenu ? "" : "!MenuHelper");

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