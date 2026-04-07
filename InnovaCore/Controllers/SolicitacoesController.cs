using InnovaCore.Domain.Entities;
using InnovaCore.Domain.ViewModels;
using InnovaCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InnovaCore.Controllers
{
    public class SolicitacoesController : Controller
    {
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly ISetorService _setorService;
        public SolicitacoesController(ISolicitacaoService solicitacaoService, ISetorService setorService)
        {
            _solicitacaoService = solicitacaoService;
            _setorService = setorService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Setor> TotalSetores = await _setorService.ObterSetoresAtivos();
            return View(TotalSetores);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Solicitacao solicitacao)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _solicitacaoService.CriarProposta(solicitacao, userId, email);
            return RedirectToAction("ListarSolicitacoesUsuario", "Solicitacoes");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListarPendentes()
        {
            IEnumerable<Solicitacao> solicitacoesPendentes = await _solicitacaoService.ListarPendentes();
            return View(solicitacoesPendentes);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Aprovar(int id)
        {
            await _solicitacaoService.AprovarSolicitacao(id);
            return RedirectToAction("TodasAsTarefas", "Tarefa");

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Rejeitar(ViewModelRejeicao vm)
        {
            await _solicitacaoService.RejeitarSolicitacao(vm.IdSolicitacao, vm.Justificativa);
            return RedirectToAction("ListarPendentes", "Solicitacoes");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarSolicitacoesUsuario()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var solicitacoes = await _solicitacaoService.ListarSolicitacosUsuario(userId);
            return View(solicitacoes);

        }

    }
}
