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
            var permissions = _context.Users.Include(u => u.Profile)
                .Include(u => u.Profile.Permissions)
                .First(u => u.Id == loggedUser.Id).Profile.Permissions;

            var nodeList = new List<DynamicNode>();
            foreach (var permission in permissions)
            {
                var dNode = new DynamicNode
                {
                    Key = permission.Id.ToString(),
                    ParentKey = permission.ParentId.ToString(),
                    Title = permission.Name,
                    Description = permission.Description,
                    Url = permission.Url,
                    Action = permission.Action,
                    Controller = permission.Controller,
                    Order = permission.Order
                };

                //Quando for CRUD
                if (permission.IsCrud)
                {
                    var cList = permission.Attributes.Split(';');
                    dNode.PreservedRouteParameters = cList;
                }

                dNode.Attributes.Add("icon", permission.IconClass);
                dNode.Attributes.Add("visibility", permission.ShowInMenu ? "" : "!MenuHelper");

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