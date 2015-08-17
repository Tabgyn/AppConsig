using System.Collections.Generic;
using AppConsig.Entidades;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoPermissao : IServicoEntidade<Permissao>
    {
        IEnumerable<Permissao> ObterPermissoesDoPerfil(long perfilId);
    }
}