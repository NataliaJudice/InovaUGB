using InnovaCore.Data.Context;
using InnovaCore.Domain.Entities;
using InnovaCore.Services.Interfaces;
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

        public SetorService(InnovationCoreDbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Setor>> ObterSetoresAtivos()
        {
            return await _context.Setor.Where(t => t.Status).ToListAsync();

        }
    }
}
