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
        public async Task<IActionResult> Create(Solicitacao solicitacao)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var email = User.FindFirstValue(ClaimTypes.Email);

                await _solicitacaoService.CriarProposta(solicitacao, userId, email);

                TempData["SuccessMessage"] = "Proposta enviada!";
                return RedirectToAction(nameof(ListarSolicitacoesUsuario));
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Não foi possível processar sua solicitação.";
                ViewBag.Setores = await _setorService.ObterSetoresAtivos();
                return View(solicitacao);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListarPendentes()
        {
            try
            {
                IEnumerable<Solicitacao> solicitacoesPendentes = await _solicitacaoService.ListarPendentes();
                return View(solicitacoesPendentes);
            }
            catch (Exception)
            {
                // Se houver erro de banco na listagem, avisamos o Admin e mandamos para o Hub
                TempData["ErrorMessage"] = "Erro crítico ao carregar solicitações pendentes.";
                return RedirectToAction("HubAdmin", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aprovar(int id)
        {
            if (id <= 0) return BadRequest("Identificador de solicitação inválido.");

            try
            {
                await _solicitacaoService.AprovarSolicitacao(id);
                TempData["SuccessMessage"] = "Solicitação aprovada com sucesso!";
                return RedirectToAction("TodasAsTarefas", "Tarefa");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Falha ao processar aprovação: " + ex.Message;
                return RedirectToAction(nameof(ListarPendentes));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rejeitar(ViewModelRejeicao vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "A justificativa é obrigatória para rejeitar.";
                return RedirectToAction(nameof(ListarPendentes));
            }

            try
            {
                await _solicitacaoService.RejeitarSolicitacao(vm.IdSolicitacao, vm.Justificativa);
                TempData["SuccessMessage"] = "Solicitação rejeitada e usuário notificado.";
                return RedirectToAction(nameof(ListarPendentes));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao rejeitar solicitação: " + ex.Message;
                return RedirectToAction(nameof(ListarPendentes));
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarSolicitacoesUsuario()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Challenge(); // Redireciona para login se a sessão expirou

                var solicitacoes = await _solicitacaoService.ListarSolicitacosUsuario(userId);
                return View(solicitacoes);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Não foi possível carregar seu histórico de solicitações.";
                return RedirectToAction("Hub", "Home");
            }
        }

    }
}
