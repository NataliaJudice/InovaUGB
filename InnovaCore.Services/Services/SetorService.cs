using InnovaCore.Data.Context;
using InnovaCore.Domain.Entities;
using InnovaCore.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Services.Services
{
    public class SetorService : ISetorService
    {
        private readonly InnovationCoreDbContext _context;
        private readonly IEmailServices _emailService;
        public SetorService(InnovationCoreDbContext context, IEmailServices emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IEnumerable<Setor>> ObterSetoresAtivos()
        {
            return await _context.Setor.Where(t => t.Status == true).ToListAsync();

        }

        public async Task CriarSetor(Setor setor)
        {
            // Garantimos os valores padrão aqui no servidor
            setor.DataCadastro = DateTime.Now;
            setor.Status = true;

            _context.Setor.Add(setor);
            await _context.SaveChangesAsync();
        }

        public async Task EditarSetor(int id, Setor setorNovo)
        {
            var setor = await _context.Setor.FirstOrDefaultAsync(s => s.Id == id);
            if(setor == null)
            {
                throw new Exception("Setor não encontrado no banco de dados.");
            }

            setor.Nome = setorNovo.Nome;
            setor.EmailResponsavel =setorNovo.EmailResponsavel;
            //setor.IdUsuario = setorNovo.IdUsuario;
            await _context.SaveChangesAsync();

        }

        public async Task EnviarEmailSetor(int? id, string taskTitle, string descricaoTarefa, string status)
        {
            var setor = await _context.Setor.FirstOrDefaultAsync(s => s.Id==id);
            var emailSetor = setor.EmailResponsavel;
            await _emailService.SendStatusUpdateEmailAsyncSetores(emailSetor, taskTitle, descricaoTarefa, status);    
        }
    }
}
