using InnovaCore.Domain.Entities;
using InnovaCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace InnovaCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(ViewModelRegistro rvm)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = rvm.UserName, Email = rvm.Email, NormalizedUserName = rvm.UserName };
                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Hub", "Home");
                }
            }

            return View(rvm);

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ViewModelLogin lvm)
        {

            if (ModelState.IsValid)
            {
                // 1. Procurar o usuário pelo email digitado
                var user = await _userManager.FindByEmailAsync(lvm.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(
                        user.UserName,
                        lvm.Password,
                        lvm.rememberme,
                        lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("HubAdmin", "Home");
                        }

                        return RedirectToAction("Hub", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
            }
            return View(lvm);
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
