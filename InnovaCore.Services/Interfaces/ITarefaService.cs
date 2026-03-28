using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnovaCore.Domain.Entities;

namespace InnovaCore.Services.Interfaces
{
    public interface ITarefaService
    {
        Task<IEnumerable<Tarefa>> GetAll();
        Task MudarStatus(int novoStatus, int idTarefa);
    }
}
