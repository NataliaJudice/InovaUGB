using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Domain.Entities
{
    public class Temas
    {
        public int Id { get; set; }
        public string TituloTema { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool Status {  get; set; }
        public IEnumerable<Solicitacao> Solicitacaos { get; set; }
    }
}
