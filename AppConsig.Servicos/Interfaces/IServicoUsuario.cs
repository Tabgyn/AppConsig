using AppConsig.Entidades;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoUsuario : IServicoEntidade<Usuario>
    {
        bool ValidarUsuario(string login, string senha);
        void ReeviarSenha(Usuario usuario);
    }
}