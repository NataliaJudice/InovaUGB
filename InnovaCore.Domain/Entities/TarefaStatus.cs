using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Domain.Entities
{
    public class TarefaStatus
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool Status { get; set; }
        public IEnumerable<Tarefa> Tarefas { get; set; }


    }
}
