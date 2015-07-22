using AppConsig.Entidades;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoUsuario : IServicoEntidade<Usuario>
    {
        Usuario ObterPeloId(long id);
        bool ValidarUsuario(string login, string senha);
    }
}