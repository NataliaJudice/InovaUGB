using InnovaCore.Domain.Entities;
using InnovaCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnovaCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SetorController : Controller
    {
        private readonly ISetorService _setorService;

        public SetorController(ISetorService setorService)
        {
            _setorService = setorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listaSetores = await _setorService.ObterSetoresAtivos();
            return View(listaSetores);
        }

        [HttpGet]
        public async Task<IActionResult> CriarSetor()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CriarSetor(Setor setor)
        {
            ModelState.Remove("DataCadastro");
            ModelState.Remove("Tarefas"); // Se existir uma lista de tarefas na Entidade
            ModelState.Remove("Solicitacoes"); // Se houver relacionamento

            if (ModelState.IsValid)
            {
                await _setorService.CriarSetor(setor);
                return RedirectToAction("Index");
            }

        
            return View(setor);
        }

        [HttpPost]
        public async Task<IActionResult> EditarSetor(int id, Setor setor)
        {
            if (ModelState.IsValid)
            {
                await _setorService.EditarSetor(id, setor);
                return RedirectToAction("Index"); // Isso retorna um Status 200 para o Fetch
            }

            // Se chegar aqui, a validação falhou
            return BadRequest("Dados inválidos");
        }



    }
}
