using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AppConsig.Dados;
using AppConsig.Entidades;
using AppConsig.Servicos.Interfaces;

namespace AppConsig.Servicos
{
    public class ServicoPermissao : ServicoEntidade<Permissao>, IServicoPermissao
    {
        public ServicoPermissao(IContexto contexto) 
            : base(contexto) 
        {
            Contexto = contexto;
            Dbset = Contexto.Set<Permissao>();
        }

        public IEnumerable<Permissao> ObterPermissoesDoPerfil(long perfilId)
        {
            var permissoes =
                Contexto.Perfis.Where(p => p.Id == perfilId)
                    .Include(p => p.Permissoes)
                    .First()
                    .Permissoes.ToList();

            return permissoes;
        }
    }
}