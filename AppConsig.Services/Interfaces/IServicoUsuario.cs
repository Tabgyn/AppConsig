using System.Collections.Generic;
using AppConsig.Entities;

namespace AppConsig.Services.Interfaces
{
    public interface IServicoUsuario : IServicoBasico<Usuario>
    {
        bool ValidarUsuario(string nomeDeUsuario, string senha);
        void ResetarSenha(Usuario usuario);
        List<Permissao> ObterPermissoesDoUsuario(long usuarioId);
    }
}