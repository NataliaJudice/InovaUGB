using InnovaCore.Data.Context;
using InnovaCore.Domain.Entities;
using InnovaCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnovaCore.Services.Services
{
    public class SolicitacaoService : ISolicitacaoService
    {
        private readonly InnovationCoreDbContext _context;
        private readonly IEmailServices _emailServices;
        public SolicitacaoService(InnovationCoreDbContext context, IEmailServices emailServices)
        {
            _context = context;
            _emailServices = emailServices;
        }

       

        /// <summary>
        /// Cria a solicitação e já define ela como "Pendente" inicialmente
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task CriarProposta(Solicitacao solicitacao, string userId, string email)
        {
            solicitacao.Datacadastro = DateTime.Now;
            solicitacao.Status = true;
            solicitacao.IdSolicitacaoStatus = 1;
            solicitacao.IdUsuario = userId;
            _context.Solicitacoes.Add(solicitacao);
            await _context.SaveChangesAsync();
            await _emailServices.SendStatusUpdateEmailAsync(email, solicitacao.Titulo, "Enviada");
        }

        public async Task<IEnumerable<Solicitacao>> ListarPendentes()
        {
            return await _context.Solicitacoes.Where(s => s.IdSolicitacaoStatus == 1).ToListAsync();
        }


        public async Task AprovarSolicitacao(int id)
        {
            var solicitacao = _context.Solicitacoes
                .Include(s => s.Usuario)
                .Where(s => s.Id == id).FirstOrDefault();
            if (solicitacao == null) return;
            solicitacao.IdSolicitacaoStatus = 2;


            Tarefa novatarefa = new Tarefa()
            {
                Datacadastro = DateTime.Now,
                DataPrevisaoEntrega = DateTime.MinValue,
                IdSolicitacao = id,
                IdTarefaStatus = 7,
                Status = true
            };

            _context.Tarefas.Add(novatarefa);
            await _context.SaveChangesAsync();
            await _emailServices.SendStatusUpdateEmailAsync(solicitacao.Usuario.Email, solicitacao.Titulo, "Aprovada");

        }

        public async Task RejeitarSolicitacao(int idSolicitacao, string justificativa)
        {
            var solicitacao = _context.Solicitacoes
                .Include(s => s.Usuario)
                .Where(s => s.Id == idSolicitacao).FirstOrDefault();
            solicitacao.IdSolicitacaoStatus = 3;
            solicitacao.JustificativaRejeicao = justificativa;
            _context.Solicitacoes.Update(solicitacao);
            await _context.SaveChangesAsync();
          //  await _emailServices.SendStatusUpdateEmailAsync(solicitacao.Usuario.Email, solicitacao.Titulo, "Inviabilizada");

        }

        public async Task<IEnumerable<Solicitacao>> ListarSolicitacosUsuario(string IdUser)
        {
            return await _context.Solicitacoes
                 .Include(s => s.SolicitacaoStatus)
                 .Include(s => s.Tema)
                 .Include(s => s.Tarefa) // Carrega a tarefa vinculada
                   .ThenInclude(t => t.TarefaStatus) // Carrega o status do Kanban
                 .Where(s => s.IdUsuario == IdUser)
                 .ToListAsync();
        }

    }
}
