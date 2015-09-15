using System.Collections.Generic;
using System.Linq;
using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoOrgao : ServicoEntidade<Orgao>, IServicoOrgao
    {
        public ServicoOrgao(IContexto contexto) : base(contexto)
        {
            Contexto = contexto;
            Dbset = Contexto.Set<Orgao>();
        }

        public List<SistemaFolha> ObterSistemasFolha()
        {
            var sistemas = Contexto.SistemasFolha.ToList();

            return sistemas;
        }
    }
}