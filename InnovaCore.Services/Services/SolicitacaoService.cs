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
        private readonly ISetorService _setorServices;
        public SolicitacaoService(InnovationCoreDbContext context, IEmailServices emailServices, ISetorService setorServices)
        {
            _context = context;
            _emailServices = emailServices;
            _setorServices = setorServices;
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
            await EnviarEmails(solicitacao.IdSetor, email, solicitacao.Titulo, solicitacao.Descricao, "Enviada");
        }

        public async Task<IEnumerable<Solicitacao>> ListarPendentes()
        {
            return await _context.Solicitacoes
                .Include( s => s.Setor)
                .Where(s => s.IdSolicitacaoStatus == 1).ToListAsync();
        }


        public async Task AprovarSolicitacao(int id)
        {
            var solicitacao = _context.Solicitacoes
                .Include(s => s.Usuario)
                .Where(s => s.Id == id).FirstOrDefault();
            if (solicitacao == null) return;
            solicitacao.IdSolicitacaoStatus = 2;

            //Transformar em metodo no TarefaService
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
            await EnviarEmails(solicitacao.IdSetor, solicitacao.Usuario.Email, solicitacao.Titulo, solicitacao.Descricao, "aprovada");
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
            await EnviarEmails(solicitacao.IdSetor, solicitacao.Usuario.Email, solicitacao.Titulo, solicitacao.Descricao, "Inviabilizada");

        }

        public async Task<IEnumerable<Solicitacao>> ListarSolicitacosUsuario(string IdUser)
        {
            return await _context.Solicitacoes
                 .Include(s => s.SolicitacaoStatus)
                 .Include(s => s.Setor)
                 .Include(s => s.Tarefa) 
                   .ThenInclude(t => t.TarefaStatus) 
                 .Where(s => s.IdUsuario == IdUser)
                 .OrderByDescending(s => s.Datacadastro)
                 .ToListAsync();
        }

        //transformar isso numa função do EmailServices
        public async Task EnviarEmails(int? id, string userEmail, string tituloTarefa, string descricaoTarefa, string status)
        {
            await _emailServices.SendStatusUpdateEmailAsync(userEmail,tituloTarefa, status);
            await _setorServices.EnviarEmailSetor(id, tituloTarefa, descricaoTarefa, status);
        }

    }
}