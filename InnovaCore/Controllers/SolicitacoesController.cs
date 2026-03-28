using InnovaCore.Domain.Entities;
using InnovaCore.Services.Interfaces;
using InnovaCore.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InnovaCore.Controllers
{
    public class SolicitacoesController : Controller
    {
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly ITemaService _temaService;
        public SolicitacoesController(ISolicitacaoService solicitacaoService, ITemaService temaService)
        {
            _solicitacaoService = solicitacaoService;
            _temaService = temaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Temas> TotalTemas = await _temaService.ObterTemasAtivos();
            return View(TotalTemas);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Solicitacao solicitacao)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _solicitacaoService.CriarProposta(solicitacao, userId, email);
            return RedirectToAction("Hub", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ListarPendentes()
        {
            IEnumerable<Solicitacao> solicitacoesPendentes = await _solicitacaoService.ListarPendentes();
            return View(solicitacoesPendentes);
        }

        [HttpPost]
        public async Task<IActionResult> Aprovar(int id)
        {
            await _solicitacaoService.AprovarSolicitacao(id);
            return RedirectToAction("TodasAsTarefas", "Tarefa");

        }

        [HttpPost]
        public async Task<IActionResult> Rejeitar(ViewModelRejeicao vm)
        {
            await _solicitacaoService.RejeitarSolicitacao(vm.IdSolicitacao, vm.Justificativa);
            return RedirectToAction("ListarPendentes", "Solicitacoes");
        }
        [HttpGet]
        public async Task<IActionResult> ListarSolicitacoesUsuario()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var solicitacoes = await _solicitacaoService.ListarSolicitacosUsuario(userId);
            return View(solicitacoes);

        }

    }
}
