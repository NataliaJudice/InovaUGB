using InnovaCore.Data.Context;
using InnovaCore.Domain.Entities;
using InnovaCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnovaCore.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly InnovationCoreDbContext _context;

        public DashboardService(InnovationCoreDbContext context)
        {
            _context = context;
        }
        public async Task<VwDashboardQtde> GetQtdes()
        {
            return await _context.VwDashboardQtdes.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VwQtdePorSetor>> GetQtdesPorSetor()
        {
            return await _context.VwQtdePorSetor.ToListAsync();
        }

    }
}
