using System.Collections.Generic;
using AppConsig.Entidades;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoOrgao : IServicoEntidade<Orgao>
    {
        List<SistemaFolha> ObterSistemasFolha();
    }
}