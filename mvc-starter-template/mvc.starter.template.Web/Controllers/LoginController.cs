using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using mvc.starter.template.Application.Interfaces;
using mvc.starter.template.Web.ViewModels;
using mvc.starter.template.Shared.Security;
using System.Diagnostics;
using mvc.starter.template.Shared.Auditing;
using mvc.starter.template.Shared.Logging;

namespace mvc.starter.template.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        private readonly TwoFactorService _twoFactorService;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserService _userService;
        private readonly IAuditService _auditService;
        private readonly ILogFileWriter _logger;

        public LoginController(
            IAuthService authService,
            TwoFactorService twoFactorService,
            IPermissionRepository permissionRepository,
            IUserService userService,
            IAuditService auditService,
            ILogFileWriter logger)
        {
            _authService = authService;
            _twoFactorService = twoFactorService;
            _permissionRepository = permissionRepository;
            _userService = userService;
            _auditService = auditService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInfo("System access detected!");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }   

            var user = _authService.Authenticate(model.Login, model.Password);

            if (user == null)
            {
                // LOGIN FALHOU
                SafeAudit(() =>
                    _auditService.LogEvent(
                        "User",
                        model.Login,
                        AuditAction.LoginFailed,
                        new { Reason = "Credenciais inválidas" }
                    )
                );

                ModelState.AddModelError(string.Empty, "Invalid login or password.");
                return View(model);
            }

            var code = _twoFactorService.GenerateCode(1);

            TempData["UserId"] = 1;
            TempData["DemoCode"] = code; //remove in production
            return RedirectToAction(nameof(TwoFactor));
        }

        [HttpGet]
        public IActionResult TwoFactor()
        {
            if (TempData["UserId"] == null)
                return RedirectToAction(nameof(Index));

            return View(new TwoFactorViewModel
            {
                UserId = (int)TempData["UserId"]
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TwoFactor(TwoFactorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var valid = _twoFactorService.ValidateCode(model.UserId, model.Code);

            if (!valid)
            {                
                SafeAudit(() =>
                    _auditService.LogEvent(
                        "User",
                        model.UserId,
                        "LOGIN_2FA_FAILED",
                        new { Reason = "Código inválido ou expirado" }
                    )
                );

                ModelState.AddModelError(string.Empty, "Código inválido ou expirado.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim("UserId", model.UserId.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });

            SafeAudit(() =>
                _auditService.LogEvent(
                    "User",
                    1,
                    AuditAction.LoginSuccess
                )
            );

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst("UserId")?.Value;

            SafeAudit(() =>
                _auditService.LogEvent(
                    "User",
                    userId ?? "N/A",
                    AuditAction.Logout
                )
            );

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        private void SafeAudit(Action action)
        {
            try
            {
                action();
            }
            catch
            {

            }
        }
    }
}
