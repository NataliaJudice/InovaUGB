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
                var user = new IdentityUser { UserName = rvm.Email, Email = rvm.Email, NormalizedUserName = rvm.UserName };
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


                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, lvm.rememberme, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                    {
                        // Se for admin, vai para a tela de escolha (HubAdmin)
                        return RedirectToAction("HubAdmin", "Home");
                    }

                    // Se for aluno, vai direto para a tela de protocolos (Hub)
                    return RedirectToAction("Hub", "Home");
                }
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida. Verifique suas credenciais.");

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
