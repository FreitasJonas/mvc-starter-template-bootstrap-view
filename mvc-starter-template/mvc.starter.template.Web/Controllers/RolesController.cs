using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Domain.Entities;
using mvc.starter.template.Shared.Logging;
using mvc.starter.template.Web.Security;
using mvc.starter.template.Web.ViewModels;
using static mvc.starter.template.Shared.Security.PermissionMap;

namespace mvc.starter.template.Web.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ILogFileWriter _logger;

        public RolesController(IRoleService roleService, ILogFileWriter logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [PermissionAuthorize(Permission.RolesView)]
        public IActionResult Index()
        {
            try
            {
                SetIndexBreadcrumb();
                var perfis = _roleService.GetAll();
                return View(perfis);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error loading roles list.", ex);
                throw;
            }
        }

        [PermissionAuthorize(Permission.RoleCreate)]
        public IActionResult Create()
        {
            try
            {
                var vm = new RoleFormViewModel
                {
                    PermissionTree = BuildPermissionTree()
                };

                SetFormBreadcrumb(true);
                return View("Form", vm);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error loading profile creation form.", ex);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(Permission.RoleCreate)]
        public IActionResult Create(RoleFormViewModel model, List<string> SelectedPermissions)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    SetFormBreadcrumb(true);
                    model.PermissionTree = BuildPermissionTree();

                    TempData["Error"] = "Please fill in all fields correctly.";
                    return View("Form", model);
                }

                var normalizedPermissions = NormalizePermissions(SelectedPermissions);

                TempData["Success"] = "Role saved successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Error creating role. Name: {model.Name}", ex);

                ModelState.AddModelError(string.Empty, "Error creating role.");
                model.PermissionTree = BuildPermissionTree();

                TempData["Error"] = "An error occurred while saving the role.";
                SetFormBreadcrumb(true);
                return View("Form", model);
            }
        }

        [PermissionAuthorize(Permission.RoleEdit)]
        public IActionResult Edit(int id)
        {
            try
            {
                var role = _roleService.GetById(id);
                if (role == null)
                    return NotFound();

                var vm = new RoleFormViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    PermissionTree = BuildPermissionTree()
                };

                SetFormBreadcrumb(false);
                return View("Form", vm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading role edit. Id: {id}", ex);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(Permission.RoleEdit)]
        public IActionResult Edit(RoleFormViewModel model, List<string> SelectedPermissions)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.PermissionTree = BuildPermissionTree(
                        _roleService.GetPermissionCodesByRoleId(model.Id)
                    );

                    TempData["Error"] = "Please fill in all fields correctly.";
                    SetFormBreadcrumb(false);
                    return View("Form", model);
                }

                TempData["Success"] = "Role updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Error updating profile. Id: {model.Id}, Name: {model.Name}", ex);

                ModelState.AddModelError(string.Empty, "Error updating profile.");
                model.PermissionTree = BuildPermissionTree(
                    _roleService.GetPermissionCodesByRoleId(model.Id)
                );

                TempData["Error"] = "Error updating profile.";
                SetFormBreadcrumb(false);
                return View("Form", model);
            }
        }

        private List<PermissionTreeNodeViewModel> BuildPermissionTree(IEnumerable<string>? selectedPermissions = null)
        {
            selectedPermissions ??= Enumerable.Empty<string>();

            var permissions = new List<PermissionEntity>
    {
        new PermissionEntity { Id = 1,  Code = "system", Description = "", IsActive = true },

        new PermissionEntity { Id = 2,  Code = "system||users", Description = "", IsActive = true },
        new PermissionEntity { Id = 3,  Code = "system||users||view", Description = "View users", IsActive = true },
        new PermissionEntity { Id = 4,  Code = "system||users||create", Description = "Create users", IsActive = true },
        new PermissionEntity { Id = 5,  Code = "system||users||edit", Description = "Edit user", IsActive = true },
        new PermissionEntity { Id = 6,  Code = "system||users||delete", Description = "Delete user", IsActive = true },

        new PermissionEntity { Id = 7,  Code = "system||roles", Description = "", IsActive = true },
        new PermissionEntity { Id = 8,  Code = "system||roles||view", Description = "View roles", IsActive = true },
        new PermissionEntity { Id = 9,  Code = "system||roles||create", Description = "Create role", IsActive = true },
        new PermissionEntity { Id = 10, Code = "system||roles||edit", Description = "Edit role", IsActive = true },

        new PermissionEntity { Id = 11, Code = "system||audit", Description = "", IsActive = true },
        new PermissionEntity { Id = 12, Code = "system||audit||view", Description = "View audit details", IsActive = true },

        new PermissionEntity { Id = 13, Code = "system||log", Description = "Log system", IsActive = true },
        new PermissionEntity { Id = 14, Code = "system||log||view", Description = "View log system", IsActive = true },
        new PermissionEntity { Id = 15, Code = "system||log||download", Description = "Download log system", IsActive = true },
    };

            var root = new List<PermissionTreeNodeViewModel>();

            foreach (var permission in permissions.Where(p => p.IsActive))
            {
                var parts = permission.Code.Split("||");
                var currentLevel = root;
                var currentPath = "";

                for (int i = 0; i < parts.Length; i++)
                {
                    currentPath = string.IsNullOrEmpty(currentPath)
                        ? parts[i]
                        : $"{currentPath}||{parts[i]}";

                    var node = currentLevel
                        .FirstOrDefault(n => n.Key == currentPath);

                    if (node == null)
                    {
                        node = new PermissionTreeNodeViewModel
                        {
                            Key = currentPath,
                            Label = parts[i],
                            Checked = false,
                            Description = permission.Description
                        };

                        currentLevel.Add(node);
                    }

                    if (selectedPermissions.Contains(currentPath))
                    {
                        node.Checked = true;
                    }

                    currentLevel = node.Children;
                }
            }

            return root;
        }

        private void SetIndexBreadcrumb()
        {
            ViewBag.Subheader = new IndexSubheaderViewModel
            {
                Title = "Access Roles",
                Icon = "bi bi-unlock",

                Breadcrumb = new[]
                {
                    new BreadcrumbItemViewModel
                    {
                        Text = "Home",
                        Controller = "Home",
                        Action = "Index"
                    },
                    new BreadcrumbItemViewModel
                    {
                        Text = "Roles",
                        IsActive = true
                    }
                },

                ActionText = "New",
                ActionUrl = Url.Action("Create", "Roles")
            };
        }

        private void SetFormBreadcrumb(bool isCadastro)
        {
            ViewBag.Subheader = new IndexSubheaderViewModel
            {
                Title = isCadastro
                    ? "Role Creating"
                    : "Role Editing",

                Icon = "bi bi-unlock",

                Breadcrumb = new[]
                {
                    new BreadcrumbItemViewModel
                    {
                        Text = "Home",
                        Controller = "Home",
                        Action = "Index"
                    },
                    new BreadcrumbItemViewModel
                    {
                        Text = "Roles",
                        Controller = "Roles",
                        Action = "Index"
                    },
                    new BreadcrumbItemViewModel
                    {
                        Text = isCadastro
                            ? "Role Creating"
                            : "Role Editing",
                        IsActive = true
                    }
                }
            };
        }

        private List<string> NormalizePermissions(List<string>? selectedPermissions)
        {
            selectedPermissions ??= new List<string>();

            if (!selectedPermissions.Contains("system"))
                selectedPermissions.Add("system");

            var all = new HashSet<string>(selectedPermissions);

            foreach (var permission in selectedPermissions.ToList())
            {
                var parts = permission.Split("||");

                for (int i = 1; i < parts.Length; i++)
                {
                    var parent = string.Join("||", parts.Take(i));
                    all.Add(parent);
                }
            }

            return all.ToList();
        }
    }
}
