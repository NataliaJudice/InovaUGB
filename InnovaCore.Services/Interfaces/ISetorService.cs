using InnovaCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Services.Interfaces
{
    public interface ISetorService
    {
        Task<IEnumerable<Setor>> ObterSetoresAtivos();
        Task CriarSetor(Setor setor);
        Task EditarSetor(int id, Setor setorNovo);
        Task EnviarEmailSetor(int? id, string taskTitle, string descricaoTarefa, string status);
        
    }
}
