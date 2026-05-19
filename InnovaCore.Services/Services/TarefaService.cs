using InnovaCore.Data.Context;
using InnovaCore.Domain.Entities;
using InnovaCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AtribuirResponsavel(int idTarefa, string nomeResponsavel)
        {
            try
            {
                var tarefa = await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == idTarefa);

                if (tarefa == null)
                    throw new KeyNotFoundException($"Tarefa com ID {idTarefa} não encontrada.");

                tarefa.NomeResponsavel = nomeResponsavel;

                _context.Tarefas.Update(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atribuir responsável: " + ex.Message);
            }
        }

        public async Task DeletarResponsavel(int idTarefa)
        {
            try
            {
                var tarefa = await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == idTarefa);

                if (tarefa == null)
                    throw new KeyNotFoundException($"Tarefa com ID {idTarefa} não encontrada.");

                tarefa.NomeResponsavel = null;

                _context.Tarefas.Update(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover responsável: " + ex.Message);
            }
        }

        public async Task MudarStatus(int novoStatus, int idTarefa)
        {
            try
            {
                var solicitacao = await _context.Solicitacoes
                    .Include(x => x.Usuario)
                    .Include(x => x.Tarefa)
                    .FirstOrDefaultAsync(t => t.Tarefa.Id == idTarefa);

                if (solicitacao == null || solicitacao.Tarefa == null)
                    throw new KeyNotFoundException("Tarefa ou Solicitação vinculada não encontrada.");


                solicitacao.Tarefa.IdTarefaStatus = novoStatus;

                var statusInfo = await _context.TarefaStatus
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.Id == novoStatus);

                string nomeStatus = statusInfo?.Nome ?? "Status Atualizado";

                await _context.SaveChangesAsync();

                if (solicitacao.Usuario != null && !string.IsNullOrEmpty(solicitacao.Usuario.Email))
                {
                    await _emailServices.SendStatusUpdateEmailAsync(solicitacao.Usuario.Email, solicitacao.Titulo, nomeStatus);
                }

                await _setorServices.EnviarEmailSetor(solicitacao.IdSetor, solicitacao.Titulo, solicitacao.Descricao, nomeStatus);
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao mudar status da tarefa: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Tarefa>> GetAll()
        {
            try
            {
                return await _context.Tarefas
                    .AsNoTracking()
                    .Include(s => s.Solicitacao)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar todas as tarefas.");
            }
        }
    }
}