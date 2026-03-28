using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Domain.Entities
{
    public class Tarefa
    {
        public int Id { get; set; }
        
       
        public DateTime? Datacadastro { get; set; }
        public DateTime? DataPrevisaoEntrega { get; set; }
        
        public int IdSolicitacao { get; set; }  
        public Solicitacao Solicitacao { get; set; }
        public int IdTarefaStatus { get; set; }
        public TarefaStatus TarefaStatus { get; set; }

       public bool Status {  get; set; }
    }
}
