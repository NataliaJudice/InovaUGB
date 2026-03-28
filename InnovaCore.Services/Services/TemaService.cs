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
    public class TemaService : ITemaService
    {
        private readonly InnovationCoreDbContext _context;
        
        public TemaService(InnovationCoreDbContext context)
        {
            _context = context;
            
        }

        public async Task<IEnumerable<Temas>> ObterTemasAtivos()
        { return await _context.Temas.Where(t => t.Status).ToListAsync(); }

    }
}
