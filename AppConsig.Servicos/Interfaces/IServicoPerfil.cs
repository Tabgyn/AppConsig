using System;
using AppConsig.Entidades;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoPerfil : IServicoEntidade<Perfil>
    {
        Perfil ObterPerfilComPermissoes(Guid id);
    }
}