using InnovaCore.Domain.ViewModels;
using InnovaCore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InnovaCore.Controllers
{
    public class TarefaController : Controller
    {
        private readonly ITarefaService _tarefaService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailServices _emailService;
        public TarefaController(ITarefaService tarefaService, UserManager<IdentityUser> userManager, IEmailServices emailServices)
        {
            _tarefaService = tarefaService;
            _userManager = userManager;
            _emailService = emailServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TodasAsTarefas(int id)
        {
            var tarefas = await _tarefaService.GetAll();
            return View(tarefas);
        }

        [HttpPost]

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> MudarStatus([FromBody] ViewModelTarefaEStatus model)
        {

            if (model == null) return BadRequest();

            await _tarefaService.MudarStatus(model.novoStatus, model.idTarefa);


            return Ok();
        }

    }
}
