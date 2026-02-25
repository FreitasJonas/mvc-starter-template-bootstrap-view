using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Domain.Entities;
using mvc.starter.template.Shared.Auditing;
using mvc.starter.template.Shared.Logging;
using mvc.starter.template.Web.Filters;
using mvc.starter.template.Web.Helpers;
using mvc.starter.template.Web.Security;
using mvc.starter.template.Web.ViewModels;
using static mvc.starter.template.Shared.Security.PermissionMap;

namespace mvc.starter.template.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private const string EntityName = "User";

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ILogFileWriter _logger;
        private readonly IAuditService _auditService;

        public UsersController(
            IUserService userService,
            IRoleService roleService,
            ILogFileWriter logger,
            IAuditService auditService)
        {
            _userService = userService;
            _roleService = roleService;
            _logger = logger;
            _auditService = auditService;
        }

        [PermissionAuthorize(Permission.UsersView)]
        public IActionResult Index()
        {
            try
            {
                SetIndexBreadcrumb();
                return View(_userService.GetAllWithRole());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error loading user list.", ex);
                throw;
            }
        }

        public IActionResult Form(int? id)
        {
            try
            {
                if (!id.HasValue || id == 0)
                {
                    if (!User.HasPermission(Permission.UserCreate))
                    {
                        TempData["Error"] = "Permission denied!";
                        return RedirectToAction("Index", "Home");
                    }                        

                    SetFormBreadcrumb();
                    return View(BuildFormViewModel());
                }

                if (!User.HasPermission(Permission.UserEdit))
                {
                    TempData["Error"] = "Permission denied!";
                    return RedirectToAction("Index", "Home");
                }

                var user = _userService.GetById(id.Value);
                if (user == null)
                    return NotFound();

                SetFormBreadcrumb();
                return View(BuildFormViewModel(user, 1));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading user form. Id: {id}", ex);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult Form(UsuarioFormViewModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    if (!User.HasPermission(Permission.UserCreate))
                    {
                        TempData["Error"] = "Permission denied!";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    if (!User.HasPermission(Permission.UserEdit))
                    {
                        TempData["Error"] = "Permission denied!";
                        return RedirectToAction("Index", "Home");
                    }
                }

                if (!ModelState.IsValid)
                {
                    model.Roles = _roleService.GetAll();
                    TempData["Error"] = "Please fill in the fields correctly!";
                    SetFormBreadcrumb();
                    return View(model);
                }
               
                if (model.Id == 0)
                {
                    TempData["Success"] = "User successfully registered.";
                }
                else
                {
                    TempData["Success"] = "User updated successfully.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Error saving user. Id: {model.Id}, Login: {model.Login}", ex);

                TempData["Error"] = "Error processing the operation.";
                model.Roles = _roleService.GetAll();
                SetFormBreadcrumb();
                return View(model);
            }
        }

        [PermissionAuthorize(Permission.UserDelete)]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!User.HasPermission(Permission.UserDelete))
                {
                    TempData["Error"] = "Permission denied!";
                    return RedirectToAction("Index", "Home");
                }

                var user = _userService.GetById(id);

                if (user == null)
                    return NotFound();

                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading user deletion. Id: {id}", ex);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(Permission.UserDelete)]
        [Audit(EntityName, AuditAction.Delete)]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (!User.HasPermission(Permission.UserDelete))
                {
                    TempData["Error"] = "Permission denied!";
                    return RedirectToAction("Index", "Home");
                }

                var user = _userService.GetById(id);
                if (user == null)
                    return NotFound();

                _userService.Delete(id);
                TempData["Success"] = "User successfully deleted.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user. Id: {id}", ex);
                TempData["Error"] = "Error deleting user.";
                return RedirectToAction(nameof(Index));
            }
        }

        private UsuarioFormViewModel BuildFormViewModel(
            User? user = null,
            int? roleId = null)
        {
            return new UsuarioFormViewModel
            {
                Id = 1,
                Name = "admin",
                LastName = "admin",
                Login = "admin",
                Email = "admin@admin",
                Telephone = "5599999999",
                DtBirth = "01/01/2026",
                Status = true,

                RoleId = roleId ?? 0,
                Roles = new List<Role>()
            };
        }

        private void SetIndexBreadcrumb()
        {
            ViewBag.Subheader = new IndexSubheaderViewModel
            {
                Title = "Users",
                Icon = "bi bi-people",

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
                        Text = "Users",
                        IsActive = true
                    }
                },

                ActionText = "New",
                ActionUrl = Url.Action("Form", "Users")
            };
        }

        private void SetFormBreadcrumb()
        {
            ViewBag.Subheader = new IndexSubheaderViewModel
            {
                Title = "Usuer Registration",
                Icon = "bi bi-person-gear",

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
                        Text = "Users",
                        Controller = "Users",
                        Action = "Index"
                    },
                    new BreadcrumbItemViewModel
                    {
                        Text = "User Registration",
                        IsActive = true
                    }
                }
            };
        }
    }
}
