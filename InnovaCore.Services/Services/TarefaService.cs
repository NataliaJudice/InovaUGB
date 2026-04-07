using InnovaCore.Data.Context;
using InnovaCore.Domain.Entities;
using InnovaCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnovaCore.Services.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly InnovationCoreDbContext _context;
        private readonly IEmailServices _emailServices;
        private readonly ISetorService _setorServices;
        public TarefaService(InnovationCoreDbContext context, IEmailServices emailServices, ISetorService setorServices)
        {
            _context = context;
            _emailServices = emailServices;
            _setorServices = setorServices;
        }


        public async Task MudarStatus(int novoStatus, int idTarefa)
        {

            var solicitacao = await _context.Solicitacoes
                .Include(x => x.Usuario)
                .Include(x => x.Tarefa)
                .FirstOrDefaultAsync(t => t.Tarefa.Id == idTarefa);

            solicitacao.Tarefa.IdTarefaStatus = novoStatus;

            var status = await _context.TarefaStatus.FirstOrDefaultAsync(f => f.Id == novoStatus);

            await _context.SaveChangesAsync();

            await _emailServices.SendStatusUpdateEmailAsync(solicitacao.Usuario.Email, solicitacao.Titulo, status.Nome);
            await _setorServices.EnviarEmailSetor(solicitacao.IdSetor, solicitacao.Titulo, solicitacao.Descricao, status.Nome);

        }

        public async Task<IEnumerable<Tarefa>> GetAll()
        {
            return await _context.Tarefas.Include(s => s.Solicitacao).ToListAsync();
        }



    }
}
