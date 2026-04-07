using InnovaCore.Services.Interfaces;
using InnovaCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InnovaCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardService _dashboardService;
        public HomeController(ILogger<HomeController> logger, IDashboardService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Authorize] // Só entra aqui quem logou
        public IActionResult Hub()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Admin")]// Só entra aqui quem logou
        public IActionResult HubAdmin()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Admin")]// Só entra aqui quem logou
        public async Task<IActionResult> Comando()
        {
            var qtdes = await _dashboardService.GetQtdes();
            var qtdeporSetor = await _dashboardService.GetQtdesPorSetor();
            
            ViewModelDashboard vm = new ViewModelDashboard();
            vm.VwDashboardQtde = qtdes;
            vm.VwQtdePorSetor = qtdeporSetor;

            return View(vm);
        }


    }
}
