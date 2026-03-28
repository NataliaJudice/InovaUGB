using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Domain.Entities
{
    public class SolicitacaoStatus
    {
        public int Id { get; set; }

        public string NomeStatus { get; set; }

        public DateTime? DataCadastro { get; set; }
        public bool Status { get; set; }

        public IEnumerable<Solicitacao> Solicitacao { get; set; }

    }
}
