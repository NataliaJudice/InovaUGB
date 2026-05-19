using InnovaCore.Services.Interfaces;
using InnovaCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InnovaCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
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
        [Authorize]
        public IActionResult Hub()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HubAdmin()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Comando()
        {
            try
            {
                var qtdes = await _dashboardService.GetQtdes();
                var qtdeporSetor = await _dashboardService.GetQtdesPorSetor();

                var vm = new ViewModelDashboard
                {
                    VwDashboardQtde = qtdes,
                    VwQtdePorSetor = qtdeporSetor
                };

                return View(vm);
            }
            catch (Exception)
            {

                var vmVazia = new ViewModelDashboard();

                TempData["ErrorMessage"] = "Não foi possível carregar os dados do dashboard no momento.";

                return View(vmVazia);
            }
        }
    }
}