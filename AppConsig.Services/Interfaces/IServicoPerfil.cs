using AppConsig.Entities;

namespace AppConsig.Services.Interfaces
{
    public interface IServicoPerfil : IServicoBasico<Perfil>
    {
        Perfil ObterPerfilComPermissoes(long perfilId);
    }
}