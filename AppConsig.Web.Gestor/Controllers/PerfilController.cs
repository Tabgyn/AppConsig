using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppConsig.Entities;
using AppConsig.Services.Interfaces;
using AppConsig.Web.Base.Entities;
using AppConsig.Web.Gestor.Filtros;
using AppConsig.Web.Gestor.Resources;
using PagedList;

namespace AppConsig.Web.Gestor.Controllers
{
    public class PerfilController : BaseController
    {
        readonly IProfileService _profileService;
        readonly IPermissionService _permissionService;

        public PerfilController(IProfileService profileService, IPermissionService permissionService)
        {
            _profileService = profileService;
            _permissionService = permissionService;
        }

        // GET: /Perfil
        [HttpGet]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? itemsPerPage)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "" : "name_desc";
            ViewBag.OwnerSortParam = sortOrder == "own" ? "own_desc" : "own";
            ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var perfis = _profileService.GetAll(a => a.Deleted == false && a.IsEditable).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                perfis = perfis.Where(a => a.CreateBy.Contains(searchString)
                || a.Name.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    perfis = perfis.OrderByDescending(a => a.Name).ToList();
                    break;
                case "own":
                    perfis = perfis.OrderBy(a => a.CreateBy).ToList();
                    break;
                case "own_desc":
                    perfis = perfis.OrderByDescending(a => a.CreateBy).ToList();
                    break;
                case "date":
                    perfis = perfis.OrderBy(a => a.CreateDate).ToList();
                    break;
                case "date_desc":
                    perfis = perfis.OrderByDescending(a => a.CreateDate).ToList();
                    break;
                default:
                    perfis = perfis.OrderBy(a => a.Name).ToList();
                    break;
            }

            var pageSize = itemsPerPage ?? 5;
            var pageNumber = (page ?? 1);

            return View(perfis.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Perfil/Detalhar/5
        [HttpGet]
        public ActionResult Detalhar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profile = _profileService.GetById(id.Value);

            if (profile == null)
            {
                return HttpNotFound();
            }

            var permissions = _permissionService.GetAll(p => p.IsStandard == false);
            var selectedPermissions = _permissionService.GetProfilePermissions(profile.Id);
            ViewBag.Permissoes = GetTreeData(permissions, selectedPermissions);

            return View(profile);
        }

        // GET: /Perfil/Criar
        [HttpGet]
        public ActionResult Criar()
        {
            var permissoes = _permissionService.GetAll(p => p.IsStandard == false);
            ViewBag.Permissoes = GetTreeData(permissoes);

            return View();
        }

        // POST: /Perfil/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Criar([Bind(Include = "Nome, Descricao")] Profile profile, long[] ckbPermissionsLongs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var defaultList = _permissionService.GetAll(p => p.IsStandard).ToList();

                    foreach (var d in defaultList)
                    {
                        profile.Permissions.Add(d);
                    }

                    var ckbSelected =
                        ckbPermissionsLongs.Select(ckb => _permissionService.GetById(ckb)).ToList();

                    foreach (var p in ckbSelected)
                    {
                        profile.Permissions.Add(p);
                    }

                    profile.IsEditable = true;

                    _profileService.Insert(profile);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            var permissions = _permissionService.GetAll(p => p.IsStandard == false);
            ViewBag.Permissoes = GetTreeData(permissions);

            return View(profile);
        }

        // GET: /Perfil/Editar/5
        [HttpGet]
        public ActionResult Editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var perfil = _profileService.GetById(id.Value);

            if (perfil == null)
            {
                return HttpNotFound();
            }

            if (!perfil.IsEditable)
            {
                return RedirectToAction("Index");
            }

            var permissions = _permissionService.GetAll(p => p.IsStandard == false);
            var selectedPermissions = _permissionService.GetProfilePermissions(perfil.Id);
            ViewBag.Permissoes = GetTreeData(permissions, selectedPermissions);

            return View(perfil);
        }

        // POST: /Perfil/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult Editar([Bind(Include = "Id, Name, Description")] Profile profile, long[] ckbPermissions)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ckbSelecionadas =
                        ckbPermissions.Select(ckb => _permissionService.GetById(ckb)).ToList();

                    profile.Permissions = ckbSelecionadas;

                    var oldPerfil = _profileService.GetProfileWithPermissions(profile.Id);

                    if (!oldPerfil.IsEditable)
                    {
                        throw new Exception(Exceptions.ActionNotAllowed);
                    }

                    oldPerfil.Name = profile.Name;
                    oldPerfil.Description = profile.Description;
                    oldPerfil.Permissions = oldPerfil.Permissions.Where(p => ckbSelecionadas.Contains(p)).ToList();

                    _profileService.Update(oldPerfil);
                    Success(Alerts.Sucess, true);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    Erro(Alerts.Erro, true, exception);
                }
            }

            var permissions = _permissionService.GetAll(p => p.IsStandard == false);
            var selectedPermissions = _permissionService.GetProfilePermissions(profile.Id);
            ViewBag.Permissoes = GetTreeData(permissions, selectedPermissions);

            return View(profile);
        }

        // GET: /Perfil/Excluir/5
        [HttpGet]
        public ActionResult Excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var perfil = _profileService.GetById(id.Value);

            if (perfil == null)
            {
                return HttpNotFound();
            }

            if (!perfil.IsEditable)
            {
                return RedirectToAction("Index");
            }

            var permissoes = _permissionService.GetAll(p => p.IsStandard == false);
            var permissoesSelecionadas = _permissionService.GetProfilePermissions(perfil.Id);
            ViewBag.Permissoes = GetTreeData(permissoes, permissoesSelecionadas);

            return View(perfil);
        }

        // POST: /Perfil/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        [Audit]
        public ActionResult ConfirmarExcluir(long id)
        {
            var perfil = _profileService.GetById(id);

            if (perfil == null)
            {
                return HttpNotFound();
            }

            if (!perfil.IsEditable)
            {
                throw new Exception(Exceptions.ActionNotAllowed);
            }

            try
            {
                _profileService.Delete(perfil);
                Success(Alerts.Sucess, true);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                Erro(Alerts.Erro, true, exception);
            }

            return View(perfil);
        }

        private static List<TreeViewNode> GetTreeData(IEnumerable<Permission> permissions, IEnumerable<Permission> selectedPermissions = null, long parentNodeId = 0)
        {
            var list = permissions.ToList();

            IList<Permission> userList = selectedPermissions?.ToList() ?? new List<Permission>();

            var pList = list.Where(p => p.ParentId == parentNodeId) as IList<Permission> ??
                             list.Where(p => p.ParentId == parentNodeId).ToList();

            return pList.Select(item => new TreeViewNode
            {
                Id = item.Id,
                Text = item.Name,
                ParentId = item.ParentId,
                Selected = userList.Any(p => p.Id == item.Id),
                Children = GetTreeData(list, userList, item.Id)
            }).ToList();
        }
    }
}