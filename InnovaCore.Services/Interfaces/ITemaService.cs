using InnovaCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Services.Interfaces
{
    public interface ITemaService
    {
        Task<IEnumerable<Temas>> ObterTemasAtivos();
    }
}
