using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnovaCore.Domain.Entities
{
    public class Solicitacao
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public DateTime? Datacadastro { get; set; }
        public bool Status {  get; set; }

        public string? JustificativaRejeicao { get; set; }

        public string IdUsuario { get; set; }

        
        public IdentityUser Usuario { get; set; }

        public string? UsuarioAlteracaoStatus { get; set; }
        public IdentityUser? UsuarioAlteracaoStatuss { get; set; }
        public int IdSolicitacaoStatus { get; set; }
        public SolicitacaoStatus SolicitacaoStatus { get; set; }

        public Tarefa? Tarefa { get; set; }

        public int? IdTema {  get; set; }
        public Temas? Tema { get; set; }

        public int? IdSetor { get; set; }
        public Setor? Setor { get; set; }


    }
}
